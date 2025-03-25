#!/bin/bash

# Create root project folder
mkdir -p Simulacrum/Assets

cd Simulacrum/Assets

# Top-level folders
mkdir Animations Data Docs Environment Prefabs Scenes Scripts Scripts_Extensions

# Data files
touch Data/LoreCodexSchema.json
touch Data/MemoryEchoData.cs

# Docs
mkdir -p Docs/Design
touch Docs/Design/MemorySystem.md
touch Docs/Design/ValeDialogueSystem.md
touch Docs/Phase2.0_AuditReport.md
touch Docs/Phase2.1_TaskBoard.md
touch Docs/Phase2.4_PlanningBoard.md
touch Docs/Phase2.4_TaskBoard.md
touch README.md

# Scripts
mkdir -p Scripts/{Avatar,Global,Interfaces,Input,Locomotion,Monitor,Narrative,Persistence,TTS,UI,UX}

# Avatar
touch Scripts/Avatar/ValeController.cs
touch Scripts/Avatar/ValeTransitionController.cs

# Global
touch Scripts/Global/EventManager.cs
touch Scripts/Global/SimulacrumCoreManager.cs
touch Scripts/Global/PrefabRegistry.cs
touch Scripts/Global/IntegrationManager.cs

# Interfaces
touch Scripts/Interfaces/Interfaces.cs

# Input
touch Scripts/Input/HandTrackingInput.cs
touch Scripts/Input/HapticFeedbackController.cs
touch Scripts/Input/XRInputRouter.cs

# Locomotion
touch Scripts/Locomotion/LocomotionSettingsManager.cs
touch Scripts/Locomotion/NaturalLocomotionController.cs
touch Scripts/Locomotion/SmoothLocomotionController.cs
touch Scripts/Locomotion/TeleportationController.cs

# Monitor
touch Scripts/Monitor/VirtualMonitorManager.cs
touch Scripts/Monitor/VirtualMonitor.cs
touch Scripts/Monitor/MonitorResizer.cs
touch Scripts/Monitor/DeskAnchorController.cs

# Narrative
touch Scripts/Narrative/StoryPortal.cs
touch Scripts/Narrative/MemoryHologram.cs
touch Scripts/Narrative/DocumentCard.cs
touch Scripts/Narrative/VoicePromptController.cs
touch Scripts/Narrative/LoreInteractionManager.cs
touch Scripts/Narrative/ValeDialogueManager.cs
touch Scripts/Narrative/DialogueContextBuilder.cs
touch Scripts/Narrative/ChatGPTConnector.cs

# Persistence
touch Scripts/Persistence/SettingsManager.cs
touch Scripts/Persistence/UserProfile.cs
touch Scripts/Persistence/ProfileManager.cs
touch Scripts/Persistence/SessionMemoryManager.cs

# TTS
touch Scripts/TTS/TTSController.cs

# UI
touch Scripts/UI/RadialSummonMenu.cs
touch Scripts/UI/TextBubbleUI.cs

# UX
touch Scripts/UX/UserSettingsPanelController.cs
touch Scripts/UX/QuickSlotController.cs
touch Scripts/UX/SummonFeedbackController.cs

# Environment
touch Environment/EnvironmentLightingController.cs

# Done
echo "✅ Simulacrum Studios folder structure created successfully."
