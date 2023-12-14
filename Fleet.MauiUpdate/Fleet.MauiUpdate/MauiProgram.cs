using CommunityToolkit.Maui.Maps;
using Microsoft.Extensions.Logging;

namespace Fleet.MauiUpdate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder 
                .UseMauiApp<App>()
                .UseMauiMaps()
                .UseMauiCommunityToolkitMaps("m1gGT5qUU9avmzL8F7mz~fwaNpwldFesm-s5wuMAweQ~" +
                "AsqmTi9DRESOqVoOer0459RUrXP8NMVGk_eKnv0TNl4-Skii5DqUYaP6wQ90JXPK")
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
