using Microsoft.EntityFrameworkCore;
using WebAPIProducto.data;

var builder = WebApplication.CreateBuilder(args);//constructor

// Add services to the container.
//para agregar las independencias para la conexión
//en UseSqlite()le entra la cadena de conexión a la BD
var cadenaConexion = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options=>options.UseSqlite(cadenaConexion));

builder.Services.AddControllers();//servicios
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();//construcción de la App

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();//miliwer

app.UseAuthorization();

app.MapControllers();//mapear controladores

app.Run();// correr la aplicación.
