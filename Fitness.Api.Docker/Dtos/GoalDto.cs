using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Api.Dtos
{
    public class GoalDto
    {
        public long GoalId { get; set; }
        public float CurrentWeight { get; set; }
        public float DesiredWeight { get; set; }
        public DateTime CompleteDesiredDate { get; set; }
        public DateTime CompleteCalculatedDate { get; set; }
        public DateTime CompleteActualDate { get; set; }
        public long UserId { get; set; }
    }
}
