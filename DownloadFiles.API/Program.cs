using DownloadFiles.Library.Services;

var builder = WebApplication.CreateBuilder(args);

string accountName = builder.Configuration["AzureStorage:AccountName"];
string accountKey = builder.Configuration["AzureStorage:AccountKey"];
string containerName = builder.Configuration["AzureStorage:ContainerName"];

// Register BlobService with DI
builder.Services.AddSingleton<IBlobService>(new BlobService(accountName, accountKey, containerName));

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
