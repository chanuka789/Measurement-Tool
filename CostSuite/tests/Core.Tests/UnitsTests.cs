using CostSuite.Core.Common;

namespace Core.Tests;

public class UnitsTests
{
    [Fact]
    public void Converts_length_between_units()
    {
        var meters = UnitConverter.ToMeters(3, Units.Imperial);
        Assert.Equal(0.9144, meters, 6);

        var feet = UnitConverter.FromMeters(1, Units.Imperial);
        Assert.Equal(3.28084, feet, 5);
    }

    [Fact]
    public void Converts_area_between_units()
    {
        var sqm = UnitConverter.ToSquareMeters(9, Units.Imperial);
        Assert.Equal(0.836127, sqm, 6);

        var sqft = UnitConverter.FromSquareMeters(1, Units.Imperial);
        Assert.Equal(10.763910, sqft, 6);
    }
}

