using Microsoft.Extensions.Logging;
using EnergyControlMaui;
using EnergyControlMaui.Services;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EnergyControlMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            string connectionString = "Host=localhost;Port=5432;Database=User;Username=postgres;Password=4321";

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(_ => connectionString);
            builder.Services.AddSingleton<AppDbContext>(_ => new AppDbContext(connectionString));
            builder.Services.AddSingleton<UserManager>();
            builder.Services.AddSingleton<WelcomePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
