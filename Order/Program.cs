using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order;
using Order.Repository;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

builder.Services.AddDbContext<OrderDbContext>(optionsAction: options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowAllOrigins",
    //    builder =>
    //    {
    //        builder.AllowAnyOrigin()
    //               .AllowAnyMethod()
    //               .AllowAnyHeader();
    //    });
    options.AddPolicy("AllowGatewayOrigin",
    builder =>
    {
        builder.WithOrigins("https://localhost:7069")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseCors("AllowAllOrigins");
app.UseCors("AllowGatewayOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
