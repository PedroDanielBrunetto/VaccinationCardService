using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.People.Create;
using VaccinationCard.Application.Vaccinations.Create;
using VaccinationCard.Application.Vaccines.Create;
using VaccinationCard.Application.People.Queries;
using VaccinationCard.Application.Vaccinations.Queries;
using VaccinationCard.Application.Vaccines.Queries;
using VaccinationCard.Application.People.Delete;
using VaccinationCard.Application.Vaccinations.Delete;
using VaccinationCard.Application.Vaccines.Delete;
using VaccinationCard.Api.Middlewares;
using VaccinationCard.Application.Abstractions.Persistence;
using VaccinationCard.Application.Abstractions.Messaging;
using VaccinationCard.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database setup
var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

builder.Services.AddDbContext<VaccinationDbContext>(options =>
{
    options.UseSqlite(connection);
});

// Application services
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreatePersonCommand).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(CreatePersonCommandValidator).Assembly);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateVaccinationCommand).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(CreateVaccinationCommandValidator).Assembly);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateVaccineCommand).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(CreateVaccineCommandValidator).Assembly);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(DeletePersonCommand).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(DeleteVaccinationCommand).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(DeleteVaccineCommand).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllPersonsQuery).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetVaccinationCardQuery).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllVaccinesQuery).Assembly));

builder.Services.AddFluentValidationAutoValidation();

// IoC
builder.Services.AddScoped<IVaccinationDbContext>(
    provider => provider.GetRequiredService<VaccinationDbContext>());

builder.Services.AddScoped<IMessageBus, FakeMessageBus>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VaccinationDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
