using System.Text.RegularExpressions;

namespace RentalDeliverer.src.Services.DelivererS
{
    public class DelivererCreateService(ApplicationDbContext context,BlobServiceClient blobServiceClient)
    {
        private readonly BlobServiceClient _blobServiceClient = blobServiceClient;
        private readonly ApplicationDbContext _context = context;

        public async Task<string> CreateDelivererAsync(DelivererCreateRequest request)
        {
            bool cnpjExist = await _context.Deliverers.AnyAsync(d => d.CNPJ == request.cnpj);
            bool cnhExist = await _context.Deliverers.AnyAsync(d => d.CNH == request.numero_cnh);

            if (cnhExist || cnpjExist)
            {
                throw new Exception();
            }

            if (request.tipo_cnh != "A" && request.tipo_cnh != "B" && request.tipo_cnh != "AB")
            {
                throw new Exception();
            }

            var cleanHash = new Regex(@"^data:image\/[a-z]+;base64,").Replace(request.imagem_cnh, "");
            byte[] imageBytes = Convert.FromBase64String(cleanHash);

            var containerName = "storageimg";
            var blobName = $"{request.numero_cnh}_cnh.jpg";

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(blobName);

            using(var stream = new MemoryStream(imageBytes))
            {
                await blobClient.UploadAsync(stream);
            }

            var cnhImagePath = blobClient.Uri.ToString();

            var deliverer = new Deliverer
            {
                DelivererName = request.identificador,
                CNPJ = request.cnpj,
                CNH = request.numero_cnh,
                CNHType = request.tipo_cnh,
                Name = request.nome,
                BirthDate = request.data_nascimento,
                CNHImgPath = cnhImagePath,
            };

            await _context.Deliverers.AddAsync(deliverer);
            await _context.SaveChangesAsync();

            return "";

        }
    }
}
