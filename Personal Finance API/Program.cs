using System.Text;
using Finance.Application.Interfaces;
using Finance.Application.Security;
using Finance.Application.Services.Authentication;
using Finance.Application.Services.Notifications;
using Finance.Application.Services.Roles;
using Finance.Application.Services.Transactions;
using Finance.Application.Services.Users;
using Finance.Application.Services.WalletAccounts;
using Finance.Application.Validators.User;
using Finance.Core.Database;
using Finance.Core.Entities;
using Finance.Core.Exceptions;
using Finance.Core.Helper;
using Finance.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Personal_Finance_API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect Database
builder.Services.AddDbContext<PersonalFinanceDbContext>(i =>
    i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Generic Services
builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<WalletAccount>, GenericRepository<WalletAccount>>();
builder.Services.AddScoped<IGenericRepository<UserTransaction>, GenericRepository<UserTransaction>>();

// Role Services
builder.Services.AddScoped<IRoleService, RoleService>();

// User Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// WalletAccount Services
builder.Services.AddScoped<IWalletAccountService, WalletAccountService>();
builder.Services.AddScoped<IWalletAccountRepository, WalletAccountRepository>();

// Transaction Services
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Authentication Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
builder.Services.AddScoped<ITokenHasher, TokenHasher>();

// Notification Services
builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();

// Validator Services
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();


var jwtKey = builder.Configuration["JwtSecretKey"] ?? throw new JwtKeyNotFoundException("No JWT Secret Key was found");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://ltdluka.ge/",
            ValidAudience = "https://ltdluka.ge/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cyber Commerce API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();