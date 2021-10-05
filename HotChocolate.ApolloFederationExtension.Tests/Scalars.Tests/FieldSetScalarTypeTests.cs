using HotChocolate.ApolloFederationExtension.Scalars;
using HotChocolate.Language;
using Snapshooter.Xunit;
using System;
using System.Collections.Generic;
using Xunit;

namespace HotChocolate.ApolloFederationExtension.Tests.Scalars.Tests
{
    public class FieldSetScalarTypeTests : ScalarTypeTestBase
    {
        [Fact]
        public void Schema_WithScalar_IsMatch()
        {
            // arrange
            ISchema schema = BuildSchema<FieldSetScalarType>();

            // act
            // assert
            schema.ToString().MatchSnapshot();
        }

        [Theory]
        [InlineData(typeof(StringValueNode), "foo")]
        public void ParseLiteral_GivenObjectValueNode_MatchExpected(Type type, object value)
        {
            // arrange
            IValueNode valueNode = CreateValueNode(type, value);

            // act
            // assert
            ExpectParseLiteralToMatch<FieldSetScalarType>(valueNode, value);
        }

        [Theory]
        [InlineData(typeof(EnumValueNode), TestEnum.Foo)]
        [InlineData(typeof(FloatValueNode), 1d)]
        [InlineData(typeof(IntValueNode), 1)]
        [InlineData(typeof(BooleanValueNode), true)]
        public void ParseLiteral_GivenValueNode_ThrowSerializationException(Type type, object value)
        {
            // arrange
            IValueNode valueNode = CreateValueNode(type, value);

            // act
            // assert
            ExpectParseLiteralToThrowSerializationException<FieldSetScalarType>(valueNode);
        }

        [Theory]
        [InlineData(typeof(StringValueNode), "foo")]
        public void ParseValue_GivenObject_MatchExpectedType(Type type, object value)
        {
            // arrange
            // act
            // assert
            ExpectParseValueToMatchType<FieldSetScalarType>(value, type);
        }

        [Theory]
        [InlineData(TestEnum.Foo)]
        [InlineData(1d)]
        [InlineData(1)]
        [InlineData(true)]
        public void ParseValue_GivenObject_ThrowSerializationException(object value)
        {
            // arrange
            // act
            // assert
            ExpectParseValueToThrowSerializationException<FieldSetScalarType>(value);
        }

        [Theory]
        [InlineData(typeof(StringValueNode), "foo")]
        public void ParseResult_GivenObject_MatchExpectedType(Type type, object value)
        {
            // arrange
            // act
            // assert
            ExpectParseResultToMatchType<FieldSetScalarType>(value, type);
        }

        [Theory]
        [InlineData(TestEnum.Foo)]
        [InlineData(1d)]
        [InlineData(1)]
        [InlineData(true)]
        public void ParseResult_GivenObject_ThrowSerializationException(object value)
        {
            // arrange
            // act
            // assert
            ExpectParseResultToThrowSerializationException<FieldSetScalarType>(value);
        }
    }
}
