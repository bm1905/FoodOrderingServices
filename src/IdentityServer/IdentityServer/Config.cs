using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new()
                {
                    ClientId = "test_1_client",
                    ClientName = "Test 1 Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "Catalog.API",
                        "roles"
                    }
                },
                new()
                {
                    ClientId = "postman_client",
                    ClientName = "Postman Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    IdentityTokenLifetime = 300,
                    AccessTokenLifetime = 300,
                    RequireConsent = false,
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "http://localhost:5010/signin-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "Catalog.API",
                        "roles"
                    }
                }
            };
        
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new("roles", "Your role(s)", new List<string>() { "role" })
            };
        
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new("Catalog.API", "Catalog API")
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new()
            };
    }
}
