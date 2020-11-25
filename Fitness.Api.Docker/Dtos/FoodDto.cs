// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

namespace Fitness.Api.Dtos
{
    public class FoodDto
    {
        public long FoodId { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public double ServiingSize { get; set; }
        public int Unit { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
        public int Salt { get; set; }
        public int Protein { get; set; }
        public int Sugar { get; set; }
    }
}
