using HotChocolate.ApolloFederationExtension.Scalars;
using HotChocolate.Types;

namespace HotChocolate.ApolloFederationExtension.Directives
{
    public class ProvidesDirective
    {
        [GraphQLType(typeof(FieldSetScalarType))]
        public string Fields { get; set; }
    }

    public class ProvidesDirectiveType : DirectiveType<ProvidesDirective>
    {
        protected override void Configure(IDirectiveTypeDescriptor<ProvidesDirective> descriptor)
        {
            descriptor.Name("provides");
            descriptor.Location(DirectiveLocation.FieldDefinition);
        }
    }
}