using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using TaxiBooking.Models;
using TaxiBooking.Models.DTO;
using TaxiBooking.Repository.IRepository;
using TaxiBooking.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaxiBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverStateController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDriverStateRepository _dbDriverState;
        private readonly IUserRepository _dbUser;
        private readonly ITaxiRepository _dbTaxi;
        protected APIResponse _response;
        public DriverStateController(IDriverStateRepository dbDriverState, IUserRepository dbUser, ITaxiRepository dbTaxi, IMapper mapper)
        {
            _dbDriverState = dbDriverState;
            _dbUser = dbUser;
            _dbTaxi = dbTaxi;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{driverId}", Name = "GetDriverState")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetDriverState(string driverId)
        {
            try
            {
                DriverState driverState = await _dbDriverState.GetAsync(includeProperties:"Taxi");
                
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = driverState;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateDriverState([FromBody]DriverStateCreateDTO createDTO)
        {
            try
            {
                if (await _dbDriverState.GetAsync(t => t.DriverId == createDTO.DriverId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Driver already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbUser.GetAsync(t => t.Id == createDTO.DriverId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Driver Id is Invalid!");
                    return BadRequest(ModelState);
                }

                if (await _dbTaxi.GetAsync(t => t.TaxiId == createDTO.BienSoXe) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "LicensePlate is Invalid!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                DriverState newState = _mapper.Map<DriverState>(createDTO);
                await _dbDriverState.CreateAsync(newState);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = newState;
                return CreatedAtRoute("GetDriverState", new { driverId = newState.DriverId }, _response);
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
            }
            return _response;
        }

        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDriverState(string id, [FromBody]DriverStateUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null)
                {
                    return BadRequest();
                }
                var driverState = await _dbDriverState.GetAsync(u=>u.DriverId == id);
                if(driverState == null)
                {
                    return NotFound();
                }

                driverState.latCurrent = updateDTO.latCurrent;
                driverState.longCurrent = updateDTO.longCurrent;
                driverState.Duong = updateDTO.Duong;
                driverState.PhuongXa = updateDTO.PhuongXa;
                driverState.QuanHuyen = updateDTO.QuanHuyen;
                driverState.TinhTP = updateDTO.TinhTP;

                await _dbDriverState.UpdateAsync(driverState);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("UpdatePartial/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDriverStatus(string id, JsonPatchDocument<DriverStateUpdatePartialDTO> patchDTO)
        {
            if (patchDTO == null || id == null)
            {
                return BadRequest();
            }
            var driverState = await _dbDriverState.GetAsync(u => u.DriverId == id, tracked: false);

            if (driverState == null)
            {
                return BadRequest();
            }

            DriverStateUpdatePartialDTO driverStateDTO = _mapper.Map<DriverStateUpdatePartialDTO>(driverState);

            patchDTO.ApplyTo(driverStateDTO, ModelState);
            DriverState model = _mapper.Map<DriverState>(driverStateDTO);

            await _dbDriverState.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
