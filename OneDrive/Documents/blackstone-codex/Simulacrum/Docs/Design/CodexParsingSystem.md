# Codex Parsing & Automation – System Design

**Owner:** Vale  
**Phase:** 3.2

## Purpose
Enable Simulacrum to automatically parse Codex entries and MemoryObjects from structured `.txt` and `.md` files using filename hints and internal tags.

## Supported Types
- `CodexEntry_*`: Parses ID, Summary, Category, Tags, Fallback Lore.
- `MemoryObject_*`: Parses Location and Lore fields.
- `CodexReflection_*`: Parses speaker-tagged dialogue lines.

## File Locations
- `Simulacrum/Docs/Narrative/[Realm]/`
- `Simulacrum/Docs/Narrative/Codex/Reflections/`

## Technical Hooks
- `CodexRegistrationManager` will be extended to load entries from disk at runtime or build time.
- Vale will maintain a CodexValidator utility to check for:
  - Missing IDs
  - Invalid categories
  - Empty tags or duplicate filenames

## Notes
Gem and Zephyr should continue tagging entries with:
- `CodexEntry:`
- `MemoryObject:`
in filename or header.
