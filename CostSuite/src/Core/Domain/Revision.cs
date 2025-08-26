using System;
using System.Collections.Generic;

namespace CostSuite.Core.Domain;

public record Revision(Guid Id, IReadOnlyList<Guid> SourceDrawingIds);

