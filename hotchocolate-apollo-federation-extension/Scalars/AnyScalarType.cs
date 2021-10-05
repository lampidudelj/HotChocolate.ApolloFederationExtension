using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Utilities;
using System;
using System.Collections.Generic;

namespace HotChocolate.ApolloFederationExtension.Scalars
{
    public sealed class AnyScalarType : ScalarType<object>
    {
        private readonly ObjectValueToDictionaryConverter _objectValueToDictConverter = new ObjectValueToDictionaryConverter();

        public AnyScalarType() : base("_Any")
        {
            Description = "Used to pass representations of entities from external services into the root _entities field for execution.";
        }

        public override bool IsInstanceOfType(IValueNode valueSyntax)
        {
            if (valueSyntax is null)
            {
                throw new ArgumentNullException(nameof(valueSyntax));
            }

            switch (valueSyntax)
            {
                case ObjectValueNode:
                    return true;

                default:
                    return false;
            }
        }

        public override object ParseLiteral(IValueNode valueSyntax, bool withDefaults = true)
        {
            switch (valueSyntax)
            {
                case ObjectValueNode ovn:
                    Dictionary<string, object> value = _objectValueToDictConverter.Convert(ovn);
                    return value;

                default:
                    throw new SerializationException("Unable to parse AnyType Literal", this);
            }
        }

        public override IValueNode ParseResult(object resultValue) =>
            ParseValue(resultValue);

        public override IValueNode ParseValue(object? runtimeValue)
        {
            if (runtimeValue is null)
            {
                return NullValueNode.Default;
            }

            switch (runtimeValue)
            {
                case string s:
                    return new StringValueNode(s);

                case int i:
                    return new IntValueNode(i);

                case double d:
                    return new FloatValueNode(d);

                case bool b:
                    return new BooleanValueNode(b);
            }

            if (runtimeValue is IReadOnlyDictionary<string, object> dict)
            {
                var fields = new List<ObjectFieldNode>();
                foreach (KeyValuePair<string, object> field in dict)
                {
                    fields.Add(new ObjectFieldNode(
                        field.Key,
                        ParseValue(field.Value)));
                }
                return new ObjectValueNode(fields);
            }

            if (runtimeValue is IReadOnlyList<object> list)
            {
                var valueList = new List<IValueNode>();
                foreach (object element in list)
                {
                    valueList.Add(ParseValue(element));
                }
                return new ListValueNode(valueList);
            }

            throw new SerializationException("Unable to parse AnyType Value", this);
        }
    }
}