using System.Web.Mvc;
using System.Web.Routing;

public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        // Route for the Home controller
        routes.MapRoute(
            name: "Home",
            url: "home",
            defaults: new { controller = "Home", action = "Home" }
        );

        // Route for the Login controller
        routes.MapRoute(
            name: "ProfileRoute",
            url: "profile",
            defaults: new { controller = "Profile", action = "ProfileIndex" }
        );
        
        routes.MapRoute(
            name: "SignInRoute",
            url: "signin",
            defaults: new { controller = "Account", action = "SignIn" }
        );
        routes.MapRoute(
            name: "SignUpRoute",
            url: "signup",
            defaults: new { controller = "Account", action = "SignUp" }
        );
        routes.MapRoute(
            name: "AboutUsRoute",
            url: "AboutUs",
            defaults: new { controller = "AboutUs", action = "AboutUsIndex" }
        );
        routes.MapRoute(
            name: "ContactRoute",
            url: "Contact",
            defaults: new { controller = "Contact", action = "ContactIndex" }
        );
        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "home", id = UrlParameter.Optional }
        );
    }
}