using Finance.Core.Entities;

namespace Finance.Core.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshToken refreshToken);
    Task<IEnumerable<RefreshToken>> GetAllRefreshTokens();
    Task<RefreshToken?> GetRefreshToken(string token);
    Task Delete(RefreshToken refreshToken);
}