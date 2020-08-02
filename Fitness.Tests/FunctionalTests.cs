using Fitness.Database.Api;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

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
            var user = databaseApi.GetUserByEmail("rmorris8812@gmail.com");
            user.Should().NotBeNull();
            user.FirstName.Should().BeEquivalentTo("Roy");
        }
    }
}
