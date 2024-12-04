using RentalDeliverer.src.Services.DelivererS;

namespace RentalDeliverer.src.Controllers.Deliverer
{
    [Route("/locacao")]
    [ApiController]
    public class DelivererRentalMotoController(DelivererRentalMotoService delivererRentalMotoService) : ControllerBase
    {
        private readonly DelivererRentalMotoService _delivererRentalMotoService = delivererRentalMotoService;

        [HttpPost]
        public async Task<ActionResult> RentalMoto(RentalMotoRequest request)
        {
            try
            {
                await _delivererRentalMotoService.RentalMotoAsync(request);
                return StatusCode(201);
            }
            catch (Exception ex) 
            {
                //return BadRequest(new { message = "Dados inválidos" });
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
