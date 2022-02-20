using GenericApi.ApiRestHandler;
using MySqlDatabase.Handlers;

namespace ForexBot.Lib.Helpers.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services)
        {
            _ = services.AddTransient<IConnectionsHandler, ConnectionsHandler>();
        }

        public static void AddOtherServices(this IServiceCollection services)
        {
            _ = services.AddTransient<IQuerysHandler, QuerysHandler>();
            _ = services.AddTransient<IApiRestHandler, ApiRestHandler>();
        }

    }
}
