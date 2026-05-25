using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Sensors.Commands.Delete;

public class DeleteSensorCommandValidator : AbstractValidator<DeleteSensorCommand>
{
    private readonly ISensorRepository _sensorRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public DeleteSensorCommandValidator(ISensorRepository sensorRepo, ISavedShelterRepository savedRepo)
    {
        _sensorRepo = sensorRepo;
        _savedShelterRepo = savedRepo;

        RuleFor(x => x).MustAsync(SensorExists).WithMessage("Sensor not found.")
                           .MustAsync(UserHasPermission).WithMessage("You don't have permission.");
    }

    private async Task<bool> SensorExists(DeleteSensorCommand cmd, CancellationToken ct)
    {
        return await _sensorRepo.ExistsByIdAsync(cmd.SensorId);
    }

    private async Task<bool> UserHasPermission(DeleteSensorCommand cmd, CancellationToken ct)
    {
        var sensor = await _sensorRepo.GetByIdAsync(cmd.SensorId);

        return await _savedShelterRepo.UserHasPermissionAsync(cmd.UserId, sensor.ShelterId);
    }
}
