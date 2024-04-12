using AutoMapper;
using Contracts;
using Entites.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObject_DTO_;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)

        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)

        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user,
            userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

            if(!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");

            return result;

        }



        /* it creates a token. It does 
           that by collecting information from the private methods and serializing 
           token options with the WriteToken method.*/

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes("your_secret_key_here_00000000000000000");
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, _user.UserName)
    };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)

        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        
    }
}
