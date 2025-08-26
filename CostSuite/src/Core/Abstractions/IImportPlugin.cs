using CostSuite.Core.Domain;

namespace CostSuite.Core.Abstractions;

public interface IImportPlugin
{
    Drawing Import(string path);
}

