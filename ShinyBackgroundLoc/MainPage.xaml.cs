using Shiny.Locations;

namespace ShinyBackgroundLoc;

public partial class MainPage : ContentPage
{
    private readonly IGpsManager _gpsManager;
    private readonly Services.Delegates.GpsDelegate _gpsDelegate;

    public MainPage(IGpsManager gpsManager, IGpsDelegate gpsDelegate)
	{
		InitializeComponent();

        _gpsManager = gpsManager;
        _gpsDelegate = (Services.Delegates.GpsDelegate)gpsDelegate;

        _gpsDelegate.OnGpsReading += OnGpsReading;

        Dispatcher.Dispatch(async () => { await CheckPermission(); });

        SetButtonState();
    }

    ~MainPage()
    {
        _gpsDelegate.OnGpsReading -= OnGpsReading;
    }

    private void OnGpsReading(object? sender, GpsReading e)
    {
        LatLabel.Text = e.Position.Latitude.ToString();
        LonLabel.Text = e.Position.Longitude.ToString();
        AccLabel.Text = e.PositionAccuracy.ToString();
    }

    async Task CheckPermission()
    {
        var ps = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

        if (ps == PermissionStatus.Granted) return;

        await Shell.Current.GoToAsync($"{nameof(PermissonPage)}");
    }

    void SetButtonState()
    {
        var enabled = _gpsManager.IsListening();

        Btn.Text = enabled ? "STOP" : "START";
        Btn.BackgroundColor = enabled ? Colors.Red: Colors.Green;
    }

    private async void OnBtnClicked(object sender, EventArgs e)
	{
        var enabled = _gpsManager.IsListening();

        if(!enabled)
        {
            await _gpsManager.StartListener(new GpsRequest
            {
                BackgroundMode = GpsBackgroundMode.Realtime,
                Accuracy = GpsAccuracy.Highest,
                DistanceFilterMeters = 1
            });
        }
        else
        {
            await _gpsManager.StopListener();
        }

        SetButtonState();
    }



}


