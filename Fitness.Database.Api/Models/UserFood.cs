// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Database.Api.Models
{
    [Table("userfood")]
    public class UserFood
    {
        [Key]
        [Column("userfoodid")]
        public long UserFoodId { get; set; }
        [Column("userid")]
        public long UserId { get; set; }
        [Column("foodid")]
        public long FoodId { get; set; }
        [Column("consumedate")]
        public DateTime DateConsumed { get; set; }
        [Column("meal")]
        public int Meal { get; set; }
    }
}
