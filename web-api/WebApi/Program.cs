using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
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
).AddMvc()
.AddApiExplorer(setupAction =>
{
    setupAction.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

/// <summary>
/// Adds SwaggerGen using documentation.
/// </summary>
var apiVersionDescriptionProvider = builder.Services.BuildServiceProvider()
  .GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen( setupAction =>
{
    foreach (var description in
        apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        setupAction.SwaggerDoc(
            $"{description.GroupName}",
            new()
            {
                Title = "Booking Web API",
                Version = description.ApiVersion.ToString(),
                Description = "Through this API you can access book and booking one of it."
            });
    }

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(); //Add exceptions handler
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            setupAction.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
