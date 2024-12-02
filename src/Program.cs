var builder = WebApplication.CreateBuilder(args);

var mongoClient = new MongoClient(builder.Configuration["MongoDB:ConnectionString"]);
var mongoDatabase = mongoClient.GetDatabase(builder.Configuration["MongoDB:DatabaseName"]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<MotoCreateService>();

builder.Services.AddSingleton(mongoDatabase);
builder.Services.AddScoped<MotoCreatedPublisher>();

builder.Services.AddMassTransitWithRabbitMq(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();