using HealthChecks.UI.Client;
using LoginTokenTask;
using LoginTokenTask.Context;
using LoginTokenTask.Repository;
using LoginTokenTask.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddHealthChecks().AddCheck("Test HealthCheck", () => HealthCheckResult.Healthy("Server is healthy"));
builder.Services.AddHealthChecks().AddCheck("DB healthCheck", () => DatabaseHealthCheck.Check(""));
builder.Services.AddHealthChecks().
    AddSqlServer(builder.Configuration.GetConnectionString("localServerConnection"));


builder.Services.AddHealthChecksUI(chk =>
{
    chk.SetEvaluationTimeInSeconds(5); //time in seconds between check    
    chk.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
    chk.SetApiMaxActiveRequests(1); //api requests concurrency    
    chk.AddHealthCheckEndpoint("default api", "/health"); //map health check api (here path is wrong so it will we unhealthy)   
})
      .AddInMemoryStorage();



builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new HeaderApiVersionReader("x-GadgetApi-version");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGadgetRepository, GadgetRepository>();
//builder.Services.AddScoped<IDataProtector>();
builder.Services.AddScoped<IGadgetService, GadgetService>();

//authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var LocalDbConnection = builder.Configuration.GetConnectionString("localServerConnection");
builder.Services.AddDbContext<LoginDbContext>(u => u.UseSqlServer(LocalDbConnection));


//for looger
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapHealthChecks("/health");
//app.MapHealthChecksUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthcheck", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();
app.MapControllers();

app.Run();
