using System;

namespace QS_Takeoff.UI.Models
{
    public class DimensionGroupPropertiesModel
    {
        public string Name { get; set; } = string.Empty;
        public string Folder { get; set; } = string.Empty;
        public MeasurementType MeasurementType { get; set; } = MeasurementType.Area;
        public MeasurementType DefaultDisplay { get; set; } = MeasurementType.Area;
        public double DefaultMultiplier { get; set; } = 1.0;
        public double DefaultWidth { get; set; } = 0.0;
        public double DefaultHeight { get; set; } = 0.0;
        public double DefaultDepth { get; set; } = 0.0;
        public bool AddToGfa { get; set; } = false;
        public string PositiveColor { get; set; } = "User Defined";
        public string PositiveStyle { get; set; } = "Solid";
        public string NegativeColor { get; set; } = "Red";
        public string NegativeStyle { get; set; } = "Solid";
        public string WeightUom { get; set; } = string.Empty;
    }
}
