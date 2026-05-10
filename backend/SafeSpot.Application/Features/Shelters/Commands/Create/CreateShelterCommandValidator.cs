using FluentValidation;

namespace SafeSpot.Application.Features.Shelters.Commands.Create;

public class CreateShelterCommandValidator : AbstractValidator<CreateShelterCommand>
{
    public CreateShelterCommandValidator()
    {
        RuleFor(x => x.Address).MaximumLength(400).NotEmpty();
        RuleFor(x => x.Latitude).NotEmpty();
        RuleFor(x => x.Longitude).NotEmpty();
        RuleFor(x => x.Capacity).GreaterThan(0);
    }
}