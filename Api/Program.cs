using Api.ViewModels;
using Data;
using Data.Interfaces;
using Data.Repositories;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

const string _corsPolicy = "EnableAll";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "api",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _corsPolicy,
                    builder =>
                    {
                        builder
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                        .AllowAnyOrigin();
                    });
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().Build());
});

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShelterDbContext>(options => options.UseSqlServer(connection));

ConfigureRepositories(builder.Services);
ConfigureServices(builder.Services);
ConfigureUtilities(builder.Services);

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

static void ConfigureRepositories(IServiceCollection services)
{
    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddScoped<ICompanyRepository, CompanyRepository>();
}

static void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<ICompanyService, CompanyService>();
}

static void ConfigureUtilities(IServiceCollection services)
{
    services.AddScoped<IApiObjectConverter, ApiObjectConverter>();
}