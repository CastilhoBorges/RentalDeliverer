using RentalDeliverer.src.Services.MotorcycleS;

namespace RentalDeliverer.src.Controllers.Motorcycle
{
    [Route("/motos")]
    [ApiController]
    public class MotoCreateController(MotoCreateService motoCreateService) : ControllerBase
    {
        private readonly MotoCreateService _motoCreateService = motoCreateService;

        [HttpPost]
        public async Task<ActionResult> CreateMoto([FromBody] MotoCreateRequest request)
        {
            try
            {
                await _motoCreateService.CreateMotoAsync(request);
                return StatusCode(201);
            }
            catch 
            {
                return BadRequest(new { message = "Dados inválidos" });
            }
        }
    }
}
