using AutoMapper;
using EBCustomerTask.Application.DTOs;
using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EBCustomerTask.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public IdentityService(IIdentityRepository identityRepository, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterViewModel model, string password)
        {
            var appUser = _mapper.Map<AppUser>(model);

            var result = await _identityRepository.CreateUserAsync(appUser, password);

            return result;
        }

        public Task<SignInResult> SignInAsync(LoginViewModel model)
        {
            var appUser = _mapper.Map<AppUser>(model);

            var result = _identityRepository.SignInAsync(model.Email, model.Password, model.RememberMe);

            return result;
        }

        public Task SignOutAsync() => _identityRepository.SignOutAsync();
    }
}
