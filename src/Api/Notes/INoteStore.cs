namespace Api.Notes;

/// <summary>
/// Persistence boundary for notes. Kept as an interface so an agent can add a real
/// implementation (EF Core, Dapper, ...) without touching the endpoints or tests.
/// </summary>
public interface INoteStore
{
    IReadOnlyList<Note> All();
    Note? Find(Guid id);
    Note Add(string title, string body);
}
