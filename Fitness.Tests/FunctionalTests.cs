using Fitness.Database.Api;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading;

namespace Fitness.Tests
{
    [TestClass]
    public class FunctionalTests
    {
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("Roy Morris")]
        public void TestGetUser_Sucess()
        {
            var databaseApi = new DatabaseApi();
            var user = databaseApi.GetUserByEmailAsync("rmorris8812@gmail.com", CancellationToken.None).Result;
            user.Should().NotBeNull();
            user.FirstName.Should().BeEquivalentTo("Roy");
        }
    }
}
