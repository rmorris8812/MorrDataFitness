using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fitness.Database.Api.Models
{
    [Table("measurement")]
    public class Measurement
    {
        [Key]
        [Column("measurementid")]
        public long MeasurementId { get; set; }
        [Column("currentweight")]
        public float Weight { get; set; }
        [Column("unit")]
        public int Unit { get; set; }
        [Column("currentneck")]
        public int Neck { get; set; }
        [Column("currentchest")]
        public int Chest { get; set; }
        [Column("currentwaist")]
        public int Waist { get; set; }
        [Column("currentarm")]
        public int Arm { get; set; }
        [Column("currenthip")]
        public int Hip { get; set; }
        [Column("currentleg")]
        public int Leg { get; set; }
        [Column("userid")]
        public long UserId { get; set; }
    }
}
