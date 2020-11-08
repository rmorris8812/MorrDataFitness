/*---------------------------------------------------------------------------
 * ProfileService.cs
 *---------------------------------------------------------------------------
 * Copyright (C) Ivanti Corporation 2020. All rights reserved.
 *
 * This file contains trade secrets of the Ivanti Corporation. No part
 * may be reproduced or transmitted in any form by any means or for any purpose
 * without the express written permission of the Ivanti Corporation.
 *---------------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Fitness.Database.Api;
using Fitness.Database.Api.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly DatabaseApi _userRepository = new DatabaseApi();
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                // Depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    // Get user from db (in my case this is by email)
                    var user = await _userRepository.GetUserByEmailAsync(context.Subject.Identity.Name, CancellationToken.None);
                    if (user != null)
                    {
                        var roleMembership = await _userRepository.GetUserRoleByUserIdAsync(user.Id, CancellationToken.None);
                        context.IssuedClaims = GetUserClaims(user, roleMembership);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.Run(() =>
            {
                try
                {
                    context.IsActive = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
        }
        public static List<Claim> GetUserClaims(FitnessUser user, UserRole role)
        {
            return new List<Claim>()
            {
                new Claim("user_id", user.Id.ToString() ?? ""),
                new Claim("user_role", role.Role.ToString() ?? ""),
                new Claim("tenant_id", user.TenantId.ToString() ?? ""),
            };
        }
    }
}
