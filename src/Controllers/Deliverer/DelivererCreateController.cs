using RentalDeliverer.src.Services.DelivererS;

namespace RentalDeliverer.src.Controllers.Deliverer
{
    [Route("/entregadores")]
    [ApiController]
    public class DelivererCreateController(DelivererCreateService delivererCreateService) : ControllerBase
    {
        private readonly DelivererCreateService _delivererCreateService = delivererCreateService;
        
        [HttpPost]
        public async Task<ActionResult> CreateDeliverer(DelivererCreateRequest request)
        {
            try
            {
                await _delivererCreateService.CreateDelivererAsync(request);
                return Ok();
            }
            catch
            {
                return BadRequest(new { message = "Dados inválidos" });
            }
        }
    }
}
