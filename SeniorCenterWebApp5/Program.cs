using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервис для выбора подключения
builder.Services.AddSingleton<DatabaseConnectionService2>();

// Настраиваем DbContext с использованием фабрики подключения
builder.Services.AddDbContext<DataContext>((serviceProvider, options) =>
{
    var dbService = serviceProvider.GetRequiredService<DatabaseConnectionService2>();
    var connectionString = dbService.GetConnectionString();

    if (connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase))
    {
        // Supabase (PostgreSQL)
        options.UseNpgsql(connectionString);
    }
    else
    {
        // SQL Server
        options.UseSqlServer(connectionString);
    }
});

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });


builder.Services.AddDataProtection();
builder.Services.AddScoped<PasswordResetService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();


