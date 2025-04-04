using Eformerapp.Data.Repositories;
using Eformerapp.Data.Repository.Interface;
using Eformerapp.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Eformerapp.Data.Entities;
using Eformerapp.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Eformerapp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add this to your Program.cs
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();



builder.Services.AddScoped<AuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserDto>().ReverseMap();
    cfg.CreateMap<User, CreateUserDto>().ReverseMap();
    cfg.CreateMap<User, UpdateUserDto>().ReverseMap();
    cfg.CreateMap<UserRole,UserRoleDto>().ReverseMap();
    cfg.CreateMap<UserRole,UpdateUserRoleDto>().ReverseMap();
    cfg.CreateMap<UserRole,CreateUserRoleDto>().ReverseMap();
});
// Add to Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add authorization
builder.Services.AddAuthorization();

// Add configuration for JWT
builder.Configuration.AddJsonFile("appsettings.json");
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
