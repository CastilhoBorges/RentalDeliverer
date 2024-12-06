using RentalDeliverer.src.Services.DelivererS; 
using RentalDeliverer.src.Services.MotorcycleS;

var builder = WebApplication.CreateBuilder(args);

var mongoClient = new MongoClient(builder.Configuration["MongoDB:ConnectionString"]);
var mongoDatabase = mongoClient.GetDatabase(builder.Configuration["MongoDB:DatabaseName"]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<MotoCreateService>();
builder.Services.AddScoped<MotoListService>();
builder.Services.AddScoped<MotoUpdatePlateService>();
builder.Services.AddScoped<MotoDeleteService>();

builder.Services.AddScoped<DelivererCreateService>();
builder.Services.AddScoped<DelivererRentalMotoService>();
builder.Services.AddScoped<DelivererDateEndAndPriceService>();
builder.Services.AddScoped<DelivererImgUpdateService>();

builder.Services.AddSingleton(mongoDatabase);
builder.Services.AddAzureBlobStorage(builder.Configuration);
builder.Services.AddMassTransitWithRabbitMq(builder.Configuration);
builder.Services.AddScoped<MotoCreatedPublisher>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) // Faz rodar o swagger apenas em ambiente de dev
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redireciona requisições HTTP para HTTPS.

app.MapControllers(); // Configura os endpoints da API baseados nos controladores criados.

app.Run();