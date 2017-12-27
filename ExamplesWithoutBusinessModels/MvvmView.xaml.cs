using Windows.UI.Xaml.Controls;

namespace ExamplesWithoutBusinessModels
{
	public sealed partial class MvvmView
	{
		public MvvmView()
		{
			this.InitializeComponent();
			this.DataContext = new MvvmViewModel();
		}
	}
}
