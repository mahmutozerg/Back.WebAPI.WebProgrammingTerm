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
        
        routes.MapRoute(
            name: "AdminRoute",
            url: "admin",
            defaults: new { controller = "Admin", action = "Index" }
        );
        
        routes.MapRoute(
            name: "AdminProductsRoute",
            url: "admin/products",
            defaults: new { controller = "Admin", action = "Products" }
        );
        routes.MapRoute(
            name: "AdminProductUpdateRoute",
            url: "products/update/{id}",
            defaults: new { controller = "Admin", action = "UpdateProduct" }
        );
        routes.MapRoute(
            name: "AdminProductDeleteRoute",
            url: "products/delete/{id}",
            defaults: new { controller = "Admin", action = "DeleteProduct" }
        );
        routes.MapRoute(
            name: "AdminUserDeleteRoute",
            url: "admin/User/delete/{id}",
            defaults: new { controller = "Admin", action = "DeleteUser" }
        );

        routes.MapRoute(
            name: "AdminUsersRoute",
            url: "admin/users",
            defaults: new { controller = "Admin", action = "Users" }
        );
        
        routes.MapRoute(
            name: "AdminUserUpdateRoute",
            url: "admin/user/update/{id}",
            defaults: new { controller = "Admin", action = "UpdateUser" }
        );

        routes.MapRoute(
            name: "CompanyRoute",
            url: "company",
            defaults: new { controller = "Company", action = "Index" }
        );
        routes.MapRoute(
            name: "CompanyProductsRoute",
            url: "company/products",
            defaults: new { controller = "Company", action = "Products" }
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
            defaults: new { controller = "ErrorPage", action = "Products" }
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
            name: "ProductSearchByCategoryRoute",
            url: "product/SearchByCategory/{searchTerm}",
            defaults: new { controller = "Product", action = "SearchByCategory" }
        );
        routes.MapRoute(
            name: "AddToCartRoute",
            url: "cart/add",
            defaults: new { controller = "Cart", action = "AddToCart" }
        );
        routes.MapRoute(
            name: "CartPage",
            url: "cart/checkout",
            defaults: new { controller = "Cart", action = "Index" }
        );
        
        routes.MapRoute(
            name: "CartDelete",
            url: "cart/delete/{id}",
            defaults: new { controller = "Cart", action = "Remove" }
        );
        
        routes.MapRoute(
            name: "Orders",
            url: "orders/MyOrders",
            defaults: new { controller = "Cart", action = "MyOrders" }
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