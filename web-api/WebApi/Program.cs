using Asp.Versioning;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository;
using Serilog;
using Services;
using WebApi.Mappers;


//TODO:
//Aggiungere un logger Serilog
//Per il monitoraggio 
Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("Logs/log.txt").CreateLogger();
var builder = WebApplication.CreateBuilder(args);
Log.Information("Starting up!");
builder.Services.AddSerilog();//Aggiunta del serilog nel servizio

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true; //Accetto solo il formato json
    })
    .AddNewtonsoftJson(options => 
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore //Ignoro i reference loops
        ); //Aggiungo il supporto per il json .net
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails(); //Logging exceptions

// Add DBContext
var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(
        _connectionString,
        b => b.MigrationsAssembly("WebApi") //Specifico l'assembly per le migrazioni
    )
);

builder.Services.AddScoped<IExtendedRepository<Book>, ExtendedRepository<Book>>();
builder.Services.AddScoped<IExtendedRepository<Booking>, ExtendedRepository<Booking>>();
builder.Services.AddScoped<IBookService, BookService>();

//Aggiunta di automapper al servizio
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddApiVersioning(setupAction =>
    {
        setupAction.ReportApiVersions = true;
        setupAction.AssumeDefaultVersionWhenUnspecified = true;
        setupAction.DefaultApiVersion = new ApiVersion(1, 0);
    }
).AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(); //Add exceptions handler
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
