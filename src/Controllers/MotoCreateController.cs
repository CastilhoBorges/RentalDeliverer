using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalDeliverer.src.Controllers
{
    [Route("adm/motos")]
    [ApiController]
    public class MotoCreateController(MotoCreateService motoCreateService) : ControllerBase
    {
        private readonly MotoCreateService _motoCreateService = motoCreateService;

        [HttpPost]
        public async Task<ActionResult> CreateMoto([FromBody] MotoCreateRequest request)
        {
            try
            {
                var motoCreated = await _motoCreateService.CreateMotoAsync(request);
                return Ok(motoCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
