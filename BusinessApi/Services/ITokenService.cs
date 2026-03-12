using BusinessApi.Entities;

namespace BusinessApi.Services;

public interface ITokenService {
    string CreateToken(User user);
}
