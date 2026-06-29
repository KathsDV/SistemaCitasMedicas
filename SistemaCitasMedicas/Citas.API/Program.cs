using Citas.Application;
using Citas.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔓 1. CONFIGURACIÓN DE CORS (Al principio del builder)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 💾 Opción A: SQL Server (ACTIVADA PERFECTAMENTE)
/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICitaRepository, SqlServerCitaRepository>();*/

// 💾 Opción B: PostgreSQL (Comentada e inactiva)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
builder.Services.AddScoped<ICitaRepository, PostgreSqlCitaRepository>();

// =========================================================================
// 🎯 CORREGIDO: Eliminamos la línea duplicada que rompía la inyección de dependencias

builder.Services.AddScoped<ProgramarCitaUseCase>();
builder.Services.AddScoped<RegistrarDiagnosticoUseCase>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔓 2. ACTIVACIÓN DE CORS
app.UseCors("PermitirTodo");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🎯 CREACIÓN AUTOMÁTICA DE TABLAS EN SQL SERVER (DOCKER)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("¡Base de datos de SQL Server creada con éxito en Docker!");
    }
    catch (System.Exception ex)
    {
        Console.WriteLine($"Nota de inicialización: {ex.Message}");
    }
}

app.UseAuthorization();
app.MapControllers();
app.Run();