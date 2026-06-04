# Add a feature in 20 minutes (with your agent)

This walks through the exact loop from [`CLAUDE.md`](../CLAUDE.md), using a real change: **add a
`priority` to notes and let callers filter by it.** Give your agent the prompt, then watch it follow
the rails. The point isn't the feature; it's the *loop*.

## The prompt

> Add an optional `priority` field to notes (`low` | `normal` | `high`, default `normal`) and a
> `GET /api/notes?priority=high` filter. Write the contract test first. Keep endpoints thin.

## 1. Test first (red)

The agent adds a contract test in `tests/Api.Tests` that asserts the new behavior before any
production code exists:

```csharp
[Fact]
public async Task Notes_can_be_filtered_by_priority()
{
    var client = factory.CreateClient();
    await client.PostAsJsonAsync("/api/notes", new { title = "Urgent", body = "", priority = "high" });
    await client.PostAsJsonAsync("/api/notes", new { title = "Whenever", body = "" });

    var high = await client.GetFromJsonAsync<List<Note>>("/api/notes?priority=high");

    Assert.NotNull(high);
    Assert.All(high!, n => Assert.Equal("high", n.Priority));
}
```

`dotnet test` → red. Good. Now we know exactly what "done" means.

## 2. Touch the model

`src/Api/Notes/Note.cs` gains one field. Records make this a one-liner:

```csharp
public record Note(Guid Id, string Title, string Body, string Priority, DateTimeOffset CreatedAt);
public record CreateNoteRequest(string? Title, string? Body, string? Priority);
```

## 3. Keep the endpoint thin

In `NotesEndpoints.cs`, the agent normalizes/validates the input and passes a filter to the store -
no business logic leaks into the route:

```csharp
group.MapGet("/", (string? priority, INoteStore store) => Results.Ok(store.All(priority)));
```

The validation (allowed values, default) sits next to the other rules in the POST handler. The
storage detail (how filtering works) lives in `InMemoryNoteStore` behind `INoteStore` - exactly where
the guardrails say it should.

## 4. Green, format, done

```bash
dotnet test       # all green, including the new test
dotnet format     # no changes
```

Run the **Definition of done** checklist in `CLAUDE.md`. Every box ticked. Ship it.

---

That is the entire workflow the full **[Sharpyard](https://sharpyard.dev)** kit is built around -
just scaled to auth, billing, multi-tenancy, and the rest, with the same thin-endpoints,
test-first, agent-on-rails discipline.
