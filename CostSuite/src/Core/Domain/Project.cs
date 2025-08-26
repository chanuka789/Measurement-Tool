using System.Collections.Generic;
using CostSuite.Core.Common;

namespace CostSuite.Core.Domain;

public class Project
{
    public string Name { get; set; }
    public Units Units { get; set; }
    public List<Drawing> Drawings { get; } = new();
    public List<Dimension> Dimensions { get; } = new();

    public Project(string name, Units units)
    {
        Name = name;
        Units = units;
    }

    public void AddDrawing(Drawing drawing) => Drawings.Add(drawing);
}

