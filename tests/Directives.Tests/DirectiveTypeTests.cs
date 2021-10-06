using HotChocolate.ApolloFederationExtension.Attributes;
using HotChocolate.ApolloFederationExtension.Directives;
using HotChocolate.Execution;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;
using System.Threading.Tasks;
using Xunit;

namespace HotChocolate.ApolloFederationExtension.Tests.Directives.Tests
{
    public class DirectiveTypeTests
    {
        [Fact]
        public async Task ExtendsDirective_IsAddedToTheSchema()
        {
            // arrange
            ISchema schema = await new ServiceCollection()
                .AddGraphQL()
                .AddQueryType(x => x.Name("Query").Field("foo").Resolve("Bar"))
                .AddDirectiveType<ExtendsDirectiveType>()
                .AddType<ExtendsObjectType>()
                .BuildSchemaAsync();

            // act
            var printedSchema = schema.Print();

            // assert
            printedSchema.MatchSnapshot();
        }

        [Fact]
        public async Task KeyDirective_IsAddedToTheSchema()
        {
            // arrange
            ISchema schema = await new ServiceCollection()
                .AddGraphQL()
                .AddQueryType(x => x.Name("Query").Field("foo").Resolve("Bar"))
                .AddDirectiveType<KeyDirectiveType>()
                .AddType<KeyObjectType>()
                .BuildSchemaAsync();

            // act
            var printedSchema = schema.Print();

            // assert
            printedSchema.MatchSnapshot();
        }

        [Fact]
        public async Task ExternalDirective_IsAddedToTheSchema()
        {
            // arrange
            ISchema schema = await new ServiceCollection()
                .AddGraphQL()
                .AddQueryType(x => x.Name("Query").Field("foo").Resolve("Bar"))
                .AddDirectiveType<ExternalDirectiveType>()
                .AddType<ExternalObjectType>()
                .BuildSchemaAsync();

            // act
            var printedSchema = schema.Print();

            // assert
            printedSchema.MatchSnapshot();
        }

        [Fact]
        public async Task ProvidesDirective_IsAddedToTheSchema()
        {
            // arrange
            ISchema schema = await new ServiceCollection()
                .AddGraphQL()
                .AddQueryType(x => x.Name("Query").Field("foo").Resolve("Bar"))
                .AddDirectiveType<ProvidesDirectiveType>()
                .AddType<ProvidesObjectType>()
                .BuildSchemaAsync();

            // act
            var printedSchema = schema.Print();

            // assert
            printedSchema.MatchSnapshot();
        }

        [Fact]
        public async Task RequiresDirective_IsAddedToTheSchema()
        {
            // arrange
            ISchema schema = await new ServiceCollection()
                .AddGraphQL()
                .AddQueryType(x => x.Name("Query").Field("foo").Resolve("Bar"))
                .AddDirectiveType<RequiresDirectiveType>()
                .AddType<RequiresObjectType>()
                .BuildSchemaAsync();

            // act
            var printedSchema = schema.Print();

            // assert
            printedSchema.MatchSnapshot();
        }
    }

    [FederationObjectDirective(Name = "extends")]
    public class ExtendsObjectType
    {
        public string Id { get; set; }
    }

    [FederationObjectDirective(Name = "key", Fields = "id")]
    public class KeyObjectType
    {
        public string Id { get; set; }
    }

    public class ExternalObjectType
    {
        [FederationFieldDirective(Name = "external")]
        public string Id { get; set; }
    }

    public class ProvidesObjectType
    {
        [FederationFieldDirective(Name = "provides", Fields = "id")]
        public string Id { get; set; }
    }

    public class RequiresObjectType
    {
        [FederationFieldDirective(Name = "requires", Fields = "id")]
        public string Id { get; set; }
    }
}