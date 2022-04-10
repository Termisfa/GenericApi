using ForexBot.Lib.Helpers.Extensions;
using GenericApi.Authorization;
using MySqlDatabase.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDatabaseContext();
builder.Services.AddOtherServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IResetConnections>().Initialize();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();