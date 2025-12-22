using Domain.Abstractions;

namespace Domain.Entities;

public class KgmEntity : Entity
{
    public int Id { get; set; }
    public string Dolphone { get; set; } = string.Empty;
}
