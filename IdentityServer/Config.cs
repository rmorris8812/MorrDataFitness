// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Fitness.Database.Api;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };

        public static IEnumerable<Client> Clients = CreateHardCodedClients();

        public static IEnumerable<Client> CreateHardCodedClients()
        {
            return new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("4378F4f23r2u!foe$ru45rY".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AllowOfflineAccess = true
                }
            };
        }
        public static IEnumerable<Client> CreateClients()
        {
            var clients = new List<Client>();
            /*using var databaseApi = new DatabaseApi();
            var identityClientsResponse = databaseApi.GetIdentityClientsAsync(CancellationToken.None).Result;
            if (identityClientsResponse.Count == 0)
                return clients;

            foreach (var identityClient in identityClientsResponse.Entities)
            {
                // Get the scopes from the database and convert to a string collection
                var scopes = new List<string>();
                var identityScopesResponse = databaseApi.GetIdentityScopesAsync(identityClient.Id, CancellationToken.None).Result;
                if (identityScopesResponse.Count != 0)
                {
                    foreach (var identityScope in identityScopesResponse.Entities)
                    {
                        scopes.Add(identityScope.Object.Name);
                    }
                }

                // Get the redirects from the database and convert to a string collection
                var redirects = new List<string>();
                var identityRedirectsResponse = databaseApi.GetIdentityRedirectsAsync(identityClient.Id, CancellationToken.None).Result;
                if (identityRedirectsResponse.Count != 0)
                {
                    foreach (var identityRedirect in identityRedirectsResponse.Entities)
                    {
                        redirects.Add(identityRedirect.Object.Uri);
                    }
                }

                // Get the logout redirects from the database and convert to a string collection
                var logoutRedirects = new List<string>();
                var identityLogoutRedirectsResponse = databaseApi.GetIdentityLogoutRedirectsAsync(identityClient.Id, CancellationToken.None).Result;
                if (identityLogoutRedirectsResponse.Count != 0)
                {
                    foreach (var identityRedirect in identityLogoutRedirectsResponse.Entities)
                    {
                        logoutRedirects.Add(identityRedirect.Object.Uri);
                    }
                }
                // Create the client and add to the list
                var client = new Client()
                {
                    ClientId = identityClient.Object.ClientId,
                    ClientSecrets = { new Secret(identityClient.Object.ClientSecret.Sha256()) },
                    AllowedGrantTypes = new List<string>() { identityClient.Object.GrantType },
                    AllowedScopes = scopes,
                    RedirectUris = redirects,
                    RequireConsent = false,
                    RequirePkce = !identityClient.Object.ClientId.Equals("client"),
                    AllowOfflineAccess = true,
                    PostLogoutRedirectUris = logoutRedirects
                };
                clients.Add(client);
            }*/
            return clients;
        }
    }
}