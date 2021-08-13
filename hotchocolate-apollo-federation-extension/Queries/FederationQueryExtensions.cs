using HotChocolate.ApolloFederationExtension.Scalars;
using HotChocolate.ApolloFederationExtension.Unions;
using HotChocolate.Language;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HotChocolate.ApolloFederationExtension.Queries
{
    [ExtendObjectType("Query")]
    public class FederationQueryExtensions
    {
        private List<Type> _entityUnionTypeAssemblies = new List<Type>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FederationQueryExtensions(IHttpContextAccessor httpContextAccessor)
        {
            _entityUnionTypeAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                                                  .SelectMany(x => x.GetTypes())
                                                  .Where(x => typeof(IEntityUnionType).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                                                  .ToList();

            _httpContextAccessor = httpContextAccessor;
        }

        public List<IEntityUnionType> _entities([GraphQLType(typeof(NonNullType<ListType<NonNullType<AnyScalarType>>>))] List<object> representations)
        {
            List<IEntityUnionType> entities = new List<IEntityUnionType>();

            //resolve reference of each incoming representation
            foreach (Dictionary<string, object> representation in representations)
            {
                if (representation.TryGetValue("__typename", out var typename))
                {
                    Type type = _entityUnionTypeAssemblies.FirstOrDefault(a => a.Name == typename.ToString());

                    if (type != null)
                    {
                        IEntityUnionType resolverClass = (IEntityUnionType)FormatterServices.GetUninitializedObject(type);

                        entities.Add(resolverClass.ResolveReference(representation.FirstOrDefault(r => r.Key != "__typename")));
                    }
                }
            }

            return entities;
        }

        public async Task<_Service> _service()
        {
            HttpContext current = _httpContextAccessor.HttpContext;
            string appBaseUrl = $"{current.Request.Scheme}://{current.Request.Host}{current.Request.PathBase}";

            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(appBaseUrl)
            };

            var schemaString = await client.GetStringAsync("/graphql?sdl");

            DocumentNode schema = Utf8GraphQLParser.Parse(schemaString);
            return new _Service() { Sdl = RemoveDirectiveTypes(schema).ToString() };
        }

        public static DocumentNode RemoveDirectiveTypes(DocumentNode schema)
        {
            HashSet<string> _directiveNames =
            new()
            {
                "extends",
                "external",
                "key",
                "provides",
                "requires",
                "specifiedBy",
                "stream",
                "defer"
            };

            if (schema is null)
            {
                throw new System.ArgumentNullException(nameof(schema));
            }

            var definitions = new List<IDefinitionNode>();

            foreach (IDefinitionNode definition in schema.Definitions)
            {
                if (definition is DirectiveDefinitionNode directive)
                {
                    if (!_directiveNames.Contains(directive.Name.Value))
                    {
                        definitions.Add(definition);
                    }
                }
                else if (definition is ScalarTypeDefinitionNode scalar && scalar.Directives.Count > 0)
                {
                    var scalarDirectives = new List<DirectiveNode>();
                    foreach (DirectiveNode scalarDirective in scalar.Directives)
                    {
                        if (!_directiveNames.Contains(scalarDirective.Name.Value))
                        {
                            scalarDirectives.Add(scalarDirective);
                        }
                    }
                    ScalarTypeDefinitionNode copyNode = scalar.WithDirectives(scalarDirectives);
                    definitions.Add(copyNode);
                }
                else
                {
                    definitions.Add(definition);
                }
            }

            return new DocumentNode(definitions);
        }

        public class _Service
        {
            public string Sdl { get; set; }
        }
    }
}