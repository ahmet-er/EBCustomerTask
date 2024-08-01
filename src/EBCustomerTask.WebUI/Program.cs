using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Application.Mappings;
using EBCustomerTask.Application.Validators;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Infrastructure.Data;
using EBCustomerTask.Infrastructure.Identity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using var serviceScope = app.Services.CreateScope();
var serviceProvider = serviceScope.ServiceProvider;
DataSeed.SeedAsync(serviceProvider).Wait();

app.Run();
