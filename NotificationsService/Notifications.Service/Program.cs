using MediatR;
using Notifications.Service.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddSignalR();
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(p => {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.WithOrigins("http://localhost:8080");
        p.AllowCredentials();
    });
});
var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCloudEvents();

app.UseAuthorization();

app.MapSubscribeHandler();
app.MapControllers();
app.MapHub<NotificationHub>("/notifications");

app.Run();
