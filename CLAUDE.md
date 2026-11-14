# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

@AGENTS.md

The imported `AGENTS.md` above covers stack, layout, conventions and gotchas. This file adds only what isn't there.

## Project context
- Academic 2D 1v1 fighting game prototype set in a Mexican public university. **Docs, UI strings, menu items and log messages are in Spanish**; keep new user-facing text and docs in Spanish (code identifiers stay in English).
- `Docs/PROJECT_MASTER.md` is the scope source of truth: §11 lists the *proposed* script names/responsibilities (`FighterController`, `FighterInput`, `Health`, `CombatResolver`, `CPUController`, `RoundManager`, `CombatHUD`, ...), §12 the fighter state machine (start with Idle/Moving/Attacking/Hitstun/KO), §13 combat rules, §18 the phase plan (Phase 2 = arena + player movement is next). Work one phase at a time.
- Code should stay simple and readable for someone learning Unity — no enterprise patterns, no third-party packages without justification.

## Running / validating
There is no CLI build or lint step; work happens in the Unity Editor (6000.6.0f1). No asmdefs exist, so scripts compile into `Assembly-CSharp` / `Assembly-CSharp-Editor`.

Phase 1 validation can run headless (exits 1 and logs `PHASE1_VALIDATION_FAILED` on error, logs `PHASE1_VALIDATION_OK` on success):

```
"<Unity 6000.6.0f1>/Editor/Unity.exe" -batchmode -nographics -quit -projectPath "C:/Users/corey/Flighting Rivals" -executeMethod FightingRivals.EditorTools.Phase1Validator.ValidatePhase1 -logFile -
```

Scene regeneration: `FightingRivals.EditorTools.Phase1SceneBuilder.BuildPhase1` (same `-executeMethod` pattern). It **overwrites** both scenes, so never run it once scenes have manual edits.

`com.unity.test-framework` is installed but there are no test assemblies yet. Manual Play-mode checklists live in `Docs/TEST_PLAN.md`; update it when a phase adds testable behavior.

## Architecture notes
- **Scenes are generated from code.** `Assets/_Project/Editor/Phase1SceneBuilder.cs` builds both scenes' hierarchy (camera, 1920x1080 Canvas, EventSystem with `InputSystemUIInputModule`, panels, buttons with persistent listeners) programmatically, and `Phase1Validator.cs` asserts that structure by object name. Renaming scene objects or changing wiring can break the validator — update both tools together, or note the divergence once scenes are hand-edited.
- UI uses legacy `UnityEngine.UI.Text` (not TextMeshPro).
- Scene flow: each scene has its own `GameManager` (no `DontDestroyOnLoad`). UI controllers (`MainMenuController`, `ResultPanelController`) hold a serialized `GameManager` ref and fall back to `FindAnyObjectByType<GameManager>()`. Scene names are constants on `GameManager`.
- `ResultPanelController.ShowVictory/ShowDefeat` are currently driven by temporary debug buttons in FightScene; future round logic (e.g. `RoundManager`) should call these instead, and the debug panel should be removed then.
