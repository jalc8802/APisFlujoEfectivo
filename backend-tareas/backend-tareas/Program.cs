using backend_tareas.Bll;
using backend_tareas.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//-----------------------
//1. ConfigureServices.
//-----------------------
builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
.AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});
//Config EF SQL Server
builder.Services.AddDbContext<AplicationDbContext>(option =>
  option.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);
builder.Services.AddScoped<FlujoEfectivo>();
//Configurar Cors permitir cualquier origen, cabecera y metodo
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
                                            data => data.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()));
//---------------------------
//2. Configure Middleware
//---------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors("AllowWebApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
