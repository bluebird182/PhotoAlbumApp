using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;  
using Microsoft.EntityFrameworkCore;
using PhotoAlbumApp.Data;
using PhotoAlbumApp.Logic;
using PhotoAlbumApp.Models;
using System;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services
    .AddDbContext<PhotoDbContext>(options =>
    options.UseSqlite("Data Source=photos.db"));

//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<PhotoDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddAuthorization();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PhotoDbContext>();
    db.Database.Migrate(); // Apply migrations automatically
}

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Photos}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PhotoDbContext>();
    db.Database.EnsureCreated(); // auto-create db

    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();

    if (!db.Users.Any())
    {
        var user = new User
        {
            Username = "test",
            Password = "1234"
        };
        user.PasswordHash = hasher.HashPassword(user, "1234");
        db.Users.Add(user);
        db.SaveChanges();
    }
}


app.Run();
