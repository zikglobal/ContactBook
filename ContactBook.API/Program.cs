using ContactBook.API.Data;
using ContactBook.API.Mapping;
using ContactBook.API.Model.Domain;
using ContactBook.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//for EntityFramework
builder.Services.AddDbContext<ContactBookDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ContacBookConnectionstring")));

builder.Services.AddScoped<IAppUserRepository, SQLAppUserRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//for identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ContactBookDbContext>()
    .AddDefaultTokenProviders();

//for authenticatin 

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});


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
