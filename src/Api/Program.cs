using Api.Notes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<INoteStore, InMemoryNoteStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // OpenAPI document at /openapi/v1.json
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }))
   .WithTags("Health");

app.MapNotesEndpoints();

app.Run();

// Exposed so the test project can boot the app in-memory via WebApplicationFactory<Program>.
public partial class Program { }
