using ForexBot.Lib.Helpers.Extensions;
using MySqlDatabase.Helpers;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Add(new ServiceDescriptor(typeof(ConstantsContext), new ConstantsContext(builder.Configuration.GetConnectionString("DefaultConnection"))));
//builder.Services.Add(new ServiceDescriptor(typeof(TestContext), new TestContext(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddDatabaseContext();
builder.Services.AddOtherServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IResetConnections>().Initialize();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
