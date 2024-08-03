using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace EBCustomerTask.Core.Interfaces
{
    public interface IIdentityRepository
    {
        Task AddToRoleAsync(AppUser user, Role role);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<SignInResult> SignInAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();
    }
}
