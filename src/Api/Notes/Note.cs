namespace Api.Notes;

/// <summary>A single note. Immutable; the store is the only thing that creates these.</summary>
public record Note(Guid Id, string Title, string Body, DateTimeOffset CreatedAt);

/// <summary>Inbound payload for creating a note. Fields are nullable because JSON may omit them.</summary>
public record CreateNoteRequest(string? Title, string? Body);
