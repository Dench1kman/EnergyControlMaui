﻿#if ANDROID
using Android.Net.Wifi;
#endif
using Microsoft.Extensions.Logging;
using EnergyControlMaui.Views;
using EnergyControlMaui.Services;
using CommunityToolkit.Maui;
using EnergyControlMaui.DB;


namespace EnergyControlMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<SqliteDbContext>();

            builder.Services.AddTransient<SignupPage>();
            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddSingleton<UserManager>();
#if ANDROID
            builder.Services.AddTransient<WifiManager>();
#endif


            var dbContext = new SqliteDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
