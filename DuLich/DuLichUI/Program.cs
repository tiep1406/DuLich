using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Identity;
using RestEase.HttpClientFactory;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("example")
    .ConfigureHttpClient(x => x.BaseAddress = new Uri(configuration["BaseAddress"] + "/api/"))
    .UseWithRestEaseClient<IAuthAPI>()
    .UseWithRestEaseClient<ITourAPI>()
    .UseWithRestEaseClient<IDestinationAPI>()
    .UseWithRestEaseClient<IRestaurantAPI>()
    .UseWithRestEaseClient<IHotelAPI>()
    .UseWithRestEaseClient<IDeliveryAPI>()
    .UseWithRestEaseClient<IUserAPI>();

builder.Services
    .AddAuthentication("UserAuth")
    .AddCookie("UserAuth", options =>
    {
        options.LoginPath = "/home";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.AccessDeniedPath = "/home";
        options.LogoutPath = "/home/logout";
        options.Cookie.Name = "User";
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
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
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();