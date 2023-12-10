using Fleet.PrincipalUpdate.Data;
using Fleet.PrincipalUpdate.Models;
using Fleet.PrincipalUpdate.Models.Dto;
using Fleet.PrincipalUpdate.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Fleet.PrincipalUpdate.Services
{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(ApplicationDbContext applicationDbContext, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            AplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber

            };

            try
            {
                var result =  await _userManager.CreateAsync(user, registrationRequestDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _db.AplicationUsers.
                        First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber

                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;

                }
            }catch (Exception ex)
            {
                return "error encounterd";
            }

       
        }
    }
} 
