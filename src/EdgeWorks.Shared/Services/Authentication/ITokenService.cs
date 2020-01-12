using System.Threading.Tasks;

namespace EdgeWorks.Shared.Services.Authentication
{
    public interface ITokenService
    {
        Task<string> GetAccessToken();
    }
}
