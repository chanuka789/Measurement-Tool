using System.Windows;
using QS_Takeoff.UI.Views;

namespace QS_Takeoff.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenDimensionGroupProperties_Click(object sender, RoutedEventArgs e)
        {
            var window = new DimensionGroupProperties();
            window.ShowDialog();
        }
    }
}
