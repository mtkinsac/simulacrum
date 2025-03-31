# Phase 3.2 – Codex System Expansion

**Architectural Lead:** Vale  
**Phase Objective:** Expand the Simulacrum Codex system to support dynamic registration, narrative tagging, and in-world reflection triggers.

## Overview
This phase focuses on refining the Codex system to serve as a centralized, extensible narrative database. The core deliverables include:

- Automatic Codex entry registration from `.txt`/`.md` sources
- Enhanced tag parsing (e.g., `[Realm]`, `[Character]`)
- Support for reflection dialogues triggered by codex events
- Structural validation for all Codex assets

## Integration Notes
Codex metadata will be parsed using naming conventions:
- `CodexEntry_*` for standard entries
- `MemoryObject_*` for lore-linked triggers
- `CodexReflection_*` for dialogue exchanges

All Codex entries will be linked to `MemoryEchoData` via metadata injection in this phase.
