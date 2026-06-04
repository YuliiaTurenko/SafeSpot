namespace SafeSpot.Application.Abstractions;

public interface IUserRoleService
{
    Task AssignOperatorRoleAsync(string identityId);
    Task RevokeOperatorRoleAsync(string identityId);
    Task<bool> IsInRoleAsync(string identityId, string role);
    Task<List<string>> GetIdentityIdsByRoleAsync(string role);
    Task<string?> GetEmailByIdentityIdAsync(string identityId);
}
