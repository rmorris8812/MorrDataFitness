using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Database.Api.Models
{
    [Table("goal")]
    public class Goal
    {
        [Key]
        [Column("goalid")]
        public long GoalId { get; set; }
        [Column("currentweight")]
        public float CurrentWeight { get; set; }
        [Column("desiredweight")]
        public float DesiredWeight { get; set; }
        [Column("completedesireddate")]
        public DateTime CompleteDesiredDate { get; set; }
        [Column("completecalculateddate")]
        public DateTime CompleteCalculatedDate { get; set; }
        [Column("completeactualdate")]
        public DateTime CompleteActualDate { get; set; }
        [Column("userid")]
        public long UserId { get; set; }
    }
}
