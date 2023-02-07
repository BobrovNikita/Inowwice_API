using FridgeProducts;
using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services;
using FridgeProducts.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlServer(connection);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

builder.Services.AddScoped<IRepository<FridgeModel>, FridgeModelsRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductsRepository>();
builder.Services.AddScoped<IRepository<Fridge>, FridgeRepository>();
builder.Services.AddScoped<IRepository<FridgeProducts.Models.FridgeProducts>, FridgeProductsRepository>();
builder.Services.AddScoped<IFridgeModelsService, FridgeModelService>();
builder.Services.AddScoped<IProductsService, ProductService>();
builder.Services.AddScoped<IFridgeProductsService, FridgeProductsService>();
builder.Services.AddScoped<IFridgeService, FridgeService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();