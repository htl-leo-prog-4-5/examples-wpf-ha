using System.Threading.Tasks;
using AuthenticationBase.DataTransferObjects;
using AuthenticationBase.Entities;

namespace AuthenticationBase.Contracts.Persistence
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser?> GetByUserIdAsync(string applicationUserId);
        Task<ApplicationUser?> FindByEmailAsync(string mail);
        Task<ApplicationUser[]> GetByUserIdAsync();
        Task<UserDetailsDto[]> GetWithRolesAndLastLogin();
        Task<UserDto?> GetUserDto(string userId);
    }
}
