# CLAUDE.md - operating guide for AI coding agents

> Read automatically by Claude Code; works as context for Copilot / Cursor too. This file is the
> single source of truth for how to work in this repo. Keep it short, current, and honest - an agent
> only follows what it can read in a few hundred tokens.

## What this is

A minimal, production-shaped **.NET 10 minimal API** (an in-memory Notes API), built to be driven by
an AI agent. It is intentionally tiny so the *operating layer* - this file, the conventions, the MCP
config, and the contract tests - is the thing worth studying and reusing.

Free taste of **Sharpyard**, the AI-agent-native .NET + Angular SaaS kit: https://sharpyard.dev

## Architecture (the whole map)

- `src/Api` - the web app.
  - `Program.cs` - composition root: registers services, maps endpoints. **Read this first.**
  - `Notes/` - one feature, the template to copy:
    - `Note.cs` - model + request records.
    - `INoteStore.cs` / `InMemoryNoteStore.cs` - the persistence boundary (an interface + an impl).
    - `NotesEndpoints.cs` - the routes (`MapNotesEndpoints`).
- `tests/Api.Tests` - contract tests that boot the real app in-memory with
  `WebApplicationFactory<Program>` and drive it over HTTP. They pin **behavior**, not internals.

No database, no auth, no DI gymnastics. If you reach for those, you're rebuilding the paid kit - stop.

## Commands (use these, don't guess)

| Task   | Command |
|--------|---------|
| Build  | `dotnet build` &nbsp;(warnings are errors - see `Directory.Build.props`) |
| Test   | `dotnet test` |
| Run    | `dotnet run --project src/Api` → open the printed URL + `/openapi/v1.json` |
| Format | `dotnet format` &nbsp;(run before calling a change done) |

## Conventions

- **C#**: nullable reference types on, file-scoped namespaces, `records` for data, `sealed` classes by
  default, `var` when the type is obvious, latest language version.
- **Endpoints are thin**: validate → call the store/service → map a result. No business logic in the
  endpoint lambda.
- **Persistence stays behind `INoteStore`.** Endpoints never touch storage directly. Swap the impl freely.
- **No new NuGet package** without a one-line reason in the PR. This stays lean on purpose.
- Public types and non-obvious code get an XML-doc one-liner explaining *why*, not *what*.

## Adding a feature (the loop)

1. Read `Notes/` end to end - it is the template.
2. Write or extend a **contract test first** in `tests/Api.Tests` (red).
3. Add a feature folder (`Things/`): model → `IThingStore` + in-memory impl → thin endpoints.
4. Register the service and call `app.MapThingsEndpoints()` in `Program.cs`.
5. `dotnet test` green → `dotnet format` → done.

Full worked example: [`docs/add-a-feature-in-20-min.md`](docs/add-a-feature-in-20-min.md).

## Definition of done (check every box)

- [ ] `dotnet build` and `dotnet test` are green.
- [ ] New/changed behavior is covered by a contract test.
- [ ] No endpoint holds business logic or touches storage directly.
- [ ] No new package without a stated reason.
- [ ] `dotnet format` reports no changes.

## Guardrails (do not)

- Do **not** add auth, a database, multi-tenancy, or billing - that's the full kit's job, not this starter's.
- Do **not** weaken `TreatWarningsAsErrors`, or delete/skip a failing test to go green. Fix the cause.
- Do **not** edit anything under `bin/` or `obj/`.
- **Token discipline**: read `Program.cs` and the one relevant feature folder, not the whole repo.

## MCP

Curated config in [`.mcp.json`](.mcp.json): filesystem, git, and (recommended) a Roslyn-aware C#
language server so the agent understands the *solution*, not just the text. Setup: [`docs/mcp.md`](docs/mcp.md).
