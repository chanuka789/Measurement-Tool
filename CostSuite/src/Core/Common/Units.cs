namespace CostSuite.Core.Common;

/// <summary>Supported measurement systems.</summary>
public enum Units
{
    Metric,
    Imperial
}

/// <summary>
/// Conversion helpers between metric (meters) and imperial (feet) units.
/// </summary>
public static class UnitConverter
{
    private const double FeetToMeters = 0.3048d;

    /// <summary>Converts a length to meters.</summary>
    public static double ToMeters(double value, Units units) =>
        units == Units.Metric ? value : value * FeetToMeters;

    /// <summary>Converts a length from meters to the specified units.</summary>
    public static double FromMeters(double meters, Units units) =>
        units == Units.Metric ? meters : meters / FeetToMeters;

    /// <summary>Converts an area to square meters.</summary>
    public static double ToSquareMeters(double area, Units units)
    {
        var factor = FeetToMeters * FeetToMeters;
        return units == Units.Metric ? area : area * factor;
    }

    /// <summary>Converts an area from square meters to the specified units.</summary>
    public static double FromSquareMeters(double area, Units units)
    {
        var factor = FeetToMeters * FeetToMeters;
        return units == Units.Metric ? area : area / factor;
    }
}

