using FluentValidation;
using FluentValidationExample.Models;
using FluentValidationExample.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidationExample.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FluentValidationExampleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FluentValidationExampleContext") ?? throw new InvalidOperationException("Connection string 'FluentValidationExampleContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IValidator<Person>, PersonValidator>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();