using VendasNuvemShopApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddHttpClient<NuvemShopService>();
builder.Services.AddSingleton<GoogleSheetsService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
