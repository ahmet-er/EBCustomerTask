using EBCustomerTask.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace EBCustomerTask.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateUserAsync(RegisterViewModel model, string password);
        Task<SignInResult> SignInAsync(LoginViewModel model);
        Task SignOutAsync();
    }
}
