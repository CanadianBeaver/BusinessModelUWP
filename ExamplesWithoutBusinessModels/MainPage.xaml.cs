using Windows.UI.Xaml.Controls;

namespace ExamplesWithoutBusinessModels
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
					case "Default":
						ContentFrame.Navigate(typeof(DefaultPage));
						break;
					case "MVVM":
						ContentFrame.Navigate(typeof(MvvmView));
						break;
				}
			}
		}

		private void NavigationView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			// set the initial SelectedItem 
			foreach (NavigationViewItemBase item in NavigationView.MenuItems)
			{
				if (item is NavigationViewItem && item.Tag.ToString() == "default")
				{
					NavigationView.SelectedItem = item;
					ContentFrame.Navigate(typeof(DefaultPage));
					break;
				}
			}

		}
	}
}
