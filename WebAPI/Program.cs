using Aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using WebAPI.Middleware;

var _MyCors = "_MyCors";
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: _MyCors, builder =>
  {
    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod();
  });
});

builder.Services.AddDbContext<CursosOnlineContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMediatR(typeof(Consulta.Manejador).Assembly);
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
builder.Services.AddIdentityCore<Usuario>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ManejadorErrorMiddleware>();
if (app.Environment.IsDevelopment())
{
  // app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(_MyCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
