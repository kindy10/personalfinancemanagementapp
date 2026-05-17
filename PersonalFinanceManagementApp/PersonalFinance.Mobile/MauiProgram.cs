using LiveChartsCore.SkiaSharpView.Maui;
using Microsoft.Extensions.Logging;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
namespace PersonalFinance.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseLiveCharts() /// integration for  LIVECHARTS
                 .ConfigureFonts(fonts =>
                  {
                      fonts.AddFont(
                          "OpenSans-Regular.ttf",
                          "OpenSansRegular");

                      fonts.AddFont(
                          "OpenSans-Semibold.ttf",
                          "OpenSansSemibold");
                  });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
