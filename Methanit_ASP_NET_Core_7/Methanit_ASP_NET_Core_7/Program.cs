using Methanit_ASP_NET_Core_7;
using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//DbContext
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlServer(connection);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });



//Repositories
builder.Services.AddScoped<IRepository<Fridge_Model>, Fridge_Models_Repository>();
builder.Services.AddScoped<IRepository<Products>, ProductsRepository>();
builder.Services.AddScoped<IRepository<Fridge>, FridgeRepository>();
builder.Services.AddScoped<IRepository<Fridge_Products>, FridgeProductsRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();