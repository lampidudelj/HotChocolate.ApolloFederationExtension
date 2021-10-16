using AutoFixture;
using FluentAssertions;
using HotChocolate.ApolloFederationExtension.Queries;
using HotChocolate.ApolloFederationExtension.Unions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotChocolate.ApolloFederationExtension.Tests.Queries.Tests
{
    public class QueryExtensionTests
    {
        readonly Fixture _fixture = new();
        private Mock<IHttpContextAccessor> _mockContextAccessor;
        FederationQueryExtensions _queryExtension;

        public QueryExtensionTests()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _queryExtension = new FederationQueryExtensions(_mockContextAccessor.Object);
        }

        [Fact]
        public async Task _entities_GivenValidTypename_ReturnsRequestedEntityType()
        {
            // arrange
            List<object> representations = new List<object>() { new Dictionary<string, object>() { { "__typename", "Foo" }, { "id", "1" } } };

            // act
            var actual = _queryExtension._entities(representations);

            // assert
            actual.Count.Should().Be(1);
            actual.First().GetType().Should().Be(typeof(Foo));
        }

        [Fact]
        public async Task _service()
        {
            // arrange
            List<object> representations = new List<object>() { new Dictionary<string, object>() { { "__typename", "Foo" }, { "id", "1" } } };

            // act
            var actual = _queryExtension._entities(representations);

            // assert
            actual.Count.Should().Be(1);
            actual.First().GetType().Should().Be(typeof(Foo));
        }

    }

    internal class Foo : IEntityUnionType
    {
        public IEntityUnionType ResolveReference(KeyValuePair<string, object> primaryKey)
        {
            return new Foo();
        }
    }
}
