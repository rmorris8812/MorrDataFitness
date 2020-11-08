/*---------------------------------------------------------------------------
 * Startup.cs
 *---------------------------------------------------------------------------
 * Copyright (C) Ivanti Corporation 2020. All rights reserved.
 *
 * This file contains trade secrets of the Ivanti Corporation. No part
 * may be reproduced or transmitted in any form by any means or for any purpose
 * without the express written permission of the Ivanti Corporation.
 *---------------------------------------------------------------------------
 */
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Inject ILS User Authentication
            services.AddTransient<IProfileService, ProfileService>();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users)
                .AddProfileService<ProfileService>();

            builder.AddDeveloperSigningCredential();

            /*using var databaseApi = new DatabaseApi();
            var federatedAuthsResponse = databaseApi.GetIdentityFederatedAuthsAsync(CancellationToken.None).Result;
            foreach (var auth in federatedAuthsResponse?.Entities)
            {
                if (auth.Object.Name.ToLower().Equals("salesforce"))
                {
                    services.AddAuthentication()
                        .AddSalesforce("Salesforce", options =>
                        {
                            options.SignInScheme = auth.Object.Scheme;
                            options.ClientId = auth.Object.ClientId;
                            options.ClientSecret = auth.Object.ClientSecret;
                            options.AuthorizationEndpoint = "https://success.ivanti.com/services/oauth2/authorize";
                        });
                }
            }*/
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}