using Serilog;
using TABP.API.Extensions;
using TABP.API.Extensions.DependencyInjection;
using TABP.API.Middlewares;
using TABP.Application.Extensions.DependencyInjection;
using TABP.Infrastructure.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.BindConfiguration(builder.Configuration);

builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterUtilites();
builder.Host.AddLoggingService();
builder.Services.AddRateLimitingService();

builder.Services.AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseMiddleware<Blacklist>();
app.UseAuthorization();
app.UseExceptionHandler();

app.MapControllers()
    .RequireRateLimiting(nameof(RateLimitingPolicies.FixedWindow));

app.Run();