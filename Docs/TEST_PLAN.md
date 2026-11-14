# Plan de pruebas

Las pruebas combinan validación automática en batch y comprobaciones
manuales en el editor. Cada fase debe probarse antes de avanzar a la
siguiente.

## Fase 0 --- Preparación

-   [x] El proyecto importa y compila en Unity 6000.6.0f1 sin errores.
-   [x] El Input System sigue activo (Project Settings > Player > Active
    Input Handling = Input System Package).
-   [x] `Product Name` es `Fighting Rivals`.
-   [x] El identificador Android es `com.fightingrivals.game`.
-   [x] La orientación está limitada a horizontal y la autorrotación está
    desactivada.
-   [x] `Docs/PROJECT_MASTER.md`, `Docs/ASSET_SOURCES.md` y
    `Docs/TEST_PLAN.md` existen.
-   [x] `git status` no muestra `Library/`, `Temp/`, `Logs/`,
    `UserSettings/` ni archivos `.csproj`.

## Fase 1 --- Esqueleto navegable

Validación automática (`Phase1Validator`, ejecutada en batch):

-   [x] `MainMenu.unity` y `FightScene.unity` están en Build Settings.
-   [x] Ambas escenas tienen cámara ortográfica, Canvas 1920×1080,
    EventSystem con `InputSystemUIInputModule` y textos con fuente.
-   [x] `GameManager` existe en ambas escenas.
-   [x] Los paneles de Opciones, Créditos y Resultado inician ocultos.
-   [x] El panel temporal de resultado inicia visible.
-   [x] Todos los botones de navegación tienen eventos conectados.
-   [x] Resultado: `PHASE1_VALIDATION_OK`.

Pruebas manuales en Play mode:

-   [ ] Jugar abre `FightScene`.
-   [ ] El simulador temporal muestra "Ganaste" y "Perdiste".
-   [ ] Reintentar recarga la pelea sin duplicar objetos.
-   [ ] Menú principal regresa desde el resultado.
-   [ ] Opciones y Créditos se abren y cierran sin errores.
-   [ ] La UI se ve legible en la ventana Game y en 1920×1080.

La pausa se prueba en la Fase 6, cuando exista.

## Fases posteriores

Los criterios funcionales completos están en la sección 19 del documento
maestro. Este archivo se ampliará cuando existan combate, audio y build
Android.
