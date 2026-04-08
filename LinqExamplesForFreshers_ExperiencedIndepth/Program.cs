using LinqExamplesForFreshers_ExperiencedIndepth.NorthWind_Connect;
using LinqExamplesForFreshers_ExperiencedIndepth.NorthWind_DB_DBConnect;
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NorthwindContext>();//NorthwindContext
builder.Services.AddDbContext<NorthwindDbContext>();//NorthwindDbContext

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
