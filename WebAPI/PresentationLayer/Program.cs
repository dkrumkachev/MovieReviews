using PresentationLayer.Extensions;
using PresentationLayer.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
	opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSqlServerDbContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureBusinessLayerServices();
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureSwagger();


var app = builder.Build();

//var scope = app.Services.CreateScope();
//var dbcontext = (DataLayer.Data.DataContext)scope.ServiceProvider
//	.GetRequiredService(typeof(DataLayer.Data.DataContext));
//new WebAPI.Seed(dbcontext).SeedDataContext();

app.UseGlobalErrorHandler();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
