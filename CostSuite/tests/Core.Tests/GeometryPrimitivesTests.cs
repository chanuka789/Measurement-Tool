using CostSuite.Core.Common;

namespace Core.Tests;

public class GeometryPrimitivesTests
{
    [Fact]
    public void Polyline_length_is_summed()
    {
        var poly = new Polyline2D(new[]
        {
            new Point2D(0, 0),
            new Point2D(3, 4),
            new Point2D(6, 8)
        });

        Assert.Equal(10, poly.Length(), 6);
    }

    [Fact]
    public void Polygon_area_accounts_for_holes()
    {
        var outer = new[]
        {
            new Point2D(0, 0),
            new Point2D(10, 0),
            new Point2D(10, 10),
            new Point2D(0, 10)
        };
        var hole = new[]
        {
            new Point2D(2, 2),
            new Point2D(6, 2),
            new Point2D(6, 6),
            new Point2D(2, 6)
        };

        var poly = new Polygon2D(outer, new[] { hole });

        Assert.Equal(84, poly.Area, 6);
    }
}

