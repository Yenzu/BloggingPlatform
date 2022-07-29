using Microsoft.EntityFrameworkCore;
using Auth0.AspNetCore.Authentication;
using BloggingPlatform.DataService.Models;
using BloggingPlatform.DataService.Interfaces;
using BloggingPlatform.BusinessService;
using BloggingPlatform.DataService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BloggingPlatformContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BloggingPlatformContext") ?? throw new InvalidOperationException("Connection string 'BloggingPlatformContext' not found.")));

//Services configuration
builder.Services.AddScoped<IPostService, PostService>();

//External Services configuration
builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.Scope = "openid profile email";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddResponseCaching();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
