using System;
using System.Collections.Generic;

namespace CostSuite.Core.Domain;

public enum DimensionType
{
    Area,
    Length,
    Count
}

public record Dimension(
    Guid Id,
    DimensionType Type,
    string Layer,
    string Color,
    double Quantity,
    IDictionary<string, string>? Metadata = null);

