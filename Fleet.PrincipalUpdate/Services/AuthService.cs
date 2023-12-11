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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(ApplicationDbContext applicationDbContext, 
            UserManager<AplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string rolename)
        {
            var user = _db.AplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {

                if(!_roleManager.RoleExistsAsync(rolename).GetAwaiter().GetResult()) {
                    //
                    _roleManager.CreateAsync( new IdentityRole(rolename)).GetAwaiter().GetResult ();
                
                }

                await _userManager.AddToRoleAsync(user, rolename);
                return true;

            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user =_db.AplicationUsers.FirstOrDefault(u => u.UserName.ToLower()
            == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

            if(user == null ||  isValid == false) { 
                    return new LoginResponseDto() { User=null,Token = ""};
            }

            // if user was found generate Jwt token
            var token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
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
