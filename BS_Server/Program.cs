using BS_Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add Database to dependency injection
builder.Services.AddDbContext<BSDbContext>(
        options => options.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;Trusted_Connection=True;MultipleActiveResultSets=true"));

#region Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});
#endregion


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

#region Add Session
app.UseSession(); //In order to enable session management
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
