namespace cw7.DTOs;

public class PcGetDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<ComponentDto> Components { get; set; }
        = new();
}