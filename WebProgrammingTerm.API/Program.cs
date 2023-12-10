using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebProgrammingTerm.API.AuthRequirements;
using WebProgrammingTerm.API.RequirementHandlers;
using WebProgrammingTerm.Auth.Service.Services;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;
using WebProgrammingTerm.Repository;
using WebProgrammingTerm.Repository.Repositories;
using WebProgrammingTerm.Repository.UnitOfWorks;
using WebProgrammingTerm.Service.Configurations;
using WebProgrammingTerm.Service.Services;
using UserService = WebProgrammingTerm.Service.Services.UserService;

var builder = WebApplication.CreateBuilder(args);
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<AppTokenOptions>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(WebProgrammingTerm.Service.Services.GenericService<>));

builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

builder.Services.AddScoped(typeof(ICompanyService), typeof(CompanyService));
builder.Services.AddScoped(typeof(ICompanyRepository), typeof(CompanyRepository));

builder.Services.AddScoped(typeof(ICompanyUserService), typeof(CompanyUserService));
builder.Services.AddScoped(typeof(ICompanyUserRepository), typeof(CompanyUserRepository));

builder.Services.AddScoped(typeof(IDepotService), typeof(DepotService));
builder.Services.AddScoped(typeof(IDepotRepository), typeof(DepotRepository));

builder.Services.AddScoped(typeof(IProductDetailService), typeof(ProductDetailService));
builder.Services.AddScoped(typeof(IProductDetailRepository), typeof(ProductDetailRepository));

builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));


builder.Services.AddScoped(typeof(ILocationService), typeof(LocationService));
builder.Services.AddScoped(typeof(ILocationRepository), typeof(LocationRepository));

builder.Services.AddScoped(typeof(IUserFavoriteService), typeof(UserFavoriteService));
builder.Services.AddScoped(typeof(IUserFavoritesRepository), typeof(UserFavoritesRepository));

builder.Services.AddScoped(typeof(IUserCommentService), typeof(UserCommentService));
builder.Services.AddScoped(typeof(IUserCommentRepository), typeof(UserCommentRepository));

builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidateIssuer = true,
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidAudience = tokenOptions.Audience[0],
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        RoleClaimType = ClaimTypes.Role,
        
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthServerPolicy", policy =>
        policy.Requirements.Add(new ClientIdRequirement("authserver")));    
    
    options.AddPolicy("AdminBypassAuthServerPolicy", policy =>
        policy.Requirements.Add(new AdminClientIdBypassRequirement("authserver")));
    options.AddPolicy("JSClientPolicy", policy =>
        policy.Requirements.Add(new ClientIdRequirement("jsclient")));
    
});
builder.Services.AddSingleton<IAuthorizationHandler, ClientIdRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminClientIdBypassRequirementHandler>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();