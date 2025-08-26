using CostSuite.Core.Domain;

namespace CostSuite.Core.Abstractions;

public interface IReportGenerator
{
    byte[] Generate(Project project);
}

