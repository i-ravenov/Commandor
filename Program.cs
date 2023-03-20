using Commandor.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddScoped<ICommandorRepo, MockCommandorRepo>();
builder.Services.AddScoped<ICommandorRepo, SqlCommandorRepo>();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Configuration.GetConnectionString("CommandorConnection");

builder.Services.AddDbContext<CommandorContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommandorConnection"));

});
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
