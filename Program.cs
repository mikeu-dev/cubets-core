using cubets_core.Data;
using cubets_core.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core / MySQL
builder.Services.AddDbContext<CubetsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))
    ));

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
