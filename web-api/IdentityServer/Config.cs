using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResource(
                    name: "profile",
                    userClaims: new[] { "name", "email", "website"},
                    displayName: "Your profile data"
                )
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { 
                new ApiScope(name: "customer.client", displayName: "Library API")
            };


    public static IEnumerable<Client> Clients =>
        new Client[] 
            {
                new Client
                {
                    ClientId = "service.client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                },
            };
}