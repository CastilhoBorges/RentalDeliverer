using RentalDeliverer.src.Services.DelivererS;

namespace RentalDeliverer.src.Controllers.Deliverer
{
    [Route("/entregadores/{id}/cnh")]
    [ApiController]
    public class DelivererImgUpdateController(DelivererImgUpdateService delivererImgUpdate) : ControllerBase
    {
        private readonly DelivererImgUpdateService _delivererImgUpdate = delivererImgUpdate;

        [HttpPost]
        public async Task<ActionResult> ImageUpdate([FromRoute] string id,[FromBody] ImageUpdateRequest request)
        {
            try
            {
                var response = await _delivererImgUpdate.ImageUpdateAsync(id, request);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
