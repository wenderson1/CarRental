using RentalCompany.Api.Filter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices()
                .AddRabbitMq()
                .AddMongo()
                .AddCache()
                .AddSubscriber()
                .AddRepositories();



builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

builder.Services.AddValidatorsFromAssemblyContaining<CustomerInputValidator>();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
