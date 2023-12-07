using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using TaxiBooking.Models;
using TaxiBooking.Models.DTO;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiTypeController : ControllerBase
    {
        private readonly ITaxiTypeRepository _dbTaxiType;
        protected APIResponse _response;
        public TaxiTypeController(ITaxiTypeRepository dbTaxiType)
        {
            _dbTaxiType = dbTaxiType;

            _response = new();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetTaxiTypes()
        {
            try
            {
                IEnumerable<TaxiType> taxiTypes = await _dbTaxiType.GetAllAsync();
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = taxiTypes;
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
        public async Task<ActionResult<APIResponse>> CreateTaxiType([FromQuery] int id,int Seats = 4,float Cost = 0)
        {
            try
            {
                if (await _dbTaxiType.GetAsync(t => t.Id == id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
                    return BadRequest(ModelState);
                }
                if (id == 0)
                {
                    return BadRequest();
                }

                TaxiType newType = new()
                {
                    Id = id,
                    NumberOfSeat = Seats,
                    Cost = Cost
                };

                await _dbTaxiType.CreateAsync(newType);
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                _response.Result = newType;
                return CreatedAtRoute(new { id = newType.Id }, _response);
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
        public async Task<ActionResult<APIResponse>> UpdateTaxiType(int id, [FromBody]TaxiType taxiTypeUpdate)
        {
            try
            {
                if (taxiTypeUpdate == null || id != taxiTypeUpdate.Id)
                {
                    return BadRequest();
                }

                await _dbTaxiType.UpdateAsync(taxiTypeUpdate);
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
