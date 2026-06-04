using System.Net;
using System.Net.Http.Json;
using Api.Notes;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.Tests;

/// <summary>
/// Contract tests: they boot the real app in-memory and exercise it over HTTP, so they pin
/// observable behavior rather than internals. This is the safety net that lets an agent
/// refactor freely: if these stay green, the API still does what callers expect.
/// </summary>
public class NotesApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Health_returns_ok()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Posting_a_note_then_fetching_it_round_trips()
    {
        var client = factory.CreateClient();

        var created = await client.PostAsJsonAsync(
            "/api/notes",
            new { title = "Ship the lead magnet", body = "Built by driving the agent." });
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);

        var note = await created.Content.ReadFromJsonAsync<Note>();
        Assert.NotNull(note);
        Assert.Equal("Ship the lead magnet", note!.Title);

        var fetched = await client.GetFromJsonAsync<Note>($"/api/notes/{note.Id}");
        Assert.NotNull(fetched);
        Assert.Equal(note.Id, fetched!.Id);
    }

    [Fact]
    public async Task Posting_a_note_without_a_title_is_rejected()
    {
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/notes", new { body = "No title." });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Fetching_an_unknown_note_returns_404()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync($"/api/notes/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
