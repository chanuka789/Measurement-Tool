using System;
using System.Collections.Generic;
using CostSuite.Core.Common;

namespace CostSuite.Core.Domain;

public class Drawing
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Path { get; init; }
    public double Scale { get; set; } = 1d;
    public List<(Point2D Image, Point2D World)> CalibrationPoints { get; } = new();
    public List<string> Layers { get; } = new();

    public Drawing(string path, double scale = 1d)
    {
        Path = path;
        Scale = scale;
    }
}

