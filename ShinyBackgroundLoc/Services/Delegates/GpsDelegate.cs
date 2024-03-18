using System.Diagnostics;
using AndroidX.Core.App;
using Shiny.Locations;

namespace ShinyBackgroundLoc.Services.Delegates;


public partial class GpsDelegate : IGpsDelegate
{
    public event EventHandler<GpsReading> OnGpsReading;

    public Task OnReading(GpsReading reading)
    {
        Debug.WriteLine($"GPS RECORD [{reading.Timestamp}]:\n{reading.Position.Latitude}\n{reading.Position.Longitude}\n{reading.PositionAccuracy}\n");

        OnGpsReading?.Invoke(this, reading);

        return Task.CompletedTask;
    }
}

#if ANDROID
public partial class GpsDelegate : Shiny.IAndroidForegroundServiceDelegate
{
    public void Configure(NotificationCompat.Builder builder)
    {
        builder
            .SetContentTitle("Location service enabled")
            .SetContentText("This app is following you!!")
            .SetSmallIcon(Android.Resource.Mipmap.SymDefAppIcon);
    }

}
#endif

