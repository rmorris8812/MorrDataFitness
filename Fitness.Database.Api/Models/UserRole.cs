using System;
using System.Collections.Generic;
using System.Text;

namespace Fitness.Database.Api.Models
{
    public class UserRole
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }
    }
}
