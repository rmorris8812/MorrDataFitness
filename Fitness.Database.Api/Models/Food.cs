using System;
using System.Collections.Generic;
using System.Text;

namespace Fitness.Database.Api.Models
{
    public class Food
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
    }
}
