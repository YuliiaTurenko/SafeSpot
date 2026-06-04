namespace SafeSpot.Api.Contracts.Admin;

public record AssignModeratorRequest(long TargetUserId, long ShelterId);
