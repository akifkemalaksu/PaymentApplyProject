using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PaymentApplyProject.Application;
using PaymentApplyProject.Infrastructure;
using PaymentApplyProject.Persistence;
using System.Text;
using PaymentApplyProject.Application.Dtos.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Unicode;
using PaymentApplyProject.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// hint: default TR culture
var cultureInfo = new CultureInfo("tr-TR");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
builder.Services.AddRequestLocalization(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo);
    options.SupportedCultures = new[] { cultureInfo };
    options.SupportedUICultures = new[] { cultureInfo };
});

// Add services to the container.
builder.Services.AddControllersWithViews(configure =>
{
    configure.CacheProfiles.Add("SelectCache", new CacheProfile
    {
        Duration = 300,
        Location = ResponseCacheLocation.Any
    });
})
.AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();

builder.Services.AddResponseCaching();

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 102400000;
});

builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterPersistence(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notification");
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
