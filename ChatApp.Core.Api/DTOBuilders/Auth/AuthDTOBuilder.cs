using ChatApp.Core.Utils.DTOs;
using System.Threading.Tasks;

namespace ChatApp.Core.Api.DTOBuilders
{
    public static class AuthDTOBuilder
    {
        public static async Task<AuthDTO> Login(string email, string password)
        {
            var dto=new AuthDTO();

            return dto;
        }
    }
}
