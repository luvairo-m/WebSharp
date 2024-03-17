using AchievementServiceConnectionLib.Extensions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Extensions;
using Presentation.Controllers;
using Services.Extensions;
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

builder.Services.AddAchievementConnectionLogic();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddUserRepositoryWithManager();
builder.Services.AddUserAndExperienceServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();