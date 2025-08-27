using System;
using System.Windows;
using QS_Takeoff.UI.Models;

namespace QS_Takeoff.UI.Views
{
    public partial class DimensionGroupProperties : Window
    {
        public DimensionGroupPropertiesModel Properties { get; } = new DimensionGroupPropertiesModel();

        public DimensionGroupProperties()
        {
            InitializeComponent();

            MeasurementTypeCombo.ItemsSource = Enum.GetValues(typeof(MeasurementType));
            DefaultDisplayCombo.ItemsSource = Enum.GetValues(typeof(MeasurementType));

            var colors = new[] { "User Defined", "Red", "Blue", "Green" };
            PositiveColorCombo.ItemsSource = colors;
            NegativeColorCombo.ItemsSource = colors;

            var styles = new[] { "Solid", "Dashed", "Dotted" };
            PositiveStyleCombo.ItemsSource = styles;
            NegativeStyleCombo.ItemsSource = styles;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            Properties.Name = NameTextBox.Text;
            Properties.Folder = FolderTextBox.Text;
            if (MeasurementTypeCombo.SelectedItem is MeasurementType mt)
                Properties.MeasurementType = mt;
            if (DefaultDisplayCombo.SelectedItem is MeasurementType dd)
                Properties.DefaultDisplay = dd;
            if (double.TryParse(MultiplierTextBox.Text, out var mult))
                Properties.DefaultMultiplier = mult;
            if (double.TryParse(WidthTextBox.Text, out var width))
                Properties.DefaultWidth = width;
            if (double.TryParse(HeightTextBox.Text, out var height))
                Properties.DefaultHeight = height;
            if (double.TryParse(DepthTextBox.Text, out var depth))
                Properties.DefaultDepth = depth;
            Properties.AddToGfa = AddToGfaCheck.IsChecked == true;
            Properties.PositiveColor = PositiveColorCombo.SelectedItem?.ToString() ?? "User Defined";
            Properties.PositiveStyle = PositiveStyleCombo.SelectedItem?.ToString() ?? "Solid";
            Properties.NegativeColor = NegativeColorCombo.SelectedItem?.ToString() ?? "Red";
            Properties.NegativeStyle = NegativeStyleCombo.SelectedItem?.ToString() ?? "Solid";
            Properties.WeightUom = WeightUomTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
