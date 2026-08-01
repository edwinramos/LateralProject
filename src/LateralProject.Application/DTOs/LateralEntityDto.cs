namespace LateralProject.Application.DTOs;

public sealed record LateralEntityDto(
    Guid Id,
    string Description,
    DateTime CreatedDateTime,
    DateTime ModifiedDateTime);