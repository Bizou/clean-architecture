namespace CleanArchitecture.Domain;

public record TodoItem
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public bool IsComplete { get; set; }
}
