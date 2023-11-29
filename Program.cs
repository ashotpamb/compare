using Compare.Data;
using Compare.Moc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddDbContext<DataContext>(options =>
{
    var concetionString = builder.Configuration.GetConnectionString("mysql");
    options.UseMySql(concetionString, ServerVersion.AutoDetect(concetionString));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var fileService = new FileService(app.Services.CreateScope().ServiceProvider);
// fileService.WriteToFile();
await fileService.ComapreAndAdd();
app.Run();
