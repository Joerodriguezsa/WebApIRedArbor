using Microsoft.EntityFrameworkCore;
using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Registrar IConfiguration para acceso a la configuración
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registrar ConexionSQLServer con DbContextOptions
builder.Services.AddDbContext<ConexionSQLServer>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StringConnectionSqlServer")));

// Add services to the container.
builder.Services.AddTransient<IRepositoryStatus, RepositoryStatus>();
builder.Services.AddTransient<IRepositoryRole, RepositoryRole>();
builder.Services.AddTransient<IRepositoryPortal, RepositoryPortal>();
builder.Services.AddTransient<IRepositoryCompany, RepositoryCompany>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
