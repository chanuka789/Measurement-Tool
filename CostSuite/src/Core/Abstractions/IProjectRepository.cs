using System;
using CostSuite.Core.Common;
using CostSuite.Core.Domain;

namespace CostSuite.Core.Abstractions;

public interface IProjectRepository
{
    Project Create(string name, Units units);
    Project? Get(Guid id);
    void Save(Project project);
}

