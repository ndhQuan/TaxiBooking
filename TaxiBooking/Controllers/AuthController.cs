using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TaxiBooking.DataContext;
using TaxiBooking.Models;
using TaxiBooking.Models.Dto;
using TaxiBooking.Utility;

namespace TaxiBooking.Controllers
{
    [Route("/api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private APIResponse _response;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;
        public AuthController(AppDbContext db, IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
            _response = new APIResponse();
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            AppUser userFromDb = _db.Users.FirstOrDefault(u=>u.PhoneNumber == model.Phone);
            bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);

            if(userFromDb == null || !isValid)
            {
                _response.Result = new LoginResponseDTO();
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("User name or password is incorrect");
                return BadRequest(_response);
            }

            //generate Jwt Token
            var roles = await _userManager.GetRolesAsync(userFromDb);
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.UTF8.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("fullname", userFromDb.Name),
                    new Claim("id", userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Email, userFromDb.Email),
                    new Claim(ClaimTypes.MobilePhone, userFromDb.PhoneNumber),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponse = new()
            {
                Id = userFromDb.Id,
                Name = userFromDb.Name,
                Email = userFromDb.Email,
                Phone = userFromDb.PhoneNumber,
                Image = userFromDb.Image,
                Token = tokenHandler.WriteToken(token),
            };

            if (loginResponse.Phone == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("User name or password is incorrect");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = false;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            AppUser userFromDb = _db.AppUsers.FirstOrDefault(u=>u.PhoneNumber == model.PhoneNumber);

            if (userFromDb != null) 
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Phone Number already registerd");
                return BadRequest(_response);
            }

            AppUser newUser = new()
            {
                UserName = model.PhoneNumber,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                CreatedAt = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            try
            {
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Driver));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                    }
                    if (model.Role.ToLower() == SD.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Admin);
                    }
                    else if (model.Role.ToLower() == SD.Role_Driver)
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Driver);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Customer);
                    }

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
            }
            catch (Exception)
            {
            }
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Error occur while register");
            return BadRequest(_response);
        }
    }
}
