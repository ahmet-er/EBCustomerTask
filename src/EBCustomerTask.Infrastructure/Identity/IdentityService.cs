using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace EBCustomerTask.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task AddToRoleAsync(AppUser user, Role role)
        {
            await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(user.Email);

            if (hasUser is not null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User with this email already exist." });
            }

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            var hasUser = await _userManager.FindByIdAsync(userId);

            if (hasUser is null) return null;

            return hasUser;
        }

        public async Task<SignInResult> SignInAsync(string email, string password, bool rememberMe)
        {
            var hasUser = await _userManager.FindByNameAsync(email);

            if (hasUser is null) return SignInResult.Failed;

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, rememberMe, false);

            return signInResult;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
