namespace ShinyBackgroundLoc;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PermissonPage), typeof(PermissonPage));
    }
}

