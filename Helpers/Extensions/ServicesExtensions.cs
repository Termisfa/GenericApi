using GenericApi.ApiRestHandler;
using GenericApi.Authorization;
using MySqlDatabase.Handlers;
using MySqlDatabase.Helpers;

namespace ForexBot.Lib.Helpers.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services)
        {
            _ = services.AddSingleton<IMasterConnectionHandler, MasterConnectionHandler>();
        }

        public static void AddOtherServices(this IServiceCollection services)
        {
            _ = services.AddTransient<IQuerysHandler, QuerysHandler>();
            _ = services.AddTransient<IApiRestHandler, ApiRestHandler>();

            _ = services.AddSingleton<IResetConnections, ResetConnections>();

            _ = services.AddScoped<IUserService, UserService>();
        }

    }
}
