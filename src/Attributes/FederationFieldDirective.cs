using HotChocolate.ApolloFederationExtension.Directives;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace HotChocolate.ApolloFederationExtension.Attributes
{
    public class FederationFieldDirective : ObjectFieldDescriptorAttribute
    {
        public string Name { get; set; }

        public string Fields { get; set; }

        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        {
            switch (Name)
            {
                case "requires":
                    descriptor.Directive(new RequiresDirective() { Fields = Fields });
                    break;

                case "provides":
                    descriptor.Directive(new ProvidesDirective() { Fields = Fields });
                    break;

                case "external":
                    descriptor.Directive<ExternalDirectiveType>();
                    break;

                default:
                    descriptor.Directive<ExternalDirectiveType>();
                    break;
            }
        }
    }
}