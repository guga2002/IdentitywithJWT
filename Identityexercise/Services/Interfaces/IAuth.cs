using Identityexercise.ResponseAndRequest.Request;

namespace Identityexercise.Services.Interfaces
{
    public interface IAuth
    {
        Task<bool> Register(RegisterRequest user);
        Task<string> SignIn(SIgnInRequest sign);
    }
}
