
using ProjectManagementSystem.API.Middlewares;
using ProjectManagementSystem.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureHosting(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();
