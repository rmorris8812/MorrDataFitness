// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Database.Api.Models
{
    [Table("food")]
    public class Food
    {
        [Key]
        [Column("foodid")] 
        public long FoodId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("calories")]
        public int Calories { get; set; }
    }
}
