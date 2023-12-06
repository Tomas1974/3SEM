
using System.Text;
using api;
using api.Middleware;
using infrastructure.Repositories;

using System.Text;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using service;
using service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EmailService = service.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
        dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString);
}

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<PasswordHashRepository>();
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<AvatarService>();
builder.Services.AddSingleton<AvatarRepository>();
builder.Services.AddSingleton<CustomerBuyRepository>();
builder.Services.AddSingleton<CustomerBuyService>();
builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<SearchService>();
builder.Services.AddSingleton<SearchRepository>();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<EmailRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var frontEndRelativePath = "./../frontend/www";
builder.Services.AddSpaStaticFiles(conf => conf.RootPath = frontEndRelativePath);




// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


//For JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => {
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
