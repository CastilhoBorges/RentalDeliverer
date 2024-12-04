namespace RentalDeliverer.src.Services.DelivererS
{
    public class DelivererRentalMotoService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<string> RentalMotoAsync(RentalMotoRequest request)
        {
            var typeRental = await TypeRentalQuery(request.Plano) ?? throw new Exception();
            var initTomorrow = isOneDayAfter(DateTime.Now, request.Data_inicio.AddDays(1));
            
            if (!initTomorrow) throw new Exception();

            return "Teste";
        }
        
        private async Task<RentalType> TypeRentalQuery(int type)
        {
            var rentalType = await this._context.RentalTypes
                .FirstOrDefaultAsync(rt => rt.Days == type);

            return rentalType;
        }

        private bool isOneDayAfter(DateTime dateNow, DateTime dateRegister)
        {
            var dateExpected = dateNow.AddDays(1);
            return dateExpected.Date == dateRegister.Date;
        }
    }
}
