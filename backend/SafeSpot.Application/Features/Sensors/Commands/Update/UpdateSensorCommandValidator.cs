using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Sensors.Commands.Update;

public class UpdateSensorCommandValidator : AbstractValidator<UpdateSensorCommand>
{
    private readonly ISensorRepository _sensorRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public UpdateSensorCommandValidator(ISensorRepository sensorRepo, ISavedShelterRepository savedRepo)
    {
        _sensorRepo = sensorRepo;
        _savedShelterRepo = savedRepo;

        RuleFor(x => x.MaxValue).GreaterThan(x => x.MinValue);

        RuleFor(x => x).MustAsync(SensorExists).WithMessage("Sensor not found.")
                       .MustAsync(UserHasPermission).WithMessage("You don't have permission.");
    }

    private async Task<bool> SensorExists(UpdateSensorCommand cmd, CancellationToken ct)
    {
        return await _sensorRepo.ExistsByIdAsync(cmd.SensorId);
    }

    private async Task<bool> UserHasPermission(UpdateSensorCommand cmd, CancellationToken ct)
    {
        var sensor = await _sensorRepo.GetByIdAsync(cmd.SensorId);

        return await _savedShelterRepo.UserHasPermissionAsync(cmd.UserId, sensor.ShelterId);
    }
}
