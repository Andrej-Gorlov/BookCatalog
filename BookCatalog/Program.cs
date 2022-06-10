using AutoMapper;
using BookCatalog;
using BookCatalog.DAL;
using BookCatalog.DAL.Interfaces;
using BookCatalog.DAL.Repository;
using BookCatalog.Middleware.Extensions;
using BookCatalog.Service.Implementations;
using BookCatalog.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
// Add services to the container.

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HomeBookkeepingWebApi",
        Description = "Пример Web API (Домашняя бухгалтерия).",
        TermsOfService = new Uri("https://yandex.ru"),
        Contact = new OpenApiContact
        {
            Name = "Горлов Андрей",
            Email = "avgorlov899@gmail.com",
            Url = new Uri("https://github.com/Andrej-Gorlov?tab=repositories")
        },
        License = new OpenApiLicense
        {
            Name = "Лицензия...",
            Url = new Uri("https://yandex.ru")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseErrorHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var dbContext = services.GetRequiredService<ApplicationDbContext>();
//        if (dbContext.Database.IsSqlServer())
//        {
//            dbContext.Database.Migrate();
//        }
//    }
//    catch (Exception ex)
//    {
//        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

//        throw;
//    }
//}

app.MapControllers();

app.Run();
