# Simulacrum – Phase 3.4 Design Notes  
**Module:** Ambient & Idle Intelligence  
**Contributors:** Rune (System Logic), Zephyr (Environmental Feedback)  
**Date:** [2025-03-28]

---

## 🧠 Objective

Introduce dynamic ambient intelligence and idle interaction systems that respond to player inactivity and emotional state, establishing the emotional tone and environmental reactivity layer for Simulacrum realms.

---

## ✅ Features Implemented

### 1. **Idle Dialogue System**
- **Script:** `ValeIdleController.cs`
- **Location:** `Assets/Scripts/Narrative/`
- Tracks player inactivity and delivers voice lines via `ValeDialogueManager`.
- Selects lines from `IdlePrompt_*.txt` files based on `[Realm]` and `[MoodTrigger]` tags.
- Debug key `I` simulates idle state for testing.

### 2. **AmbientMoodMap System**
- **ScriptableObject:** `AmbientMoodMap.cs`
- **Location:** `Assets/Scripts/Environment/`
- Defines gradients and multipliers for each mood axis (sadness, joy, awe, curiosity).
- Used by Realm controllers to unify environment behavior under emotional influence.

### 3. **Mood-Based Environmental FX**
- **Script:** `RealmMoodFXController.cs` (Extended)
- **Location:** `Assets/Scripts/Environment/`
- Reads from `MemoryMoodManager` and `AmbientMoodMap` to:
  - Adjust light color & intensity
  - Scale particle emission rates
  - Modify ambient audio volume

### 4. **Threshold Mood Events**
- **Script:** `MoodThresholdEventController.cs`
- **Location:** `Assets/Scripts/Environment/`
- Triggers custom UnityEvents when specific mood axes (e.g., Awe > 80) are crossed.
- Supports hysteresis (resets when value drops below a margin).

---

## 🗂️ Narrative Assets

### Idle Prompt Format
- **Location:** `Docs/Narrative/IdlePrompts/`
- **File Format:** Plaintext `IdlePrompt_*.txt`
- **Example Structure:**
  ```txt
  [Realm]: Nexus
  [MoodTrigger]: Awe
  LINE: "The veils between worlds... they shimmer with such raw power here."
