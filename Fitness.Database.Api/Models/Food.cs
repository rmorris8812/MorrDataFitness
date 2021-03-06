﻿// ***************************************************************
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
        [Column("serving")]
        public double ServiingSize { get; set; }
        [Column("unit")]
        public int Unit { get; set; }
        [Column("carbs")]
        public int Carbs { get; set; }
        [Column("fat")]
        public int Fat { get; set; }
        [Column("salt")]
        public int Salt { get; set; }
        [Column("protein")]
        public int Protein { get; set; }
        [Column("sugar")]
        public int Sugar { get; set; }
    }
}
