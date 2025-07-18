using JogosApi.Data;
using JogosApi.Models;
using JogosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("JogoConnection");

var issuer = "http://127.0.0.1:5500";
var audience = "http://127.0.0.1:5500";
var key = "SAOhsd0ASDhaASDs0GAGhshHA7sVAh08AS";

builder.Services.AddDbContext<JogoContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddSingleton(new KeyEncryptionService("ASdas98asdJASD9h"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

        ValidateIssuer = true,
        ValidIssuer = issuer,

        ValidateAudience = true,
        ValidAudience = audience,

        ClockSkew = TimeSpan.FromMinutes(2) // produção mais seguro
    };
});

builder.Services.AddIdentity<Usuario, IdentityRole>().AddEntityFrameworkStores<JogoContext>().AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ADICIONEI ISSO PARA PODER PUXAR NO SITE
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsLocalhost", policy =>
    {
        policy.WithOrigins(
                "http://127.0.0.1:5500",
                "http://localhost:5500"// Live Server, http-server, etc.
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
//-----------------------------------------

var app = builder.Build();

//ISSO AQUI TAMBEM
app.UseCors("CorsLocalhost");
//-----------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
