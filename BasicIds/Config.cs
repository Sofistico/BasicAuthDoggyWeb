// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace BasicIds
{
    public static class Config
    {
        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                     SubjectId = "1",
                     Username = "Dog",
                     Password = "dog",
                     Claims =
                     {
                        new Claim(JwtClaimTypes.Name, "Dog dog"),
                        new Claim(JwtClaimTypes.GivenName, "Dog"),
                        new Claim(JwtClaimTypes.FamilyName, "da silva"),
                        new Claim(JwtClaimTypes.WebSite, "https://www.google.com.br"),
                     }
              }
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("read"),
                new ApiScope("write"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
               new ApiResource("doggy.api")
               {
                   Scopes = new List<string>{ "read","write" },
                   ApiSecrets = new List<Secret>{ new Secret("secret".Sha256()) }
               }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "doggy.api",
                    ClientName = "Client Credentials Client for API",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "read" }
                },
            };
    }
}
