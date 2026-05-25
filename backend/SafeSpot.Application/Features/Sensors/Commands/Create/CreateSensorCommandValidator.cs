using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Sensors.Commands.Create;

public class CreateSensorCommandValidator : AbstractValidator<CreateSensorCommand>
{
    private readonly ISensorRepository _sensorRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public CreateSensorCommandValidator(ISensorRepository sensorRepo, ISavedShelterRepository savedRepo)
    {
        _sensorRepo = sensorRepo;
        _savedShelterRepo = savedRepo;

        RuleFor(x => x.MaxValue).GreaterThan(x => x.MinValue);

        RuleFor(x => x).MustAsync(SensorExists).WithMessage("Sensor with such type already exists.")
                       .MustAsync(UserHasPermission).WithMessage("You don't have permission.");
    }

    private async Task<bool> SensorExists(CreateSensorCommand cmd, CancellationToken ct)
    {
        return await _sensorRepo.ExistsByTypeAsync(cmd.ShelterId, cmd.Type);
    }

    private async Task<bool> UserHasPermission(CreateSensorCommand cmd, CancellationToken ct)
    {
        return await _savedShelterRepo.UserHasPermissionAsync(cmd.UserId, cmd.ShelterId);
    }
}