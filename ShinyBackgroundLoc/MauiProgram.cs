using Microsoft.Extensions.Logging;
using Shiny;

namespace ShinyBackgroundLoc;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseShiny()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddGps<Services.Delegates.GpsDelegate>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<PermissonPage>();

        return builder.Build();
	}
}

