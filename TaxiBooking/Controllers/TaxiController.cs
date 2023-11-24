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

namespace TaxiBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaxiRepository _dbTaxi;
        private readonly ITaxiTypeRepository _dbTaxiType;
        protected APIResponse _response;
        public TaxiController(ITaxiRepository dbTaxi, ITaxiTypeRepository dbTaxiType, IMapper mapper)
        {
            _dbTaxi = dbTaxi;
            _dbTaxiType = dbTaxiType;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetTaxis()
        {
            try
            {
                IEnumerable<Taxi> taxi = await _dbTaxi.GetAllAsync(includeProperties:"Type");
                
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<TaxiDTO>>(taxi);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTaxi([FromBody]TaxiCreateDTO createDTO)
        {
            try
            {
                if (await _dbTaxi.GetAsync(t => t.TaxiId == createDTO.TaxiId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Taxi already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbTaxiType.GetAsync(t => t.Id == createDTO.TypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Taxi type Id is Invalid!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                Taxi newTaxi = _mapper.Map<Taxi>(createDTO);
                await _dbTaxi.CreateAsync(newTaxi);
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                _response.Result = newTaxi;
                return CreatedAtRoute(new { id = newTaxi.TaxiId }, _response);
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
            }
            return _response;
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateTaxi(string id, [FromQuery]int LoaiXe)
        {
            try
            {
                if (await _dbTaxiType.GetAsync(u => u.Id == LoaiXe) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "TaxiType ID is Invalid!");
                    return BadRequest(ModelState);
                }

                Taxi taxi = new Taxi() { TaxiId = id, TypeId = LoaiXe};

                await _dbTaxi.UpdateAsync(taxi);
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
