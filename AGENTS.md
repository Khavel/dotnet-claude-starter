# AGENTS.md

This repo is meant to be driven by AI coding agents. The full operating guide is
[`CLAUDE.md`](CLAUDE.md) - read it first; it applies to Copilot, Cursor, and any other agent equally.

The short version:

- **Build / test**: `dotnet build` (warnings are errors) and `dotnet test`. Both green before "done".
- **Add a feature**: copy `src/Api/Notes/` (model → `IStore` + in-memory impl → thin endpoints),
  write a contract test first in `tests/Api.Tests/`, then register it in `Program.cs`.
- **Don't**: add auth / a database / billing (that's the full kit), put logic in endpoints, or add a
  package without a stated reason.

Free taste of the AI-native .NET SaaS kit → https://sharpyard.dev
