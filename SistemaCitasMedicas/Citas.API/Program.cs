using Citas.Application;
using Citas.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 🔌 CONFIGURACIÓN CONMUTABLE DE BASE DE DATOS (REQUERIMIENTO DEL PDF)
// =========================================================================

// Opción A: SQL Server (Descomenta esta línea y comenta la de Postgres para usar SQL Server)
  builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICitaRepository, SqlServerCitaRepository>();

// Opción B: PostgreSQL (Descomenta esta línea y comenta la de SQL Server para usar Postgres)
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
// builder.Services.AddScoped<ICitaRepository, PostgreSqlCitaRepository>();

// =========================================================================

builder.Services.AddScoped<ProgramarCitaUseCase>();
builder.Services.AddScoped<RegistrarDiagnosticoUseCase>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();