using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using DataAccess.Entities;
using ShopMVC;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using ShopMVC.Services;
using NuGet.Protocol.Core.Types;
using DataAccess.Interfaces;
using DataAccess;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
 
//get connection string
string connection = builder.Configuration.GetConnectionString("ShopMVCConnection") ?? throw new InvalidOperationException("Connection string 'ShopMVCConnection' not found.");
//add contect WebAppLibraryContext as service by application
builder.Services.AddDbContext<ShopMVCDbContext>(options =>
{
    options.UseSqlServer(connection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); //for 
});

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ShopMVCDbContext>();

//add Fluent Validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

//add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.Name = "_ShopMVC.Session";
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;  
});
//The built-in IoC container supports three kinds of lifetimes:
//-Singleton: IoC container will create and share a single instance
//            of a service throughout the application's lifetime.
//-Transient: The IoC container will create a new instance of the specified
//            service type every time you ask for it.
//- Scoped: IoC container will create an instance of the specified service
//          type once per request and will be shared in a single request.
//di remote services
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFileService, FileService>();

//add IRepositore for all Entities

//builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
//builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
// OR
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//Add auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddHttpContextAccessor();

//var services = builder.Services;
var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    Seeder.SeedRoles(serviceProvider).Wait();
    Seeder.SeedAdmin(serviceProvider).Wait();   
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
