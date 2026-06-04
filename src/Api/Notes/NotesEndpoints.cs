namespace Api.Notes;

/// <summary>
/// All /api/notes routes live here. Endpoints are thin: validate, call the store, map a result.
/// This is the pattern to copy when an agent adds a new resource (see docs/add-a-feature-in-20-min.md).
/// </summary>
public static class NotesEndpoints
{
    private const int MaxTitleLength = 120;
    private const int MaxBodyLength = 10_000;

    public static IEndpointRouteBuilder MapNotesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/notes").WithTags("Notes");

        group.MapGet("/", (INoteStore store) => Results.Ok(store.All()));

        group.MapGet("/{id:guid}", (Guid id, INoteStore store) =>
            store.Find(id) is { } note ? Results.Ok(note) : Results.NotFound());

        group.MapPost("/", (CreateNoteRequest request, INoteStore store) =>
        {
            var title = request.Title?.Trim() ?? string.Empty;
            var body = request.Body?.Trim() ?? string.Empty;

            var errors = new Dictionary<string, string[]>();
            if (title.Length == 0)
                errors["title"] = ["Title is required."];
            else if (title.Length > MaxTitleLength)
                errors["title"] = [$"Title must be {MaxTitleLength} characters or fewer."];
            if (body.Length > MaxBodyLength)
                errors["body"] = [$"Body must be {MaxBodyLength} characters or fewer."];

            if (errors.Count > 0)
                return Results.ValidationProblem(errors);

            var note = store.Add(title, body);
            return Results.Created($"/api/notes/{note.Id}", note);
        });

        return app;
    }
}
