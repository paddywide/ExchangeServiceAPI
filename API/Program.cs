using Api;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();
builder.Services.AddAppDI(builder.Configuration);
builder.Services.AddCorsPolicy();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

if (OperatingSystem.IsWindows()) // Enable EventLog only on Windows
{
    builder.Logging.AddEventLog();
}

//when deploy to container, it will listen 8080 by default, below line to force it
//if local pc install self-cert, then can add 5001
builder.WebHost.UseUrls("http://*:5000");

var app = builder.Build();
//app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TokenValidationMiddleware>();

app.UseCors("AllowReactApp");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
