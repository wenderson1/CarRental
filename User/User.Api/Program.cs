using User.Infrastructure;
using User.Application;
using User.Api.Filter;
using FluentValidation.AspNetCore;
using User.Application.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRabbitMq()
                .AddMongo()
                .AddRepositories()
                .AddServices()
                .AddSubscriber();

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

builder.Services.AddValidatorsFromAssemblyContaining<CarInputValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
