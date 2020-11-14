using Fitness.Database.Api;
using Fitness.Database.Api.Models;
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
        [TestInitialize]
        public void TestInitialize()
        {
            var databaseApi = new DatabaseApi("host=localhost;port=5432;database=Fitness;user id=postgres; password=admin123$");
            var user = databaseApi.GetUserByEmailAsync("jane.dow@acme.com", CancellationToken.None).Result;
            if (user != null)
            {
                databaseApi.DeleteUserAsync(user.Id, CancellationToken.None);
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("Roy Morris")]
        public void TestGetUser_Sucess()
        {
            var databaseApi = new DatabaseApi("host=localhost;port=5432;database=Fitness;user id=postgres; password=admin123$");
            var user = databaseApi.GetUserByEmailAsync("rmorris8812@gmail.com", CancellationToken.None).Result;
            user.Should().NotBeNull();
            user.FirstName.Should().BeEquivalentTo("Roy");
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("Roy Morris")]
        public void TestInsertUser_Sucess()
        {
            var databaseApi = new DatabaseApi("host=localhost;port=5432;database=Fitness;user id=postgres; password=admin123$");
            var user = new FitnessUser()
            {
                Email = "jane.doe@acme.com",
                ExternalAuth = false,
                FirstName = "Jane",
                LastName = "Doe",
                Password = "secret",
                TenantId = Guid.NewGuid().ToString(),
                Id = 2
            };
                
            var id = databaseApi.InsertUserAsync(user, CancellationToken.None).Result;
            id.Should().NotBe(0);
        }
    }
}
