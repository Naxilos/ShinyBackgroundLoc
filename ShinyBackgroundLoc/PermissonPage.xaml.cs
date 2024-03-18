namespace ShinyBackgroundLoc;

public partial class PermissonPage : ContentPage
{
	public PermissonPage()
	{
		InitializeComponent();
	}

	bool permissionGranted = false;

	private async Task AskForPermission()
	{
        var ps = await Permissions.RequestAsync<Permissions.LocationAlways>();

        GoToMainPageBtn.IsEnabled = ps == PermissionStatus.Granted;
    }

	

    async void GrantBtn_Clicked(Object sender, EventArgs e)
    {
        await AskForPermission();
    }

    async void GoToMainPageBtn_Clicked(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
