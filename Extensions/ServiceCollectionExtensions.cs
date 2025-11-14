using cubets_core.Common;
using cubets_core.Helpers;
using cubets_core.Modules.Auth.Services;
using cubets_core.Modules.Player.Services;
using cubets_core.Modules.Score.Services;

namespace CubetsCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<PlayerService>();
            services.AddScoped<ScoreService>();

            return services;
        }
    }
}
