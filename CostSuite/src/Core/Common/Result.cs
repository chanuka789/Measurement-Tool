namespace CostSuite.Core.Common;

/// <summary>
/// Simple success/failure wrapper used across the domain.
/// </summary>
public record Result<T>(bool Success, T? Value, string? Error)
{
    /// <summary>Creates a successful result.</summary>
    public static Result<T> Ok(T value) => new(true, value, null);

    /// <summary>Creates a failed result with the provided error message.</summary>
    public static Result<T> Fail(string error) => new(false, default, error);
}

