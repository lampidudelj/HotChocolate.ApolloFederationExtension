using HotChocolate.ApolloFederationExtension.Scalars;
using HotChocolate.Language;
using Snapshooter.Xunit;
using System;
using System.Collections.Generic;
using Xunit;

namespace HotChocolate.ApolloFederationExtension.Tests.Scalars.Tests
{
    public class AnyScalarTypeTests : ScalarTypeTestBase
    {
        [Fact]
        public void Schema_WithScalar_IsMatch()
        {
            // arrange
            ISchema schema = BuildSchema<AnyScalarType>();

            // act
            // assert
            schema.ToString().MatchSnapshot();
        }

        [Theory]
        [InlineData(typeof(EnumValueNode), TestEnum.Foo, false)]
        [InlineData(typeof(FloatValueNode), 1d, false)]
        [InlineData(typeof(IntValueNode), 1, false)]
        [InlineData(typeof(BooleanValueNode), true, false)]
        [InlineData(typeof(StringValueNode), "foo", false)]
        [InlineData(typeof(NullValueNode), null, false)]
        public void IsInstanceOfType_GivenValueNode_MatchExpected(Type type, object value, bool expected)
        {
            // arrange
            IValueNode valueNode = CreateValueNode(type, value);

            // act
            // assert
            ExpectIsInstanceOfTypeToMatch<AnyScalarType>(valueNode, expected);
        }

        [Fact]
        public void IsInstanceOfType_GivenObjectValueNode_MatchExpected()
        {
            // arrange
            ObjectValueNode valueNode = new ObjectValueNode(new ObjectFieldNode("id", 1), new ObjectFieldNode("name", "foo"));

            // act
            // assert
            ExpectIsInstanceOfTypeToMatch<AnyScalarType>(valueNode, true);
        }

        [Fact]
        public void ParseLiteral_GivenObjectValueNode_MatchExpected()
        {
            // arrange
            ObjectValueNode valueNode = new ObjectValueNode(new ObjectFieldNode("id", 1), new ObjectFieldNode("name", "foo"));
            Dictionary<string, object> expected = new Dictionary<string, object>() { { "id", 1 }, { "name", "foo" } };

            // act
            // assert
            ExpectParseLiteralToMatch<AnyScalarType>(valueNode, expected);
        }

        [Theory]
        [InlineData(typeof(EnumValueNode), TestEnum.Foo)]
        [InlineData(typeof(FloatValueNode), 1d)]
        [InlineData(typeof(IntValueNode), 1)]
        [InlineData(typeof(BooleanValueNode), true)]
        [InlineData(typeof(StringValueNode), "")]
        [InlineData(typeof(StringValueNode), "foo")]
        public void ParseLiteral_GivenValueNode_ThrowSerializationException(Type type, object value)
        {
            // arrange
            IValueNode valueNode = CreateValueNode(type, value);

            // act
            // assert
            ExpectParseLiteralToThrowSerializationException<AnyScalarType>(valueNode);
        }

        [Theory]
        [InlineData(typeof(FloatValueNode), 1d)]
        [InlineData(typeof(IntValueNode), 1)]
        [InlineData(typeof(BooleanValueNode), true)]
        [InlineData(typeof(StringValueNode), "foo")]
        [InlineData(typeof(NullValueNode), null)]
        public void ParseValue_GivenObject_MatchExpectedType(Type type, object value)
        {
            // arrange
            // act
            // assert
            ExpectParseValueToMatchType<AnyScalarType>(value, type);
        }

        [Fact]
        public void ParseValue_GivenDictionaryObject_MatchExpectedType()
        {
            // arrange
            Dictionary<string, object> value = new Dictionary<string, object>() { { "id", 1 }, { "name", "foo" } };

            // act
            // assert
            ExpectParseValueToMatchType<AnyScalarType>(value, typeof(ObjectValueNode));
        }


        [Fact]
        public void ParseValue_GivenListObject_MatchExpectedType()
        {
            // arrange
            List<object> value = new List<object>() {{ "id" }, { 1 }, { "name" }, { "foo" }};

            // act
            // assert
            ExpectParseValueToMatchType<AnyScalarType>(value, typeof(ListValueNode));
        }

        [Theory]
        [InlineData(TestEnum.Foo)]
        public void ParseValue_GivenObject_ThrowSerializationException(object value)
        {
            // arrange
            // act
            // assert
            ExpectParseValueToThrowSerializationException<AnyScalarType>(value);
        }

        [Theory]
        [InlineData(typeof(FloatValueNode), 1d)]
        [InlineData(typeof(IntValueNode), 1)]
        [InlineData(typeof(BooleanValueNode), true)]
        [InlineData(typeof(StringValueNode), "foo")]
        [InlineData(typeof(NullValueNode), null)]
        public void ParseResult_GivenObject_MatchExpectedType(Type type, object value)
        {
            // arrange
            // act
            // assert
            ExpectParseResultToMatchType<AnyScalarType>(value, type);
        }

        [Fact]
        public void ParseResult_GivenDictionaryObject_MatchExpectedType()
        {
            // arrange
            Dictionary<string, object> value = new Dictionary<string, object>() { { "id", 1 }, { "name", "foo" } };

            // act
            // assert
            ExpectParseResultToMatchType<AnyScalarType>(value, typeof(ObjectValueNode));
        }


        [Fact]
        public void ParseResult_GivenListObject_MatchExpectedType()
        {
            // arrange
            List<object> value = new List<object>() { { "id" }, { 1 }, { "name" }, { "foo" } };

            // act
            // assert
            ExpectParseResultToMatchType<AnyScalarType>(value, typeof(ListValueNode));
        }

        [Theory]
        [InlineData(TestEnum.Foo)]
        public void ParseResult_GivenObject_ThrowSerializationException(object value)
        {
            // arrange
            // act
            // assert
            ExpectParseResultToThrowSerializationException<AnyScalarType>(value);
        }
    }
}
