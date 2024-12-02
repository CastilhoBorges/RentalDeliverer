﻿namespace RentalDeliverer.src.Services
{
    public class MotoCreateService(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<Motorcycle> CreateMotoAsync(MotoCreateRequest request)
        {
            var identifier = request.Identificador;
            var year = request.Ano;
            var model = request.Modelo;
            var licensePlate = request.Placa;

            bool plateExists = await _context.Motorcycles.AnyAsync(m => m.LicensePlate == licensePlate);

            if (plateExists)
            {
                throw new Exception($"A placa {licensePlate} ja esta cadastrada");
            }

            var moto = new Motorcycle
            {
                identifier = identifier,
                Year = year,
                Model = model,
                LicensePlate = licensePlate,
            };

            await _context.Motorcycles.AddAsync(moto);
            await _context.SaveChangesAsync();

            var motoCreatedEvent = new MotoCreated
            {
                Year = year,
                LicensePlate = licensePlate
            };

            await _publishEndpoint.Publish(motoCreatedEvent);

            return moto;
        }
    }
}
