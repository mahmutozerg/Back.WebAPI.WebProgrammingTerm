using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebProgrammingTerm.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
    protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
    {
        // Configure Google authentication
        var authenticationOptions = new GoogleOAuth2AuthenticationOptions
        {
            ClientId = "YOUR_CLIENT_ID",
            ClientSecret = "YOUR_CLIENT_SECRET",
            SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
            CallbackPath = new PathString("/Account/ExternalLoginCallback"),
            Provider = new GoogleOAuth2AuthenticationProvider
            {
                OnAuthenticated = context =>
                {
                    // Handle the authentication result and create claims
                    var email = context.Identity.FindFirstValue(ClaimTypes.Email);
                    // Add additional claims as needed

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, context.Identity.FindFirstValue(ClaimTypes.Name)),
                        new Claim(ClaimTypes.Email, email),
                        // Add additional claims as needed
                    };

                    context.Identity.AddClaims(claims);
                    return Task.CompletedTask;
                }
            }
        };

        app.UseCookieAuthentication(new CookieAuthenticationOptions
        {
            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            LoginPath = new PathString("/Account/Login"),
        });

        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        app.UseGoogleAuthentication(authenticationOptions);
    }
}