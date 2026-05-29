namespace SafeSpot.Application.Abstractions;

public interface ISensorCommandPublisher
{
    Task PublishDisableAsync(long shelterId, long sensorId);
    Task PublishEnableAsync(long shelterId, long sensorId);
}
