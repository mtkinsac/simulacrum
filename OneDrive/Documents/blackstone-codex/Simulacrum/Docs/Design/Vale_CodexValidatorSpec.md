# Vale Codex Validator – Specification

**Purpose:** Validate all `.txt`-based codex entries before registration.

## Checks
- All `CodexEntry_*` files must include:
  - `ID:`
  - `Summary:`
  - `Category:`
  - `Tags:` (at least one)
- All `MemoryObject_*` files must include:
  - `Location:`
  - `Lore:`

## Output
Validator will print warnings for:
- Duplicate IDs
- Unregistered categories
- Missing fields
- Files with invalid naming patterns

## Execution
This script can be run during editor time (Unity Editor tool) or at build time in CI/CD if desired.
