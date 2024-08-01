using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace EBCustomerTask.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task AddToRoleAsync(AppUser user, Role role);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<SignInResult> SignInAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();
    }
}
