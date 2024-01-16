using AspNetCoreHero.ToastNotification;
using LapTopStore_Client.Models.VNPAY;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using LapTopStore_Computer.MyInterface;
using LapTopStore_Computer.Repository;
using LapTopStore_Computer.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddNotyf(config => { 
    config.DurationInSeconds = 10; 
    config.IsDismissable = true; 
    config.Position = NotyfPosition.TopRight; 
});

builder.Services.AddDbContext<LapTopStoreContext>(options =>
{
    var connectStr = builder.Configuration.GetConnectionString("LapTopStore");
    options.UseSqlServer(connectStr);
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Customer/RegisterLogin";
    options.LogoutPath = "/Customer/Logout";
    options.AccessDeniedPath = "/Customer/AccessDenied";
});

builder.Services.Configure<VnPaySettings>(configuration.GetSection("VnPaySettings"));

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

app.Run();
