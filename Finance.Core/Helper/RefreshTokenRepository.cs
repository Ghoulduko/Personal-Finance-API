using Finance.Core.Database;
using Finance.Core.Entities;
using Finance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Core.Helper;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly PersonalFinanceDbContext _context;

    public RefreshTokenRepository(PersonalFinanceDbContext context)
    {
        _context = context;
    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RefreshToken>> GetAllRefreshTokens()
    {
        return await _context.RefreshTokens.ToListAsync();
    }

    public async Task<RefreshToken?> GetRefreshToken(string incomingHashedToken)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == incomingHashedToken);
    }

    public async Task Delete(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
        await _context.SaveChangesAsync();
    }
}