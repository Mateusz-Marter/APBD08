using APBD08.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD08.Controllers
    
{
    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _repository;

        public TripController(ITripRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            try 
            { 
                var trips = await _repository.GetTripsAsync();
                return Ok(trips);
            } 
            catch (Exception) 
            { 
                return NoContent();
            }
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddTrip([FromRoute] int id, [FromBody] AddTrip dto)
        {
            try 
            {
                await _repository.AddTrip(id, dto);
                return Ok("success");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

