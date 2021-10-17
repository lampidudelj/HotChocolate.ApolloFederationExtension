using FluentAssertions;
using HotChocolate.ApolloFederationExtension.Queries;
using HotChocolate.ApolloFederationExtension.Unions;
using HotChocolate.Execution;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Snapshooter.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotChocolate.ApolloFederationExtension.Tests.Queries.Tests
{
    public class QueryExtensionTests
    {
        private Mock<IHttpContextAccessor> _mockContextAccessor;
        private FederationQueryExtensions _queryExtension;

        public QueryExtensionTests()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _queryExtension = new FederationQueryExtensions(_mockContextAccessor.Object);
        }

        [Fact]
        public void _entities_GivenValidTypename_ReturnsRequestedEntityType()
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
        public async Task RemoveDirectiveTypes_MatchExpected()
        {
            // arrange
            ISchema schema = await new ServiceCollection()
                .AddGraphQL()
                .AddQueryType(x => x.Name("Query").Field("foo").Resolve("Bar"))
                .AddType<Foo>()
                .AddFederationExtensions()
                .BuildSchemaAsync();

            // act
            var resultingSchema = FederationQueryExtensions.RemoveDirectiveTypes(schema.ToDocument());

            // assert
            resultingSchema.ToString().MatchSnapshot();
        }

        [Fact]
        public void RemoveDirectiveTypes_GivenEmptySchema_ThrowsArgumentNullException()
        {
            //act
            Exception? result = Record.Exception(() => FederationQueryExtensions.RemoveDirectiveTypes(null));

            // assert
            Assert.IsType<ArgumentNullException>(result);
        }
    }

    public class Foo : IEntityUnionType
    {
        public string Id { get; set; }

        [GraphQLIgnore]
        public IEntityUnionType ResolveReference(KeyValuePair<string, object> primaryKey)
        {
            return new Foo();
        }
    }
}