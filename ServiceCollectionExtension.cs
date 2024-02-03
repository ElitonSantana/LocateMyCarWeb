using LocateMyCarWeb.Services.Interfaces;
using LocateMyCarWeb.Services;

namespace LocateMyCarWeb
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IVehicleService, VehicleService>();
        }
    }
}
