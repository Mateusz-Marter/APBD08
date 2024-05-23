using Microsoft.AspNetCore.Mvc;

namespace APBD08.Controllers 
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;

        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            try
            {
                await _repository.DeleteClientAsync(id);
                return Ok("deleted");
            } 
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
    