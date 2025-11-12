using cubets_core.Common;
using cubets_core.Data;
using cubets_core.Helpers;
using cubets_core.Hubs;
using cubets_core.Modules.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// EF Core / MySQL
builder.Services.AddDbContext<CubetsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))
    ));
builder.Services.AddScoped<IResponseService, ResponseService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
// SignalR
builder.Services.AddSignalR();

// Controllers
builder.Services.AddControllers();

// OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer(); // wajib sebelum builder.Build()
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI hanya di Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // JSON OpenAPI
    app.UseSwaggerUI();    // Swagger UI di /swagger
}

// Middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Redirect root "/" ke Swagger UI
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

// Endpoint Controller dan Hub
app.MapControllers();
app.MapHub<GameHub>("/gamehub");

app.Run();
