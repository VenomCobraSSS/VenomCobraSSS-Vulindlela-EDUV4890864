using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Vulindlela.Booking.Api.Authorisation;
using Vulindlela.Booking.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlite(
        builder.Configuration.GetConnectionString("Data Source=Bookings.db")));

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddHttpClient<IClearanceClient, ClearanceClient>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CitizenRecordPolicy", policy =>
    {
        policy.Requirements.Add(new CitizenRecordRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, CitizenRecordHandler>();

builder.WebHost.UseUrls("http://localhost:5401");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db =
        scope.ServiceProvider.GetRequiredService<BookingDbContext>();

    db.Database.EnsureCreated();
}

app.Run();