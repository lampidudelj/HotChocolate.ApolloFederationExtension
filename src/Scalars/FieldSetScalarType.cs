using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.ApolloFederationExtension.Scalars
{
    public sealed class FieldSetScalarType : ScalarType<string, StringValueNode>
    {
        public FieldSetScalarType()
            : base("_FieldSet")
        {
            Description = "Represents a set of fields";
        }

        public override IValueNode ParseResult(object? resultValue) =>
            ParseValue(resultValue);

        protected override string ParseLiteral(StringValueNode valueSyntax)
        {
            return valueSyntax.Value;
        }

        protected override StringValueNode ParseValue(string runtimeValue)
        {
            return new StringValueNode(runtimeValue);
        }
    }
}