using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Application.Mappings;
using EBCustomerTask.Application.Services;
using EBCustomerTask.Application.Validators;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;
using EBCustomerTask.Infrastructure.Data;
using EBCustomerTask.Infrastructure.Factories;
using EBCustomerTask.Infrastructure.Identity;
using EBCustomerTask.Infrastructure.Repositories;
using EBCustomerTask.Infrastructure.Strategies;
using EBCustomerTask.Infrastructure.Strategy;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();


builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

// factory and context
builder.Services.AddScoped<ICustomerRepositoryContext, CustomerRepositoryContext>();
builder.Services.AddScoped<ICustomerRepositoryFactory, CustomerRepositoryFactory>();

// customer repository implementations
builder.Services.AddScoped<CustomerRepositoryFromSqlServer>();
builder.Services.AddScoped<CustomerRepositoryFromMongoDb>();

// customer strategy implementations
builder.Services.AddScoped<SqlServerCustomerRepositoryStrategy>();
builder.Services.AddScoped<MongoDbCustomerRepositoryStrategy>();

// customer service
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IPhotoService, PhotoService>();


builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();


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
