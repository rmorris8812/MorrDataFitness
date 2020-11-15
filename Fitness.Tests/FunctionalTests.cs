using Fitness.Database.Api;
using Fitness.Database.Api.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitness.Tests
{
    [TestClass]
    public class FunctionalTests
    {
        private static readonly string ConnectionString = "host=localhost;port=5432;database=Fitness;user id=postgres; password=admin123$";
        [TestInitialize]
        public async Task TestInitialize()
        {
            // Clean database
            var databaseApi = new DatabaseApi(ConnectionString);
            for (int id = 10; id <= 11; id++)
            {
                var user = databaseApi.GetUserByIdAsync(id, CancellationToken.None).Result;
                if (user != null)
                {
                    await databaseApi.DeleteUserAsync(user.Id, CancellationToken.None);
                }
            }
            // Seed database
            var newUser = new FitnessUser()
            {
                Id = 10,
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foobar@acme.com",
                Password = "secret",
                TenantId = "1234",
                ExternalAuth = false
            };
            await databaseApi.InsertUserAsync(newUser, CancellationToken.None);
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("Roy Morris")]
        public void TestGetUser_Sucess()
        {
            var databaseApi = new DatabaseApi(ConnectionString);
            var user = databaseApi.GetUserByEmailAsync("foobar@acme.com", CancellationToken.None).Result;
            user.Should().NotBeNull();
            user.FirstName.Should().BeEquivalentTo("Foo");
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("Roy Morris")]
        public void TestInsertUser_Sucess()
        {
            var databaseApi = new DatabaseApi(ConnectionString);
            var user = new FitnessUser()
            {
                Email = "jane.doe@acme.com",
                ExternalAuth = false,
                FirstName = "Jane",
                LastName = "Doe",
                Password = "secret",
                TenantId = Guid.NewGuid().ToString(),
                Id = 11
            };
                
            var id = databaseApi.InsertUserAsync(user, CancellationToken.None).Result;
            id.Should().NotBe(0);
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("Roy Morris")]
        public async Task TestUpdateUser_Sucess()
        {
            var databaseApi = new DatabaseApi(ConnectionString);
            var user = await databaseApi.GetUserByIdAsync(10, CancellationToken.None);
            user.Should().NotBeNull();
            user.FirstName = "Joe";
            user.LastName = "Blow";
            await databaseApi.UpdateUserAsync(user, CancellationToken.None);

            user = await databaseApi.GetUserByIdAsync(10, CancellationToken.None);
            user.Should().NotBeNull();
            user.FirstName.Should().BeEquivalentTo("Joe");
            user.LastName.Should().BeEquivalentTo("Blow");
        }
    }
}
