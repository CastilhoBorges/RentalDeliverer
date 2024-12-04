namespace RentalDeliverer.src.Services.DelivererS
{
    public class DelivererImgUpdateService(ApplicationDbContext context, BlobServiceClient blobServiceClient)
    {
        private readonly BlobServiceClient _blobServiceClient = blobServiceClient;
        private readonly ApplicationDbContext _context = context;

        public async Task<object> ImageUpdateAsync(string id, ImageUpdateRequest imgUpdate)
        {
            var deliverer = await _context.Deliverers.FindAsync(id) ?? throw new Exception("Deliverer Não existe");
            var string64 = imgUpdate.Imagem_cnh.Split(",");

            string header = string64[0];
            string data64 = string64[1];

            string format = header.Split(';')[0].Split('/')[1];

            byte[] imgBytes = Convert.FromBase64String(data64);

            if (format != "png" && format != "bmp") throw new Exception("Arquivo Não permitido");

            string PngOrBmp = format == "png" ? ".png" : ".bmp";

            var containerName = "storageimg";
            var blobName = $"{deliverer.CNH}_cnh.{PngOrBmp}";

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = new MemoryStream(imgBytes))
            {
                await blobClient.UploadAsync(stream);
            }

            var cnhImagePath = blobClient.Uri.ToString();

            deliverer.CNHImgPath = cnhImagePath;

            await _context.SaveChangesAsync();

            return new { mensagem = "Foto da CNH Registrada" };
        }
    }
}
