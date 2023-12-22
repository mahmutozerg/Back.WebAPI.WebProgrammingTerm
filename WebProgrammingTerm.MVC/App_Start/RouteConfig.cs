﻿using System.Web.Mvc;
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

        routes.MapRoute(
            name: "ProfileRoute",
            url: "profile",
            defaults: new { controller = "Profile", action = "ProfileIndex" }
        );
        routes.MapRoute(
            name: "AddressConfig",
            url: "profile/adresses",
            defaults: new { controller = "Address", action = "Addresses" }
        );
        routes.MapRoute(
            name: "NotFoundPageRoute",
            url: "notfound",
            defaults: new { controller = "ErrorPage", action = "Index" }
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
            name: "ProductPage",
            url: "product/productsId/{id}",
            defaults: new { controller = "Product", action = "Index" }
        );
        routes.MapRoute(
            name: "ProductSearchRoute",
            url: "product/search/{searchTerm}",
            defaults: new { controller = "Product", action = "Search" }
        );
        routes.MapRoute(
            name: "AddToCartRoute",
            url: "cart/add",
            defaults: new { controller = "Cart", action = "AddToCart" }
        );
        
        routes.MapRoute(
            name: "AddToFavoritesPage",
            url: "Favorites",
            defaults: new { controller = "Favorites", action = "Index" }
        );
        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "home", id = UrlParameter.Optional }
        );
    }
}