namespace RentalDeliverer.src.Models.DTO
{
    public class RentalMotoRequest
    {
        public Guid Entregador_id { get; set; }
        public Guid Moto_id { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_termino { get; set; }
        public DateTime Data_previsao_termino { get; set; }
        public int Plano { get; set; }

    }
}
