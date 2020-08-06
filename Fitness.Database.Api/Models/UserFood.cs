using System;
using System.Collections.Generic;
using System.Text;

namespace Fitness.Database.Api.Models
{
    public class UserFood
    {
        public long UserFoodId { get; set; }
        public long UserId { get; set; }
        public long FoodId { get; set; }
        public DateTime DateConsumed { get; set; }
        public int Meal { get; set; }
    }
}
