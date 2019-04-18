using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OLBIL.OncologyApplication.Wards.Queries;
using OLBIL.OncologyTests.Utils;

namespace OLBIL.OncologyTests.Integration
{
    [TestClass]
    public class MediatRHandlersTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void EnsureAllRequestsHaveHandler()
        {
            var requestsWithoutHandlers = MediatorPair.FindUnmatchedRequests(typeof(GetWardsListQuery).Assembly);

            requestsWithoutHandlers.Should().BeEmpty();
        }
    }
}