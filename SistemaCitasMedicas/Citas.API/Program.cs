using Citas.Application;
using Citas.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

//SQL Server
/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICitaRepository, SqlServerCitaRepository>();*/

//PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
builder.Services.AddScoped<ICitaRepository, PostgreSqlCitaRepository>();


builder.Services.AddScoped<ProgramarCitaUseCase>();
builder.Services.AddScoped<RegistrarDiagnosticoUseCase>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("PermitirTodo");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("¡Base de datos creada con éxito en Docker!");
    }
    catch (System.Exception ex)
    {
        Console.WriteLine($"Nota de inicialización: {ex.Message}");
    }
}

app.UseAuthorization();
app.MapControllers();
app.Run();