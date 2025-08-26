using System.Collections.Generic;
using System.Linq;

namespace CostSuite.Core.Domain;

public class Estimate
{
    public List<EstimateItem> Items { get; } = new();

    public double Total => Items.Sum(i => i.Quantity * i.Rate);
}

public record EstimateItem(string Name, double Quantity, double Rate);

