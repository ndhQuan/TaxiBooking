using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using TaxiBooking.Models;
using TaxiBooking.Models.DTO;
using TaxiBooking.Repository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaxiBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJourneyRepository _dbJourney;
        private readonly IUserRepository _dbUser;
        private readonly ITaxiRepository _dbTaxi;
        protected APIResponse _response;
        public JourneyController(IJourneyRepository dbJourney, IUserRepository dbUser, ITaxiRepository dbTaxi, IMapper mapper)
        {
            _dbJourney = dbJourney;
            _dbUser = dbUser;
            _dbTaxi = dbTaxi;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet(Name = "GetJourney")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetJourneys([FromQuery]string phone, bool isCustomer = true)
        {
            try
            {
                if(!string.IsNullOrEmpty(phone))
                {
                    var user = await _dbUser.GetAsync(u => u.PhoneNumber == phone);
                    if (user == null)
                    {
                        return BadRequest();
                    }

                    if (isCustomer)
                    {
                        var customerJourney = await _dbJourney.GetAllAsync(u => u.CustomerId == user.Id);
                        if (customerJourney == null)
                        {
                            _response.IsSuccess = false;
                            return NotFound();
                        }
                        _response.Result = _mapper.Map<List<JourneyDTO>> (customerJourney);
                        _response.StatusCode = HttpStatusCode.OK;
                        return Ok(_response);
                    }
                    else
                    {
                        var driverJourney = await _dbJourney.GetAllAsync(u => u.DriverId == user.Id);
                        if (driverJourney == null)
                        {
                            _response.IsSuccess = false;
                            return NotFound();
                        }
                        _response.Result = _mapper.Map<List<JourneyDTO>>(driverJourney);
                        _response.StatusCode = HttpStatusCode.OK;
                        return Ok(_response);
                    }
                    
                }
                IEnumerable<JourneyLog> Journey = await _dbJourney.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<JourneyDTO>>(Journey);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[HttpPost]
        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> CreateJourney([FromBody]JourneyCreateDTO createDTO)
        //{
        //    try
        //    {
        //        if (await _dbJourney.GetAsync(t => t.DriverId == createDTO.DriverId) != null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "Driver already Exists!");
        //            return BadRequest(ModelState);
        //        }

        //        if (await _dbUser.GetAsync(t => t.Id == createDTO.DriverId) == null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "Driver Id is Invalid!");
        //            return BadRequest(ModelState);
        //        }

        //        if (await _dbTaxi.GetAsync(t => t.TaxiId == createDTO.BienSoXe) == null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "LicensePlate is Invalid!");
        //            return BadRequest(ModelState);
        //        }

        //        if (createDTO == null)
        //        {
        //            return BadRequest(createDTO);
        //        }
        //        Journey newState = _mapper.Map<Journey>(createDTO);
        //        await _dbJourney.CreateAsync(newState);
        //        _response.IsSuccess = true;
        //        _response.StatusCode = System.Net.HttpStatusCode.Created;
        //        _response.Result = newState;
        //        return CreatedAtRoute("GetJourney", new { id = newState.DriverId }, _response);
        //    }
        //    catch(Exception ex) 
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages.Add(ex.Message);
        //    }
        //    return _response;
        //}

        //[Authorize]
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<APIResponse>> UpdateJourney(string id, [FromBody]JourneyUpdateDTO updateDTO)
        //{
        //    try
        //    {
        //        if (updateDTO == null || id != updateDTO.DriverId)
        //        {
        //            return BadRequest();
        //        }

        //        var Journey = _dbJourney.GetAsync(u=>u.DriverId == id);
        //        if(Journey == null)
        //        {
        //            return NotFound();
        //        }

        //        Journey newState = _mapper.Map<Journey>(updateDTO);
        //        await _dbJourney.UpdateAsync(newState);
        //        _response.IsSuccess = true;
        //        _response.StatusCode = System.Net.HttpStatusCode.NoContent;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}



    }
}
