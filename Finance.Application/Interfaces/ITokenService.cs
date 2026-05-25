using Finance.Core.Entities;

namespace Finance.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}