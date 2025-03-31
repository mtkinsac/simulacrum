# Simulacrum Phase 3.0 — Vale Task Board

## Purpose
Phase 3.0 focuses on integrating interactive UX experiences across Simulacrum’s Study, Balcony, and Gaming Nexus. All systems are now moving from modular prototypes to cohesive cross-module interactivity and AI-enhanced features.

---

## Vale's Deliverables

### 🧠 Narrative Systems & Interaction

- [x] `ValeDialogueManager.cs` update: integrated GPT callback, TTS fallback, and avatar transition effects.
- [x] `DialogueContextBuilder.cs` and `AdaptiveDialogueContextBuilder.cs`: modular prompt composition, narrative memory integration.
- [x] `LoreInteractionManager.cs`: expanded memory object trigger functionality.
- [x] `ValeOfflineTemplateGenerator.cs`: graceful offline fallback for dialogue prompts.
- [x] `ValeDialogueTriggerAPI.cs`: external system hook-in for on-demand dialogue.

---

### 🗂️ Data & Memory Management

- [x] `SessionMemoryManager.cs`: improved codex/hologram session persistence and replay.
- [x] `ProfileManager.cs` & `UserProfile.cs`: JSON-driven profile creation, fallback handling.
- [x] `SettingsManager.cs`: new user preference system with keyboard layout, locomotion toggles.
- [x] `MemoryEchoData.cs`: structured ScriptableObject for lore metadata and references.

---

### 🎭 Avatar & Transition Enhancements

- [x] `ValeTransitionController.cs`: refined fade in/out system using `Image`-based UI controller.
- [x] Idle behavior system prep for Vale autonomy (future roadmap).

---

### 📁 Systems Infrastructure

- [x] EventManager.cs: new global events including `OnValeSpeakRequest`, `OnMonitorClosed`, `OnProfileLoaded`.
- [x] SummonableContentEvents.cs: dedicated event dispatcher for summonable interactions.
- [x] IntegrationManager.cs: central bridge for global events.
- [x] PrefabRegistry.cs: central registry for named prefabs in runtime use.

---

### 🧪 UX "Straw Dog" Infrastructure

- [x] `SimulatedSummonUI.cs`: OnGUI debug UI for testing scene transitions, monitor spawns, and command routing.
- [x] `SummonableMonitorController.cs`: runtime spawning of desk/fireplace/roundtable monitors with dismissal logic.
- [x] `CallboxController.cs`: Phase 3.0 upgrade to manage destinations and summon relay logic.

---

### 🧙‍♂️ Gandor Interaction System

- [x] `WandGestureListener.cs`: G-key gesture listener (stub for future wand tracking).
- [x] `GandorSummonController.cs`: prefab summoning, voice line routing, and FX playback.

---

## Notes

- All files organized under:  
  `Simulacrum/Assets/Scripts/{Managers, Narrative, Avatar, Input, UI, Persistence}`  
- Prefabs referenced via public inspector slots or `PrefabRegistry.cs`.
- All public events use UnityEvent or custom delegates with clean unsubscribe patterns.

---
