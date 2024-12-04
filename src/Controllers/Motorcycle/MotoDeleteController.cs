using RentalDeliverer.src.Services.MotorcycleS;

namespace RentalDeliverer.src.Controllers.Motorcycle
{
    [Route("moto/{id}")]
    [ApiController]
    public class MotoDeleteController(MotoDeleteService motoDeleteService) : ControllerBase
    {
        private readonly MotoDeleteService _motoDeleteService = motoDeleteService;

        [HttpDelete]
        public async Task<ActionResult> DeleteMoto(string id)
        {
            try
            {
                await _motoDeleteService.DeleteMotoAsync(id);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
