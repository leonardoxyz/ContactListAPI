using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using volxyseat.Domain.Core.Data;
using volxyseat.Domain.Models;
using volxyseat.Domain.Services;
using volxyseat.Infrastructure.Data;
using Volxyseat.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();

var connectionString = configuration.GetConnectionString("Homologation");
builder.Services.AddDbContext<DataContext>(opts =>
    opts.UseSqlite(connectionString));

builder.Services.AddScoped<IRepository<Contact, Guid>, ContactRepository>();
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
builder.Services.AddScoped<ContactService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:5173") // Remova a barra final
            .AllowAnyHeader()
            .AllowAnyMethod());
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "VOLXYSEAT-API", Version = "v1" });
    });

    builder.Services.AddSwaggerGen();

    builder.Services.AddEndpointsApiExplorer();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin"); // Aplicar a política de CORS aqui

app.MapControllers();

app.Run();
