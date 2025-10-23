using Employees.Factory;
using Employees.Factory.Interfaces;
using Employees.FileReader;
using Employees.FileReader.Interfaces;
using Employees.Services;
using Employees.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICollectionFactory, CollectionFactory>();
builder.Services.AddTransient<IFileReader, FileReader>();
builder.Services.AddTransient<IFileService, FileService>();

//var orig = builder.Configuration.GetSection("AllowSpecificOrigin").Get<List<string>>();
builder.Services.AddCors(options =>
{
	options.AddPolicy("POLICY", builder =>
	{
		builder
		.WithOrigins("http://localhost:4200")
			.AllowAnyHeader()
			.AllowAnyMethod();

		
	})
	;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("POLICY");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
