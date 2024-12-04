using RentalDeliverer.src.Services.DelivererS;

namespace RentalDeliverer.src.Controllers.Deliverer
{
    [Route("locacao/{id}/devolucao")]
    [ApiController]
    public class DelivererDateEndAndPriceController(DelivererDateEndAndPriceService delivererDateEndAndPriceService) : ControllerBase
    {
        private readonly DelivererDateEndAndPriceService _delivererDateEndAndPriceService = delivererDateEndAndPriceService;

        [HttpPut]
        public async Task<ActionResult> GetPrice([FromRoute] Guid id, [FromBody] DateEndRequest dateEnd)
        {
            try
            {
                var result = await _delivererDateEndAndPriceService.GetPriceAsync(dateEnd, id);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(new {mensagem = ex.Message});
            }
        }
    }
}
