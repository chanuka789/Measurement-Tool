namespace CMX.Domain.Entities;

public class Drawing
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
}
