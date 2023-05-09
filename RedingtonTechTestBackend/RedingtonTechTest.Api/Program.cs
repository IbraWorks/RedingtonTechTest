using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using RedingtonTechTest.Api.Commands;
using RedingtonTechTest.Api.Strategy;
using RedingtonTechTest.Api.Validators;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) =>
{
    //NOTE: the filter is just to make the logs easier to read for the assignment
    loggerConfig.WriteTo.File("./logs.txt", rollingInterval: RollingInterval.Day)
    .Filter.ByIncludingOnly(x => x.MessageTemplate.Text.Contains("Using calc strategy"));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
        //.WithOrigins("http://localhost:5173");
    });
});

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<RedingtonProbabilityPairValidator>();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<ICalculationStrategyFactory, CalculationStrategyFactory>();



var app = builder.Build();

app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
