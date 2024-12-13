using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WizStore.Auth;
using WizStore.Data;
using WizStore.Entities;
using WizStore.Helpers;
using WizStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WizardStoreDataContext")
    ?? throw new InvalidOperationException("Connection string 'WizardStoreDataContext' not found.")));

builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// configure HTTP request pipeline
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();



DataBaseManagmentService.MigrationInitialization(app);

//Test users
var testUsers = new List<User>
{
    new( 1, "Admin", "admin", BCrypt.Net.BCrypt.HashPassword("admin"), Role.Admin),
    new( 2, "Normal User", "normal_user", BCrypt.Net.BCrypt.HashPassword("user"), Role.User)
};

using var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
if (!dataContext.Users.Contains<User>(testUsers.First())) 
{
    dataContext.Users.AddRange(testUsers);
    dataContext.SaveChanges();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
