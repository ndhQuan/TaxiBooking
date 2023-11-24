using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaxiBooking.Models;
using TaxiBooking.Models.DTO;
using TaxiBooking.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxiBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUserRepository _dbUser;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserController(IUserRepository dbUser, IMapper mapper, UserManager<AppUser> userManager)
        {
            _dbUser = dbUser;
            _mapper = mapper;
            _userManager = userManager;
            _response = new();
        }
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetUsers([FromQuery]string? role)
        {
            try
            {
                IEnumerable<AppUser> users;
                if(!string.IsNullOrEmpty(role))
                {
                    users = await _userManager.GetUsersInRoleAsync(role);
                }
                else
                {
                    users = await _dbUser.GetAllAsync();
                }
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = users.ToList();
                return Ok(_response);
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
            }
            return _response;
        }

        // PUT api/<UserController>/5
        [Authorize(Roles = "admin,customer")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateUser(string id, [FromBody]UserUpdateDTO updateDTO)
        {
            try
            {
                if(id == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                AppUser user = await _dbUser.GetAsync(u=>u.Id == id, false);
                if(user == null)
                {
                    return NotFound();
                }
                AppUser updatedUser = new()
                {
                    Id = updateDTO.Id,
                    PhoneNumber = updateDTO.PhoneNumber,
                    Email = updateDTO.Email,
                    Name = updateDTO.Name,
                    Image = updateDTO.Image,
                    UserName = user.UserName,
                    SecurityStamp = user.SecurityStamp,
                    PasswordHash = user.PasswordHash,
                    LockoutEnabled = user.LockoutEnabled,
                    NormalizedEmail = user.NormalizedEmail,
                    NormalizedUserName =user.NormalizedUserName,
                    ConcurrencyStamp = user.ConcurrencyStamp,
                };

                await _dbUser.UpdateAsync(updatedUser);
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
