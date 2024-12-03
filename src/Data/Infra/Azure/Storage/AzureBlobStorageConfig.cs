namespace RentalDeliverer.src.Data.Infra.Azure.Storage
{
    public static class AzureStorageConfig
    {
        public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {    
            var connectionString = configuration.GetSection("AzureStorage:StringConnection").Value;
            services.AddSingleton(sp => new BlobServiceClient(connectionString));
            return services;
        }
    }
}

