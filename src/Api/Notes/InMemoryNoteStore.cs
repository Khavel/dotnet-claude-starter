using System.Collections.Concurrent;

namespace Api.Notes;

/// <summary>
/// Thread-safe in-memory store. Fine for the starter and for tests; swap it for a
/// database-backed implementation in the full kit (the interface stays the same).
/// </summary>
public sealed class InMemoryNoteStore : INoteStore
{
    private readonly ConcurrentDictionary<Guid, Note> _notes = new();

    public IReadOnlyList<Note> All() =>
        _notes.Values.OrderByDescending(n => n.CreatedAt).ToList();

    public Note? Find(Guid id) =>
        _notes.TryGetValue(id, out var note) ? note : null;

    public Note Add(string title, string body)
    {
        var note = new Note(Guid.NewGuid(), title, body, DateTimeOffset.UtcNow);
        _notes[note.Id] = note;
        return note;
    }
}
