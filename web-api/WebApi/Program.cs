using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Context;
using Factories;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository;
using Serilog;
using Services;
using System.Reflection;
using System.Security.Claims;
using WebApi.Mappers;

// <summary>
// Configures the logger configuration.
// </summary>
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

// <summary>
// Starts the logger
// </summary>
Log.Information("Starting up!");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters.ValidateAudience = false;
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "libraryApi");
        });
    });

    // <summary>
    // Adds controllers and NewtonsoftJson to the container.
    // </summary>
    builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true; //Accetto solo il formato json
        })
        .AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

    builder.Services.AddProblemDetails(); //Logging exceptions

    // <summary>
    // Adds DBContext to the services.
    // </summary>
    var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<LibraryContext>(options =>
        options.UseSqlServer(
            _connectionString,
            b => b.MigrationsAssembly("WebApi") //Specifies the assembly where migrations are saved
        )
    );

    // <summary>
    // Adds the repositories and booking services to the services.
    // </summary>
    builder.Services.AddScoped<IExtendedRepository<Book>, ExtendedRepository<Book, LibraryContext>>();
    builder.Services.AddScoped<IExtendedRepository<Booking>, ExtendedRepository<Booking, LibraryContext>>();
    builder.Services.AddTransient<IExtendedRepository<User>, ExtendedRepository<User, LibraryContext>>();

    builder.Services.AddTransient<IBookService, BookService>();
    builder.Services.AddTransient<IPremiumServiceBook, PremiumBookService>();
    builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddTransient<IFactoryService<IBookService>, BookFactoryService>();

    // <summary>
    // Adds the AutoMapper to the services.
    // </summary>
    builder.Services.AddAutoMapper(typeof(MapperProfile));

    // <summary>
    // Adds the ApiVersioning to the services.
    // </summary>
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

    // <summary>
    // Adds SwaggerGen using documentation.
    // </summary>
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
                    Description = "Through this API you can access book and booking one."
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

    // <summary>
    // Hosting serilog.
    // </summary>
    builder.Host.UseSerilog((ctx, lc) => lc
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

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

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers().RequireAuthorization();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}