using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new("Catalog.API", "Catalog API"),
                new("Discount.API", "Discount API"),
                new("read", "Read your data"),
                new("write", "Write your data"),
                new("delete", "Delete your data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new()
                {
                    ClientId = "postman",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "Catalog.API",
                        "Discount.API"
                    }
                },
                new()
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://<client-url-base>/signin-oidc" },
                    PostLogoutRedirectUris = { "https://<client-url-base>/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "Catalog.API",
                        "Discount.API"
                    }
                }
            };
    }
}