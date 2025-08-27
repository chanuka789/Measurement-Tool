using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QS_Takeoff.UI.Views
{
    public partial class ProjectExplorer : UserControl
    {
        public ProjectExplorer()
        {
            InitializeComponent();
        }

        private void FilterBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FilterBox.Text == "Filter")
            {
                FilterBox.Text = string.Empty;
                FilterBox.Foreground = Brushes.Black;
            }
        }

        private void FilterBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterBox.Text))
            {
                FilterBox.Text = "Filter";
                FilterBox.Foreground = Brushes.Gray;
            }
        }
    }
}
