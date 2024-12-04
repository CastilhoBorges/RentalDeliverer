namespace RentalDeliverer.src.Services.DelivererS
{
    public class DelivererRentalMotoService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Guid> RentalMotoAsync(RentalMotoRequest request)
        {
            var moto = await MotorcycleExist(request.Moto_id);
            var typeRental = await TypeRentalQuery(request.Plano) ?? throw new Exception("Tipo de locação não existe");
            var typeCnhOk = await IsCnhTypeA(request.Entregador_id) ? true : throw new Exception("CNH diferente de A");
            var initTomorrow = IsOneDayAfter(DateTime.Now, request.Data_inicio);
            
            if (!initTomorrow) throw new Exception("Data de inicio invalida!");

            var rental = new Rental
            {
                MotorcycleId = request.Moto_id,
                DelivererId = request.Entregador_id,
                RentalTypeId = typeRental.RentalTypeId,
                StartDate = request.Data_inicio,
                EndDate = request.Data_termino,
                ExpectedEndDate = request.Data_previsao_termino
            };

            await _context.AddAsync(rental);
            await _context.SaveChangesAsync();

            return rental.RentalId;
        }
        
        private async Task<RentalType> TypeRentalQuery(int type)
        {
            var rentalType = await this._context.RentalTypes
                .FirstOrDefaultAsync(rt => rt.Days == type);

            return rentalType;
        }

        private static bool IsOneDayAfter(DateTime dateNow, DateTime dateRegister)
        {
            var dateExpected = dateNow.Date.AddDays(1);
            return dateExpected == dateRegister.Date;
        }

        private async Task<bool> IsCnhTypeA(string id)
        {
            var deliverer = await this._context.Deliverers.FindAsync(id) ?? throw new Exception("Entregador não existe");
            return deliverer.CNHType == "A";
        }

        private async Task<Motorcycle> MotorcycleExist(string id)
        {
            var moto = await this._context.Motorcycles.FindAsync(id) ?? throw new Exception("Moto não existe");
            return moto;
        }
    }
}
