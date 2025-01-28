using TABP.API.Extensions;
using TABP.API.Extensions.DependencyInjection;
using TABP.API.Middlewares;
using TABP.Application.Extensions.DependencyInjection;
using TABP.Infrastructure.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();

builder.Services.BindConfiguration(builder.Configuration);

builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterUtilites();

builder.Services.AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<Blacklist>();
app.UseAuthorization();
app.UseExceptionHandler();



app.MapControllers();


app.Run();