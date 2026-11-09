using Microsoft.EntityFrameworkCore;
using Vulindlela.Clearance.Api.Data;
using Vulindlela.Clearance.Api.Repositories;
using Vulindlela.Clearance.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DocumentClearanceDbContext>(options =>
    options.UseSqlite("Data Source=Documents.db"));

builder.Services.AddScoped<IDocumentClearanceService, DocumentClearanceService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddSingleton<IDocumentQueue, DocumentQueue>();
builder.Services.AddHostedService<DocumentVerificationWorker>();
builder.WebHost.UseUrls("http://localhost:5402");

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider
             .GetRequiredService<DocumentClearanceDbContext>();

    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();