using HotChocolate.ApolloFederationExtension.Scalars;
using HotChocolate.Types;

namespace HotChocolate.ApolloFederationExtension.Directives
{
    public class RequiresDirective
    {
        [GraphQLType(typeof(FieldSetScalarType))]
        public string Fields { get; set; }
    }

    public class RequiresDirectiveType : DirectiveType<RequiresDirective>
    {
        protected override void Configure(IDirectiveTypeDescriptor<RequiresDirective> descriptor)
        {
            descriptor.Name("requires");
            descriptor.Location(DirectiveLocation.FieldDefinition);
        }
    }
}