using Asp.Versioning;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository;
using Serilog;
using Services;
using System.Reflection;
using WebApi.Mappers;

/// <summary>
/// Configures the logger configuration.
/// </summary>
Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("Logs/log.txt").CreateLogger();

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Starts the logger
/// </summary>
Log.Information("Starting up!");

/// <summary>
/// Adds serilog to the services
/// </summary>
builder.Services.AddSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

/// <summary>
/// Adds controllers and NewtonsoftJson to the container.
/// </summary>
builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true; //Accetto solo il formato json
    })
    .AddNewtonsoftJson(options => 
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

builder.Services.AddEndpointsApiExplorer();

/// <summary>
/// Adds SwaggerGen using documentation.
/// </summary>
builder.Services.AddSwaggerGen( setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    var dtoAssembly = Assembly.Load("DTOs");
    if(dtoAssembly != null)
    {
        var dtoCommentsFile = $"{dtoAssembly.GetName().Name}.xml";
        var dtoXmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, dtoCommentsFile);

        setupAction.IncludeXmlComments(dtoXmlCommentsFullPath);
    }

    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddProblemDetails(); //Logging exceptions

/// <summary>
/// Adds DBContext to the services.
/// </summary>
var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(
        _connectionString,
        b => b.MigrationsAssembly("WebApi") //Specifies the assembly where migrations are saved
    )
);

/// <summary>
/// Adds the repositories and booking services to the services.
/// </summary>
builder.Services.AddScoped<IExtendedRepository<Book>, ExtendedRepository<Book, LibraryContext>>();
builder.Services.AddScoped<IExtendedRepository<Booking>, ExtendedRepository<Booking, LibraryContext>>();
builder.Services.AddScoped<IBookService, BookService>();

/// <summary>
/// Adds the AutoMapper to the services.
/// </summary>
builder.Services.AddAutoMapper(typeof(MapperProfile));

/// <summary>
/// Adds the ApiVersioning to the services.
/// </summary>
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
