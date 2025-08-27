using System.Windows;
using QS_Takeoff.UI.ViewModels;
using QS_Takeoff.UI.Views;

namespace QS_Takeoff.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProjectExplorerViewModel _projectExplorerViewModel = new ProjectExplorerViewModel();

        public MainWindow()
        {
            InitializeComponent();

            // Bind the project explorer to the backing view model
            ProjectExplorer.DataContext = _projectExplorerViewModel;

            // Seed with the sample drawings that were previously hard coded
            _projectExplorerViewModel.Drawings.Add("A-101 - Floor Plan");
            _projectExplorerViewModel.Drawings.Add("A-201 - Sections");
            _projectExplorerViewModel.Drawings.Add("S-001 - General Notes");
        }

        private void OpenDimensionGroupProperties_Click(object sender, RoutedEventArgs e)
        {
            var window = new DimensionGroupProperties();
            if (window.ShowDialog() == true)
            {
                // After the dialog is accepted, add the new group to the explorer
                _projectExplorerViewModel.DimensionGroups.Add(window.Properties.Name);
            }
        }
    }
}
