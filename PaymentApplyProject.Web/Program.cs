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
using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Middlewares;
using PaymentApplyProject.Infrastructure.Hubs;
using PaymentApplyProject.Application.Context;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Infrastructure.Services.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

//// hint: default TR culture
//var cultureInfo = new CultureInfo("tr-TR");
//cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
//cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
//CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
//CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
//builder.Services.AddRequestLocalization(options =>
//{
//    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo);
//    options.SupportedCultures = new[] { cultureInfo };
//    options.SupportedUICultures = new[] { cultureInfo };
//});

// Add services to the container.
builder.Services.AddControllersWithViews()
.AddRazorRuntimeCompilation()
.AddNewtonsoftJson();

builder.Services.AddHttpContextAccessor();

builder.Services.AddResponseCaching();

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 102400000;
});

builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterPersistence(builder.Configuration);

builder.Services.AddAuthorizationBuilder().AddPolicy("background_service", policy => policy.RequireRole("admin"));

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
    endpoints.MapHub<DepositPaymentHub>("/depositpayment");
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapGet("/background", (DepositRequestControlBackgroundService service) =>
{
    return new DepositRequestControlBackgroundServiceState(service.IsEnabled);
});

app.MapMethods("/background", new[] { "PATCH" }, (DepositRequestControlBackgroundServiceState state, DepositRequestControlBackgroundService service) =>
{
    service.IsEnabled = state.IsEnabled;
}).RequireAuthorization("background_service");

app.Run();
