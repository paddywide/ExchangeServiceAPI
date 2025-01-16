using Api;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();
builder.Services.AddAppDI(builder.Configuration);
builder.Services.AddCorsPolicy();

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
