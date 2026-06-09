# .NET + Claude Code starter

[![CI](https://github.com/Khavel/dotnet-claude-starter/actions/workflows/ci.yml/badge.svg)](https://github.com/Khavel/dotnet-claude-starter/actions/workflows/ci.yml)
[![.NET 10](https://img.shields.io/badge/.NET-10-512BD4)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A tiny, production-shaped **.NET 10 minimal API** built to be driven by an **AI coding agent** (Claude Code, Copilot, Cursor). The code is deliberately small. The point is the *operating layer* around it: a tuned [`CLAUDE.md`](CLAUDE.md), an [`AGENTS.md`](AGENTS.md), a curated [MCP config](.mcp.json), and contract tests that let an agent refactor without fear.

It is the free taste of **[Sharpyard](https://sharpyard.dev)** - the AI-agent-native .NET + Angular SaaS starter kit. Same conventions, scaled up to auth, multi-tenancy, billing, and the rest.

## What's inside

```
CLAUDE.md            ← how an agent should work here (read this)
AGENTS.md            ← the same, for Copilot / Cursor
.mcp.json            ← curated MCP servers: filesystem + git (optional Roslyn C# server in docs/mcp.md)
.editorconfig        ← C# conventions the agent follows
src/Api/             ← a minimal API: in-memory Notes, organized by feature folder
tests/Api.Tests/     ← contract tests that boot the real app and pin its behavior
docs/                ← "add a feature in 20 minutes" walkthrough + MCP notes
```

## Quickstart

```bash
git clone https://github.com/Khavel/dotnet-claude-starter
cd dotnet-claude-starter

dotnet test                       # 4 contract tests, all green
dotnet run --project src/Api      # then open the printed URL + /openapi/v1.json
```

Then open the folder in Claude Code (or your agent of choice) and try:

> Add a `priority` field to notes and a `GET /api/notes?priority=high` filter. Write the contract test first.

The agent reads `CLAUDE.md`, copies the `Notes/` feature pattern, keeps the tests green, and stays on rails. That is the whole idea.

## Why this exists

AI agents perform best on small, well-documented, well-tested codebases with clear conventions. Most .NET repos give an agent none of that. This one is the opposite: a worked example of an **AI-native .NET** project you can copy the *shape* of.

See [`docs/add-a-feature-in-20-min.md`](docs/add-a-feature-in-20-min.md) for the full loop.

## Want the whole thing?

This is the 1%. The full **[Sharpyard](https://sharpyard.dev)** kit is a production .NET 10 + Angular SaaS foundation on these same conventions: auth + RBAC + multi-tenancy, billing via Merchant-of-Record, transactional email, admin, background jobs, Docker, CI/CD - plus the tuned agent layer wired throughout.

**→ [Join the Sharpyard waitlist](https://list.sharpyard.dev/?utm_source=github&utm_medium=readme&utm_campaign=dotnet-claude-starter)** to lock in founding-access pricing and early access to the full kit.

*Lo mismo en español: un starter de .NET pensado para que lo maneje tu agente de IA. [Únete a la lista de espera](https://list.sharpyard.dev/?utm_source=github&utm_medium=readme&utm_campaign=dotnet-claude-starter&utm_content=es) para reservar el precio fundador.*

## License

MIT - see [LICENSE](LICENSE). Use it for anything, including commercial work.
