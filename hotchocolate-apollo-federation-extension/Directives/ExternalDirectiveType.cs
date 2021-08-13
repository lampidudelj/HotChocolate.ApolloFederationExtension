using HotChocolate.Types;

namespace HotChocolate.ApolloFederationExtension.Directives
{
    public class ExternalDirectiveType : DirectiveType
    {
        protected override void Configure(IDirectiveTypeDescriptor descriptor)
        {
            descriptor.Name("external");
            descriptor.Location(DirectiveLocation.FieldDefinition);
        }
    }
}