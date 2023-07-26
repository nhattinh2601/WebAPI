using authentication_authorization_webapi.DataAccess;
using authentication_authorization_webapi.DTO;
using authentication_authorization_webapi.Entity;
using authentication_authorization_webapi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace authentication_authorization_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly MyDbContext _context;
        private readonly AppSetting _appSettings;

        public UserController(IUserService userService,MyDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _userService = userService;
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult GetAll() 
        {
            try
            {
                var users = _userService.getAll();
                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.username == loginDTO.username && u.password == loginDTO.password);
                if (user == null)
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = "Invalid username/password"
                    });
                }
                //cap token
                var token = await GenerateToken(user);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Authenticate success",
                    Data = token
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        private async Task<TokenModel> GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);                        
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.fullname),
                    new Claim(JwtRegisteredClaimNames.Email, user.email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("username",user.username),
                    new Claim("Id",user.Id.ToString()),

                    //roles
                    new Claim(ClaimTypes.Role, user.GetRoleName())
                    //new Claim(ClaimTypes.Role, user.Role?.role_name?? "admin")
                    

                }),
                Expires = DateTime.UtcNow.AddSeconds(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);

            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();


            //luu vao db

            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                
                user_id = user.Id,
                JwtId = token.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssueAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)

            };

            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }


        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }

    }
}
