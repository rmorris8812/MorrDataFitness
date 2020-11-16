// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Database.Api.Models
{
    [Table("fitnessuser")]
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
        [Column("tenantid")]
        public string TenantId { get; set; }
        [Column("externalauth")]
        public bool ExternalAuth { get; set; }
        [Column("userrole")]
        public string UserRole { get; set; }
    }
}
