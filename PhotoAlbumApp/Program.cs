using Microsoft.AspNetCore.Identity;  
using Microsoft.EntityFrameworkCore;                       

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services
    .AddDbContext<PhotoDbContext>(options =>
    options.UseSqlite("Data Source=photos.db"));

//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<PhotoDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.Run();
