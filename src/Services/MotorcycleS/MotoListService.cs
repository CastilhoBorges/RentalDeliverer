namespace RentalDeliverer.src.Services.MotorcycleS
{
    public class MotoListService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Motorcycle[]> ListMotoAsync(MotoFilterByPlateParams request)
        {
            var plate = request.Placa;
            
        }
    }
}
