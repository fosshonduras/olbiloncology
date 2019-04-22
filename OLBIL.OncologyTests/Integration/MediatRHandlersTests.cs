using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OLBIL.OncologyApplication.Wards.Queries;
using OLBIL.OncologyTests.Utils;

namespace OLBIL.OncologyTests.Integration
{
    [TestClass]
    [TestCategory("Integration")]
    public class MediatRHandlersTests
    {
        [TestMethod]
        public void EnsureAllRequestsHaveHandler()
        {
            var requestsWithoutHandlers = MediatorPair.FindUnmatchedRequests(typeof(GetWardsListQuery).Assembly);

            requestsWithoutHandlers.Should().BeEmpty();
        }
    }
}