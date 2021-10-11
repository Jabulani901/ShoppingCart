using ShoppingCart.Helpers;

namespace ShoppingCart.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, LoginRequest loginRequest);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
