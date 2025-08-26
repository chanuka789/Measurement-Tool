using CostSuite.Core.Domain;

namespace CostSuite.Core.Abstractions;

public interface IWorkbookEngine
{
    void Recalculate(Estimate estimate);
}

