// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Api.Configuration;
using Fitness.Database.Api;
using Fitness.Database.Api.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
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
                    TenantId = "acme.com",
                    ExternalAuth = false
                };
                await databaseApi.InsertUserAsync(newUser, CancellationToken.None);
            }
        }
        #region User
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("Roy Morris")]
        public void TestGetUser_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var user = databaseApi.GetUserByEmailAsync("foobar@acme.com", CancellationToken.None).Result;
                user.Should().NotBeNull();
                user.FirstName.Should().BeEquivalentTo("Foo");
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestInsertUser_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var user = new FitnessUser()
                {
                    Email = "jane.doe@acme.com",
                    ExternalAuth = false,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Password = "secret",
                    TenantId = "acme.com",
                    Id = 11
                };

                var id = databaseApi.InsertUserAsync(user, CancellationToken.None).Result;
                id.Should().NotBe(0);
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public async Task TestUpdateUser_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
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
        #endregion
        #region Food
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestGetFood_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = databaseApi.GetFood(0, 100);
                food.Should().NotBeNull();
                food.Count.Should().NotBe(0);
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestGetFoodByDate_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = databaseApi.GetFood(0, 100);
                food.Should().NotBeNull();
                food.Count.Should().NotBe(0);
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestInsertFood_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = new Food()
                {
                    Calories = 10,
                    Carbs = 10,
                    Fat = 10,
                    Name = "FooFood",
                    Protein = 10,
                    Salt = 10,
                    ServiingSize = 1,
                    Unit = 0,
                    Sugar = 1
                };
                var foodId = databaseApi.InsertFoodAsync(food, CancellationToken.None).Result;
                foodId.Should().NotBe(0);
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestSearchFood_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = databaseApi.SearchFood("Banana");
                food.Should().NotBeNull();
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestGetFoodById_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = databaseApi.GetFoodByIdAsync(1, CancellationToken.None).Result;
                food.Should().NotBeNull();
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestUpdateFood_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = databaseApi.GetFoodByIdAsync(1, CancellationToken.None).Result;
                food.Should().NotBeNull();
                food.Calories = 70;
                var foodId = databaseApi.UpdateFoodAsync(food, CancellationToken.None).Result;
                foodId.Should().NotBe(0);

                food = databaseApi.GetFoodByIdAsync(1, CancellationToken.None).Result;
                food.Calories.Should().Be(70);
            }
        }
        #endregion
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestInsertUserFood_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = new UserFood()
                {
                    DateConsumed = DateTime.Now,
                    Meal = 0,
                    FoodId = 1,
                    UserId = 10
                };
                var foodId= databaseApi.InsertUserFoodAsync(food, CancellationToken.None).Result;
                foodId.Should().NotBe(0);
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestGetUserFoodByDate_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = new UserFood()
                {
                    DateConsumed = DateTime.Now,
                    Meal = 0,
                    FoodId = 1,
                    UserId = 10
                };
                var foodId = databaseApi.InsertUserFoodAsync(food, CancellationToken.None).Result;
                foodId.Should().NotBe(0);

                var foods = databaseApi.GetUserFood(0, 100, DateTime.Now);
                foods.Should().NotBeNull();
                foods.Count.Should().NotBe(0);
            }
        }
        [TestMethod]
        [TestCategory("Functional")]
        [Owner("roy.morris@morrdata.com")]
        public void TestGetUserFoodByDateAndMeal_Sucess()
        {
            using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
            {
                var food = new UserFood()
                {
                    DateConsumed = DateTime.Now,
                    Meal = MealTypes.Breakfast,
                    FoodId = 1,
                    UserId = 10
                };
                var foodId = databaseApi.InsertUserFoodAsync(food, CancellationToken.None).Result;
                foodId.Should().NotBe(0);

                var foods = databaseApi.GetUserFood(0, 100, DateTime.Now, MealTypes.Breakfast);
                foods.Should().NotBeNull();
                foods.Count.Should().NotBe(0);
            }
        }
    }
}
