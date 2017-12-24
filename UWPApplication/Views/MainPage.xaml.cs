using Windows.UI.Xaml.Controls;

namespace UWPApplication.Views
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (args.IsSettingsInvoked)
			{
				//ContentFrame.Navigate(typeof(SettingsPage));
			}
			else
			{
				switch (args.InvokedItem)
				{
					case "Home":
						ContentFrame.Navigate(typeof(HomePage));
						break;
				}
			}
		}

		private void NavigationView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			// set the initial SelectedItem 
			foreach (NavigationViewItemBase item in NavigationView.MenuItems)
			{
				if (item is NavigationViewItem && item.Tag.ToString() == "home")
				{
					NavigationView.SelectedItem = item;
					ContentFrame.Navigate(typeof(HomePage));
					break;
				}
			}

		}
	}
}
