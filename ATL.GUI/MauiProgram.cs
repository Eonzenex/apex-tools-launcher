﻿using ATL.GUI.Services;
using ATL.GUI.Services.App;
using ATL.GUI.Services.Development;
using ATL.GUI.Services.Game;
using ATL.GUI.Services.Mod;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace ATL.GUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration = new SnackbarConfiguration
            {
                SnackbarVariant = Variant.Outlined,
                PositionClass = Defaults.Classes.Position.BottomLeft,
                VisibleStateDuration = 5 * 1000,
                ShowTransitionDuration = 150,
                HideTransitionDuration = 300
            };
        });

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        builder.Services.AddScoped<ILogService, LogService>();
        builder.Services.AddScoped<IAppStateService, AppStateService>();
        
        builder.Services.AddScoped<AppConfigService>();
        builder.Services.AddScoped<IGameConfigService, GameConfigService>();
        builder.Services.AddScoped<IProfileConfigService, ProfileConfigService>();
        builder.Services.AddScoped<IModConfigService, ModConfigService>();
        
        builder.Services.AddScoped<LaunchGameService>();
        
        return builder.Build();
    }
}