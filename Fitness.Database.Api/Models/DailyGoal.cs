using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Database.Api.Models
{
    [Table("dailygoal")]
    public class DailyGoal
    {
        [Key]
        [Column("goalid")]
        public long GoalId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("calories")]
        public int Calories { get; set; }
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
