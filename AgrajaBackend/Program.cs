using AgrajaBackend;
using AgrajaBackend.Controllers.Validators;
using AgrajaBackend.Services;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using jwtConst = AgrajaBackend.Constants.Config.JWT;
using corsConst = AgrajaBackend.Constants.Config.CORS;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
    {
        setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Version = "1.0",
            Title = "AGRAJA gestión de servicios de agricultura"
        });
        setup.IncludeXmlComments("doc\\api.xml");
    });

//builder.Services.AddSingleton<IConfigService, ConfigService>();

builder.Services.AddSingleton<ICriptoService, CriptoService>();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<ICitiesService, CitiesService>();
builder.Services.AddScoped<ICropTypesService, CropTypesService>();
builder.Services.AddScoped<IPayOptionsService, PayOptionsService>();
builder.Services.AddScoped<ICratesService, CratesService>();
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IFarmersService, FarmersService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<PersonDataValidator>();

var config = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = config.GetValue(jwtConst.KEY_VALIDATE_ISSUER, false),
        ValidateAudience = config.GetValue(jwtConst.KEY_VALIDATE_AUDIENCE, false),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = config.GetValue(jwtConst.KEY_AUDIENCE, ""),
        ValidIssuer = config.GetValue(jwtConst.KEY_ISSUER, ""),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue(jwtConst.KEY_KEY, ""))),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapControllers();

app.UseCors(c =>
{
    if(config.GetValue(corsConst.KEY_ALLOW_ANY_HEADER, true))
        c.AllowAnyHeader();
    
    if(config.GetValue(corsConst.KEY_ALLOW_ANY_METHOD, true))
        c.AllowAnyMethod();
    
    if(config.GetValue(corsConst.KEY_ALLOW_ANY_ORIGIN, true))
        c.AllowAnyOrigin();
    
    if(!config.GetValue(corsConst.KEY_WITH_ORIGINS, "").Equals(""))
        c.WithOrigins(config.GetValue<string>(corsConst.KEY_WITH_ORIGINS));
});

app.UseAuthentication();
app.UseAuthorization();

app.Run();
