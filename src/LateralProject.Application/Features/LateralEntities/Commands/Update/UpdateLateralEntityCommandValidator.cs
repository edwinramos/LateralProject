using FluentValidation;

namespace LateralProject.Application.Features.LateralEntities.Commands.Update;

public sealed class UpdateLateralEntityCommandValidator
    : AbstractValidator<UpdateLateralEntityCommand>
{
    public UpdateLateralEntityCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(250);
    }
}