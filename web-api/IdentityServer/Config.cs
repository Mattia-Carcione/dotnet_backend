using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { 
                new ApiScope(name: "libraryApi", displayName: "Access to library api", userClaims: new[] { "name", "email" })
            };


    public static IEnumerable<Client> Clients =>
        new Client[] 
            {
                new Client
                {   
                    ClientId = "web.client",

                    RequirePkce = false,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "libraryApi"
                    },

                    AllowOfflineAccess = true
                },
            };
}