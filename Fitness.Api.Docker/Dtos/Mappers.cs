using Fitness.Database.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Api.Dtos
{
    public static class Mappers
    {
        public static DailyGoalDto Map(DailyGoal goal)
        {
            return new DailyGoalDto()
            {
                Calories = goal.Calories,
                Carbs = goal.Carbs,
                Fat = goal.Fat,
                GoalId = goal.GoalId,
                Name = goal.Name,
                Protein = goal.Protein,
                Salt = goal.Salt,
                Sugar = goal.Sugar
            };
        }
        public static FoodDto Map(Food food)
        {
            var dto = new FoodDto()
            {
                Name = food.Name,
                Calories = food.Calories,
                Carbs = food.Carbs,
                Fat = food.Fat,
                Protein = food.Protein,
                Salt = food.Salt,
                ServiingSize = food.ServiingSize,
                Sugar = food.Sugar,
                Unit = food.Unit
            };
            return dto;
        }
        public static Food Map(FoodDto dto)
        {
            var food = new Food()
            {
                Name = dto.Name,
                Calories = dto.Calories,
                Carbs = dto.Carbs,
                Fat = dto.Fat,
                Protein = dto.Protein,
                Salt = dto.Salt,
                ServiingSize = dto.ServiingSize,
                Sugar = dto.Sugar,
                Unit = dto.Unit
            };
            return food;
        }
        public static GoalDto Map(Goal goal)
        {
            return new GoalDto()
            {
                CompleteActualDate = goal.CompleteActualDate,
                CompleteCalculatedDate = goal.CompleteCalculatedDate,
                CompleteDesiredDate = goal.CompleteDesiredDate,
                CurrentWeight = goal.CurrentWeight,
                DesiredWeight = goal.DesiredWeight,
                GoalId = goal.GoalId,
                UserId = goal.UserId
            };
        }
        public static FitnessUser Map(UserDto userDto)
        {
            var user = new FitnessUser()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                // Email = userDto.Email, // This is an alternate key
                Password = userDto.Password,
                TenantId = userDto.TenantId
            };
            return user;
        }
        public static UserDto Map(FitnessUser user)
        {
            var dto = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TenantId = user.TenantId
            };
            return dto;
        }
        public static UserFoodDto Map(UserFood food)
        {
            return new UserFoodDto()
            {
                DateConsumed = food.DateConsumed,
                FoodId = food.FoodId,
                Meal = food.Meal,
                UserFoodId = food.UserFoodId,
                UserId = food.UserId
            };
        }
        public static MeasurementDto Map(Measurement m)
        {
            return new MeasurementDto()
            {
                Arm = m.Arm,
                Chest = m.Chest,
                Hip = m.Hip,
                Leg = m.Leg,
                MeasurementId = m.MeasurementId,
                Neck = m.Neck,
                Unit = m.Unit,
                UserId = m.UserId,
                Waist = m.Waist,
                Weight = m.Weight
            };
        }
    }
}
