﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Database.Api.Models
{
    public class FitnessUser
    {
        [Key]
        [Column("userid")]
        public long Id { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("token")]
        public string Token { get; set; }
        public long TenantId { get; set; }
        public bool ExternalAuth { get; set; }
    }
}
