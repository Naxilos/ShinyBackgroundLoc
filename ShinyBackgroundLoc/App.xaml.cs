using Shiny.Locations;

namespace ShinyBackgroundLoc;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}

