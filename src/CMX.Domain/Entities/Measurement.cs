namespace CMX.Domain.Entities;

public class Measurement
{
    public int Id { get; set; }
    public int DrawingId { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
}
