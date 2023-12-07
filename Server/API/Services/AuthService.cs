using API.ViewModels;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class AuthService
    {
        private readonly IConfiguration config;
        private readonly ILogger logger;
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly SignInManager<AppIdentityUser> signInManager;

        public AuthService(
            IConfiguration configuration,
            ILogger<AuthService> logger,
            UserManager<AppIdentityUser> userManager,
            SignInManager<AppIdentityUser> signInManager) 
        {
            this.config = configuration;
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<SignUpResponseViewModel> SignUpAsync(SignUpRequestViewModel request)
        {
            if (request is null)
            {
                this.logger.LogCritical("Model validation at the presentation layer didn't work");
                throw new ArgumentNullException($"{nameof(request)}", "Argument is null");
            }

            var appIdentityUser = new AppIdentityUser()
            {
                Email = request.Email,
                UserName = request.UserName,
                AppUser = new AppUser()
                {
                    Name = request.UserName,
                    Email = request.Email
                },
            };

            var result = await this.userManager
                .CreateAsync(appIdentityUser, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                this.logger.LogWarning("Failed registration attempt.", errors);
                return new SignUpResponseViewModel
                {
                    Errors = errors,
                };
            }

            return new SignUpResponseViewModel { Success = true };

        }

        public async Task<SignInResponseViewModel> LoginAsync(SignInRequestViewModel request)
        {
            if (request is null)
            {
                this.logger.LogCritical("Model validation at the presentation layer didn't work");
                throw new ArgumentNullException($"{nameof(request)}", "Argument is null");
            }

            var user = await this.userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                this.logger.LogWarning("Authorization attempt with invalid name");
                return new SignInResponseViewModel
                {
                    Message = "Invalid email.",
                };
            }

            if (!await this.userManager.CheckPasswordAsync(user, request.Password))
            {
                this.logger.LogWarning("Authorization attempt with invalid password");
                return new SignInResponseViewModel
                {
                    Message = "Invalid password.",
                };
            }

            if (user.AppUser is null)
            {
                this.logger.LogCritical("Identity User doesn't have contain AppUser property");
                throw new ArgumentNullException("Identity user doesn't contain AppUser property");
            }

            var token = this.GetToken(user);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new SignInResponseViewModel
            {
                Success = true,
                Token = jwt,
            };
        }

        private static string EncodeToken(string token)
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(bytes);
        }

        private static string DecodeToken(string token)
        {
            var bytes = WebEncoders.Base64UrlDecode(token);
            return Encoding.UTF8.GetString(bytes);
        }

        private JwtSecurityToken GetToken(AppIdentityUser user)
        {
            JwtSecurityToken token = new(
                issuer: this.config["JwtSettings:Issuer"],
                audience: this.config["JwtSettings:Audience"],
                claims: GetClaims(user),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(
                    this.config["JwtSettings:ExpirationTimeMinutes"])),
                signingCredentials: this.GetSigningCredentials());

            this.logger.LogInformation("Generate token for user {userEmail}", user.Email);

            return token;
        
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(this.config["JwtSettings:SecurityKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private static List<Claim> GetClaims(AppIdentityUser user)
        {
            var claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.AppUser!.Name),
                new Claim("userId", user.AppUser!.Id.ToString()),
            };

            return claims;

        }


    }
}
