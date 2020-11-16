// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;

namespace Fitness.Api.Dtos
{
    public class UserFoodDto
    {
        public long UserFoodId { get; set; }
        public long UserId { get; set; }
        public long FoodId { get; set; }
        public DateTime DateConsumed { get; set; }
        public int Meal { get; set; }
    }
}
