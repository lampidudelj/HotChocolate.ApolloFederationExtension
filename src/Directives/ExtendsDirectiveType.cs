using HotChocolate.Types;

namespace HotChocolate.ApolloFederationExtension.Directives
{
    public class ExtendsDirectiveType : DirectiveType
    {
        protected override void Configure(IDirectiveTypeDescriptor descriptor)
        {
            descriptor.Name("extends");
            descriptor.Location(DirectiveLocation.Object | DirectiveLocation.Interface);
        }
    }
}