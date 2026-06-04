# MCP setup

[Model Context Protocol](https://modelcontextprotocol.io) servers give your agent real tools instead of
guesswork. [`.mcp.json`](../.mcp.json) ships two that work out of the box:

| Server | What it gives the agent | Needs |
|--------|-------------------------|-------|
| `filesystem` | Scoped read/write to this repo | Node (`npx`) |
| `git` | History, diffs, blame, staged changes | [`uv`](https://docs.astral.sh/uv/) (`uvx`) |

Claude Code picks `.mcp.json` up automatically. For Cursor/Copilot, point their MCP settings at the
same two servers.

## Add a Roslyn-aware C# server (recommended)

The biggest win for .NET is a server that understands the **solution** semantically - symbols,
references, types - not just the text. That lets the agent rename safely, find real call sites, and
reason about the compiler's view. A good community option is
[SharpTools (`kooshi/SharpToolsMCP`)](https://github.com/kooshi/SharpToolsMCP), a Roslyn-powered MCP
server for analyzing and editing C# solutions. Follow its README to add it to `.mcp.json`, pointed at
`DotnetClaudeStarter.slnx`.

> The full **[Sharpyard](https://sharpyard.dev)** kit ships this wired and documented, plus a Postgres
> MCP server for the database tier - so the agent can inspect schema and data as it works.

## A Postgres server (for when you add a database)

This starter has no database on purpose. When you add one (in your own project, or with the full kit),
a Postgres MCP server lets the agent read the schema before writing a query. Add it like:

```jsonc
"postgres": {
  "command": "npx",
  "args": ["-y", "@modelcontextprotocol/server-postgres", "postgresql://localhost/yourdb"]
}
```
