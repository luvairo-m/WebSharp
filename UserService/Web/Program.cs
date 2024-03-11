using Domain.Repository;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;
using Presentation.Controllers;
using Services;
using Services.Abstractions;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
        options.ReturnHttpNotAcceptable = true;
        options.RespectBrowserAcceptHeader = true;
    })
    .AddApplicationPart(typeof(UserController).Assembly);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<UserContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("Npgsql");
    options.UseNpgsql(connection + builder.Configuration["DbPassword"],
        contextOptions => contextOptions.MigrationsAssembly("Web"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();