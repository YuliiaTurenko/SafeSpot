using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class ShelterResource : BaseEntity
{
    public long ShelterId { get; set; }
    public virtual Shelter Shelter { get; set; }

    public ResourceType Type { get; set; }
    public ResourceStatus Status { get; set; }
    public int Amount { get; set; }
}
