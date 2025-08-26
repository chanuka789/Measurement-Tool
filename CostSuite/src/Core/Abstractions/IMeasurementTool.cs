using CostSuite.Core.Common;
using CostSuite.Core.Domain;

namespace CostSuite.Core.Abstractions;

public interface IMeasurementTool
{
    Result<Dimension> Measure(Polyline2D geometry, Units units);
}

