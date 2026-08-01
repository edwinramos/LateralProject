using FluentValidation;

namespace LateralProject.Application.Features.LateralEntities.Commands.Create;

public sealed class CreateLateralEntityCommandValidator
    : AbstractValidator<CreateLateralEntityCommand>
{
    public CreateLateralEntityCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(250);
    }
}