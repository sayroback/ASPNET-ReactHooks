using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Dominio;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Seguridad.TokenSeguridad;
using System.Text;
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
builder.Services.AddControllers();

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Nuevo>();

// Identity
var identityBuilder = builder.Services.AddIdentityCore<Usuario>();
var identityBuilderServices = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
identityBuilderServices.AddEntityFrameworkStores<CursosOnlineContext>();
identityBuilderServices.AddSignInManager<SignInManager<Usuario>>();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IJwtGenerador, JwtGenerador>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Palabra Secreta 3242 zxc"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
{
  ValidateIssuerSigningKey = true,
  IssuerSigningKey = key,
  ValidateAudience = false,
  ValidateIssuer = false,
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  try
  {
    var userManager = services.GetRequiredService<UserManager<Usuario>>();
    var context = scope.ServiceProvider.GetRequiredService<CursosOnlineContext>();
    context.Database.Migrate();
    DataPrueba.InsertarData(context, userManager).Wait();
  }
  catch (Exception e)
  {
    var logging = services.GetRequiredService<ILogger<Program>>();
    logging.LogError(e, "Ocurrió un error en la migración");
  }
}

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

//Agregar autenticación
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
