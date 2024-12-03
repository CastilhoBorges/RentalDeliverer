using RentalDeliverer.src.Services.MotorcycleS;

namespace RentalDeliverer.src.Controllers.Motorcycle
{
    [Route("/motos")]
    [ApiController]
    public class MotoListController(MotoListService motoListService) : ControllerBase
    {
        private readonly MotoListService _motoListService = motoListService;

        [HttpGet]
        public async Task<ActionResult> ListMoto([FromQuery] MotoFilterByPlateParams request)
        {
            try
            {
                var response = await _motoListService.ListMotoAsync(request);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
