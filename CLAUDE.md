# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

@AGENTS.md

The imported `AGENTS.md` above covers stack, layout, conventions and gotchas. This file adds only what isn't there.

## Project context
- Academic 2D 1v1 fighting game prototype set in a Mexican public university. **Docs, UI strings, menu items and log messages are in Spanish**; keep new user-facing text and docs in Spanish (code identifiers stay in English).
- `Docs/PROJECT_MASTER.md` is the scope source of truth: §11 lists the *proposed* script names/responsibilities (`FighterController`, `FighterInput`, `Health`, `CombatResolver`, `CPUController`, `RoundManager`, `CombatHUD`, ...), §12 the fighter state machine (start with Idle/Moving/Attacking/Hitstun/KO), §13 combat rules, §18 the phase plan (Phases 2–6 done; Phase 7 = Android/touch is next). Work one phase at a time.
- Code should stay simple and readable for someone learning Unity — no enterprise patterns, no third-party packages without justification.

## Running / validating
There is no CLI build or lint step; work happens in the Unity Editor (6000.6.0f1). No asmdefs exist, so scripts compile into `Assembly-CSharp` / `Assembly-CSharp-Editor`.

Validation can run headless (exits 1 and logs `SCENE_VALIDATION_FAILED` on error, logs `SCENE_VALIDATION_OK` on success):

```
# Windows
"<Unity 6000.6.0f1>/Editor/Unity.exe" -batchmode -nographics -quit -projectPath "<ruta del proyecto>" -executeMethod FightingRivals.EditorTools.GameSceneValidator.ValidateScenes -logFile -
# Linux (Unity Hub): ~/Unity/Hub/Editor/6000.6.0f1/Editor/Unity con los mismos argumentos
```

On Linux the editor may fail with `libxml2.so.2: cannot open shared object file` on distros that ship only `libxml2.so.16`; point `LD_LIBRARY_PATH` at a directory containing a `libxml2.so.2` (don't modify system libraries). Opening the project on Linux also adds `com.unity.sdk.linux-*`/`com.unity.toolchain.*` to `Packages/manifest.json` and touches `.vscode/settings.json` — revert those, they are machine-specific.

Scene regeneration: `FightingRivals.EditorTools.GameSceneBuilder.BuildAll` (same `-executeMethod` pattern). It **overwrites** both scenes, so never run it once scenes have manual edits. Prefabs (`Prefabs/Characters/`) and data assets (`Data/`) are only created when missing.

The project has Enter Play Mode Options with **domain reload disabled**: static fields survive between play sessions, so reset statics explicitly (see `AudioManager.Instance`). A script recompile during Play mode wipes C# event subscriptions — if events stop firing in a test, check for a mid-play reload first.

`com.unity.test-framework` is installed but there are no test assemblies yet. Manual Play-mode checklists live in `Docs/TEST_PLAN.md`; update it when a phase adds testable behavior.

## Architecture notes
- **Scenes are generated from code.** `Assets/_Project/Editor/GameSceneBuilder.cs` builds both scenes (camera, 1920x1080 Canvas, EventSystem with `InputSystemUIInputModule`, arena, fighter prefab instances, HUD, pause/result panels, buttons with persistent listeners), the fighter prefabs, the placeholder sprite and the ScriptableObject data. `GameSceneValidator.cs` asserts that structure (partly by object name). Renaming scene objects or changing wiring can break the validator — update both tools together, or note the divergence once scenes are hand-edited.
- Fighter = one shared prefab layout: root (`Rigidbody2D`, `BoxCollider2D`, `Health`, `FighterAttack`, `FighterController`, `FighterAudio`, plus `PlayerInputHandler` **or** `CPUController`), child `Visual` (`SpriteRenderer` + `FighterVisual`), child `Hitbox` (`AttackHitbox`). Fighters ignore each other's physics collision; `FighterController.KeepApartFromOpponent` separates them and `ClampInsideArena` uses `ArenaBounds`.
- Damage is applied only in `CombatResolver.ResolveHit`. Attack timing is in seconds (`AttackData` startup/active/recovery); hitboxes use `Physics2D.OverlapBox` during the active window and track already-hit targets.
- `RoundManager` drives the round as an Update-based phase machine (Intro → Fighting → Ending → Finished); it calls `ResultPanelController.ShowVictory/ShowDefeat/ShowDraw`. A tie on time-out or a same-frame double KO is a draw.
- UI uses legacy `UnityEngine.UI.Text` (not TextMeshPro).
- Scene flow: each scene has its own `GameManager` (no `DontDestroyOnLoad`). UI controllers (`MainMenuController`, `ResultPanelController`) hold a serialized `GameManager` ref and fall back to `FindAnyObjectByType<GameManager>()`. Scene names are constants on `GameManager`.
