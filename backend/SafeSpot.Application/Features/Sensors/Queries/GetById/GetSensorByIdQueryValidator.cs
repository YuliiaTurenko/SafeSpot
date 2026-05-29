using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Sensors.Queries.GetById;

public class GetSensorByIdQueryValidator : AbstractValidator<GetSensorByIdQuery>
{
    private readonly ISensorRepository _sensorRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public GetSensorByIdQueryValidator(ISensorRepository sensorRepo, ISavedShelterRepository savedRepo)
    {
        _sensorRepo = sensorRepo;
        _savedShelterRepo = savedRepo;

        RuleFor(x => x).MustAsync(SensorExists).WithMessage("Sensor not found.")
                       .MustAsync(UserHasPermission).WithMessage("You don't have permission.");
    }

    private async Task<bool> SensorExists(GetSensorByIdQuery cmd, CancellationToken ct)
    {
        return await _sensorRepo.ExistsByIdAsync(cmd.SensorId);
    }

    private async Task<bool> UserHasPermission(GetSensorByIdQuery cmd, CancellationToken ct)
    {
        var sensor = await _sensorRepo.GetByIdAsync(cmd.SensorId);

        return await _savedShelterRepo.UserHasPermissionAsync(cmd.UserId, sensor.ShelterId);
    }
}
