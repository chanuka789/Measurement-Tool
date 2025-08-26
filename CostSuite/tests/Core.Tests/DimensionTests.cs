using CostSuite.Core.Common;
using CostSuite.Core.Domain;

namespace Core.Tests;

public class DimensionTests
{
    [Fact]
    public void Project_can_compute_dimensions_with_unit_conversion()
    {
        var project = new Project("Demo", Units.Metric);
        var drawing = new Drawing("A1");
        project.AddDrawing(drawing);

        var poly = new Polygon2D(new[]
        {
            new Point2D(0, 0),
            new Point2D(5, 0),
            new Point2D(5, 5),
            new Point2D(0, 5)
        });

        var area = poly.Area; // 25 sq meters
        var areaFeet = UnitConverter.FromSquareMeters(area, Units.Imperial);

        var dim = new Dimension(Guid.NewGuid(), DimensionType.Area, "Default", "#FF0000", area);
        project.Dimensions.Add(dim);

        Assert.Single(project.Drawings);
        Assert.Single(project.Dimensions);
        Assert.Equal(25, dim.Quantity, 6);
        Assert.Equal(269.09776, areaFeet, 5);

        var line = new Polyline2D(new[]
        {
            new Point2D(0, 0),
            new Point2D(0, 3)
        });

        var lengthFeet = UnitConverter.FromMeters(line.Length(), Units.Imperial);
        Assert.Equal(9.84252, lengthFeet, 5);
    }
}

