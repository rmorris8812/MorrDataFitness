// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;

namespace Fitness.Api.Dtos
{
    public class DashboardDto
    {
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public int CaloriesConsumed { get; set; }
        public int CaloriesBurned { get; set; }
        public int Unit { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
        public int Salt { get; set; }
        public int Protein { get; set; }
        public int Sugar { get; set; }
        public DailyGoalDto DailyGoal { get; set; }
        public GoalDto Goal { get; set; }
        public MeasurementDto LastMeasurment { get; set; }
    }
}
