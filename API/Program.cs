using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BTB.Service;
using BTB.Repository;
using BTB.Repository.Interfaces;
using Microsoft.OpenApi.Models;
using BTB.Data;
using Microsoft.EntityFrameworkCore;
using BTB.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connexion = builder.Configuration.GetConnectionString("BTBDatabase");
// Para EntityFramework
builder.Services.AddDbContext<BTBContext>(options => 
    options.UseMySQL(connexion));

// Para cloudinary
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings")
);


// Register application services / repositories
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPartidaRepository, PartidaRepository>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddScoped<ITierRepository, TierRepository>();
builder.Services.AddScoped<INodoRepository, NodoRepository>();
// Register service layer
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPartidaService, PartidaService>();
builder.Services.AddScoped<ITierService, TierService>();
builder.Services.AddScoped<INodoService, NodoService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();



// Configure JWT authentication
var jwtSecret = builder.Configuration["JWT:SecretKey"] ?? string.Empty;
var issuer = builder.Configuration["JWT:ValidIssuer"];
var audience = builder.Configuration["JWT:ValidAudience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

// Para la autentificacion en el swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });


    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


// Para el cors
string  MyAllowSpecificOrigins = "MiPoliticaDejaATodos";
builder.Services.AddCors(options =>
{
    //Se puede configuara para que unos pocos entren, pero aqui no es el caso
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MiPoliticaDejaATodos");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BTBContext>();
    db.Database.Migrate();
}

app.Run();
