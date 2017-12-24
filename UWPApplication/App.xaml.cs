using System.Threading.Tasks;
using BusinessModels;
using BusinessModels.Contracts;
using Microsoft.Practices.Unity;
using UWPApplication.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;

namespace UWPApplication
{
	sealed partial class App
	{
		public App()
		{
			this.InitializeComponent();
			this.UnhandledException += App_UnhandledException;
			this.Suspending += OnSuspending;
		}

		protected override Task OnInitializeAsync(IActivatedEventArgs args)
		{
			DatabaseInitializer.InitializeDatabase(); //TODO: make it as Dependency Injection

			Container.RegisterType<IHomePageDataService, HomePageDataService>();
			Container.RegisterType<IHomePageAlternativeDataService, HomePageAlternativeDataService>();

			return base.OnInitializeAsync(args);
		}

		protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{
			NavigationService.Navigate(PageTokens.HomePage, null);
			return Task.FromResult(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
		{
			// TODO: Implementing the logging and analytics, such as HockeyApp or something like that
#if DEBUG
			System.Diagnostics.Debug.WriteLine($"Exception: {e.Message}");
			if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#endif
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
}
