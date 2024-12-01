namespace RentalDeliverer.src.Controllers
{
    [Route("/user")]
    [ApiController]
    public class UserCreateController(UserCreateService userCreateService) : ControllerBase
    {
        private readonly UserCreateService _userCreateService = userCreateService;

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            try
            {
                var user = await _userCreateService.CreateUserAsync(request);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { error = "Erro interno do servidor." });
            }
        }
    }
}
