using HotChocolate.Execution;
using Snapshooter.Xunit;
using System.Threading.Tasks;

namespace HotChocolate.ApolloFederationExtension.Tests
{
    public static class SnapshotExtensions
    {
        public static async Task<string> ToJsonAsync(this Task<IExecutionResult> task)
        {
            IExecutionResult result = await task;
            return await result.ToJsonAsync();
        }

        public static void MatchSnapshot(this GraphQLException ex)
        {
            QueryResultBuilder.CreateError(ex.Errors).ToJson().MatchSnapshot();
        }
    }
}