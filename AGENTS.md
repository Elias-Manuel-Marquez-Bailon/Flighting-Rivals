# AGENTS.md

## Stack
- Unity **6000.6.0f1**, URP 2D, **Input System 1.20.0**
- 2D fighting game prototype; product `Fighting Rivals`, Android package `com.fightingrivals.game` (landscape locked, ref 1920x1080)
- Repo root is Unity project (`Flighting Rivals/` folder name is a typo; keep it)

## Layout
- **All game content**: `Assets/_Project/` (Art/Audio/Data/Prefabs/Scenes/Scripts/Settings/UI). Do **not** mix with imported packages/templates.
- Scenes: `Assets/_Project/Scenes/MainMenu.unity`, `Assets/_Project/Scenes/FightScene.unity`. `Assets/Scenes/SampleScene.unity` is template-only (not in build).
- Entry points: `MainMenu.unity` → `JUGAR` loads `FightScene.unity` (no character select yet).
- Core scripts: `Assets/_Project/Scripts/Core/GameManager.cs` (scene load/restart), UI in `Scripts/UI/`.

## Commands / Editor tools
- **Build scenes**: `Fighting Rivals > Construir escenas del prototipo` (`GameSceneBuilder`; overwrites MainMenu/FightScene — only if no manual changes; creates prefabs/data assets only if missing)
- **Validate**: `Fighting Rivals > Validar escenas del prototipo` (`GameSceneValidator`; checks Build Settings, data, prefabs, refs, buttons)
- **Regenerate fighter prefabs**: `Fighting Rivals > Regenerar prefabs de luchadores (sobrescribe)` (deletes art assigned to prefabs)
- Build Settings must include `MainMenu` and `FightScene` (the builder enforces this).

## Current state (playable prototype, Phases 2–6 without Android)
- Menu → fight vs CPU → KO/time-out → result (Ganaste/Perdiste/Empate) → retry/menu. Pause (Esc) and music/SFX volume (Options).
- Placeholder visuals (colored squares from `Art/Placeholders/Square.png`); art/audio teammate fills slots in prefabs, `FighterVisual`, `FighterAudio`, `AudioManager`, `AttackData`.
- `GameManager` uses `SceneManager`, resets `Time.timeScale = 1f` on load.
- Input: `Assets/_Project/Settings/FightingRivalsControls.inputactions` (maps `Fighter`, `Game`); UI uses `InputSystemUIInputModule` defaults. Touch controls (Phase 7) should use On-Screen Controls bound to the gamepad paths already in the asset.
- Scripts (one responsibility each): `Characters/` FighterController, FighterAttack, FighterVisual, FighterState; `Combat/` Health, AttackHitbox, CombatResolver (only place damage is applied); `Controls/` PlayerInputHandler; `AI/` CPUController; `Core/` GameManager, RoundManager; `UI/` CombatHUD, HealthBarUI, PauseMenuController, ResultPanelController, MainMenuController, OptionsController; `Audio/` AudioManager, FighterAudio; `Arena/` ArenaBounds; `Data/` ScriptableObjects.

## Conventions
- Namespaces: `FightingRivals.Core`, `FightingRivals.UI`, `FightingRivals.EditorTools` etc.
- Prefer editing existing components; don't duplicate systems. Share fighter logic between player/CPU, only change input source.
- UI must not compute/apply damage directly — go through combat systems.
- Hitboxes: activate only during active window, prevent multi-hit per attack on same target.
- Block is dedicated button (front block only, no chip damage). Keyboard/touch must feed the same logical actions.
- Multiplatform: design for Windows now, Android later (landscape). Don't hardcode keyboard-only in fighter logic.

## Git y colaboración (trabajo en equipo — obligatorio)
- **Nunca** commit ni push directo a `main`/`master`. Revisar `git branch` antes de tocar nada; si estás en `main`/`master`, crear rama: `feature/nombre-corto` (mecánicas/contenido), `fix/nombre-corto` (bugs), `art/nombre-corto` (sprites, animaciones, UI, audio).
- Commits con **Conventional Commits en español e imperativo**: `feat:`, `fix:`, `art:`, `refactor:`, `chore:`. Pequeños y atómicos; no mezclar código con escenas en el mismo commit si no hace falta.
- **Preguntar siempre** al usuario antes de `git commit` o `git push`, mostrando qué se va a incluir.
- Escenas (`.unity`) y prefabs (`.prefab`) compartidos generan conflictos: **avisar antes de modificarlos** para coordinar con el equipo (esto incluye ejecutar `GameSceneBuilder`, que reescribe ambas escenas).
- Nunca borrar ni ignorar archivos `.meta`; al crear o mover un asset, incluir su `.meta` en el mismo commit.
- Verificar que `.gitignore` de Unity existe y funciona antes de `git add` (no deben entrar `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `*.csproj`, `*.slnx`).

## Gotchas
- Folder name typo is intentional historically — don't rename repo folder.
- Don't add online/store/RPG/features from later phases. Priority: 1v1 vs CPU, win/lose/restart.
- Use ScriptableObjects in `Assets/_Project/Data/` for configurable values (health/damage/time). Avoid scattered magic numbers.
- External assets need license recorded in `Docs/ASSET_SOURCES.md` (no ripped commercial assets).
- Read before editing: check existing implementations first (`GameSceneBuilder` is the reference for UI/scene patterns).

## Docs
- Full plan/phases/architecture: `Docs/PROJECT_MASTER.md` (source of truth for scope)
- Assets: `Docs/ASSET_SOURCES.md`
- Tests: `Docs/TEST_PLAN.md`
- README: quick start (open with Unity 6000.6.0f1)
