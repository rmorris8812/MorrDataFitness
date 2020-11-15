// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

namespace Fitness.Database.Api.Models
{
    public class UserRole
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }
    }
}
