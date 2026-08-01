using LateralProject.Domain.Exceptions;

namespace LateralProject.Domain.Entities;

public class LateralEntity
{
    public Guid Id { get; private set; }

    public string Description { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime ModifiedDateTime { get; private set; }

    private LateralEntity() { }

    public LateralEntity(string description)
    {
        Id = Guid.NewGuid();
        SetDescription(description);

        CreatedDateTime = DateTime.UtcNow;
        ModifiedDateTime = CreatedDateTime;
    }

    public void Update(string description)
    {
        SetDescription(description);
        ModifiedDateTime = DateTime.UtcNow;
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description is required.");

        Description = description.Trim();
    }
}