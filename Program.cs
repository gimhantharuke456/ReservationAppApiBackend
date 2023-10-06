using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReservationAppApi.Configurations;
using ReservationAppApi.Services;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
//services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<TravelerService>();
builder.Services.AddSingleton<ReservationService>();
builder.Services.AddSingleton<TrainService>();
//
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddControllers();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAny");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
