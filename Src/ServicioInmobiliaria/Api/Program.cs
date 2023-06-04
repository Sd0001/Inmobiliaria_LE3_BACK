using Inmobiliaria.Api.Controllers;
using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDatos<Imagen>, ImagenData>();
builder.Services.AddScoped<IDatos<Inmueble>, InmuebleData>();
builder.Services.AddScoped<IDatos<Oferta>, OfertaData>();
builder.Services.AddScoped<IDatos<Persona>, PersonaData>();
builder.Services.AddScoped<IDatos<Sucursal>, SucursalData>();
builder.Services.AddScoped<IDatos<TipoInmueble>, TipoInmuebleData>();
builder.Services.AddScoped<IDatos<Transaccion>, TransaccionData>();
builder.Services.AddScoped<IDatos<Visita>, VisitaData>();
builder.Services.AddScoped<IDatosRead<Estado>, EstadoData>();
builder.Services.AddScoped<IDatosRead<TipoTransaccion>, TipoTransaccionData>();


var connectionString = "";

connectionString = builder.Configuration.GetConnectionString("Pruebas");

builder.Services.AddDbContext<InmobiliariaContext>(
       options => options.UseSqlServer(connectionString));

builder.Services.AddControllers(); 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Inmobiliaria API",
        Description = "Taller para materia Linea de �nfasis 3 universidad Remington", 
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


var dbConfig = new DbConfig();
dbConfig.ConnectionString = connectionString??"";
builder.Services.AddSingleton<DbConfig>(dbConfig);

builder.Services.AddOptions();
var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 
app.UseSwagger();
app.UseSwaggerUI(
//    options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//    options.RoutePrefix = string.Empty;
//}
);

app.Run();
