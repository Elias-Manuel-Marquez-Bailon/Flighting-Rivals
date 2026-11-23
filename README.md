# Fighting Rivals

Juego de lucha 2D 1 contra 1 ambientado en una universidad pública
mexicana. Prototipo académico en Unity.

## Estado

Prototipo jugable (Fases 2 a 6, sin Android): menú → pelea contra la CPU →
K.O. o tiempo → resultado (Ganaste / Perdiste / Empate) → reintentar o
volver al menú. Incluye pausa y volumen de música/efectos. Los luchadores y
el escenario son **rectángulos de color provisionales**; el arte y el audio
definitivos se agregan después sin tocar el código (ver más abajo).

## Requisitos

-   Unity **6000.6.0f1**
-   URP 2D
-   Unity Input System 1.20.0 (incluido en `Packages/manifest.json`)

## Cómo abrir y jugar

1.  Abrir esta carpeta desde Unity Hub con la versión indicada.
2.  Abrir `Assets/_Project/Scenes/MainMenu.unity`.
3.  Ejecutar Play mode y pulsar **Jugar**.

| Acción          | Teclado              | Control        |
| --------------- | -------------------- | -------------- |
| Mover           | A / D o flechas      | Stick / cruceta |
| Saltar          | Espacio, W o ↑       | A (sur)        |
| Ataque ligero   | J                    | X (oeste)      |
| Ataque pesado   | K                    | Y (norte)      |
| Bloquear        | L (mantener)         | RB             |
| Pausa           | Esc                  | Start          |

Las teclas se cambian en
`Assets/_Project/Settings/FightingRivalsControls.inputactions`.

## Herramientas de editor

-   `Fighting Rivals > Construir escenas del prototipo` regenera
    `MainMenu.unity` y `FightScene.unity`. **Sobrescribe las escenas**: solo
    debe usarse mientras nadie las haya editado a mano. Los prefabs y los
    datos solo se crean si no existen.
-   `Fighting Rivals > Validar escenas del prototipo` comprueba Build
    Settings, datos, prefabs, referencias y botones.
-   `Fighting Rivals > Regenerar prefabs de luchadores (sobrescribe)` borra y
    recrea los prefabs de los luchadores (se pierden sprites asignados).

## Dónde está cada cosa

| Qué                               | Dónde                                                |
| --------------------------------- | ---------------------------------------------------- |
| Vida, velocidad, salto            | `Assets/_Project/Data/Characters/` (PlayerStats, CPUStats) |
| Daño y tiempos de cada ataque     | `Assets/_Project/Data/Combat/` (LightAttack, HeavyAttack) |
| Duración del round                | `Assets/_Project/Data/RoundSettings`                 |
| Dificultad de la CPU              | `Assets/_Project/Data/CPUSettings`                   |
| Prefabs de los luchadores         | `Assets/_Project/Prefabs/Characters/`                |

## Para el equipo de arte y audio

-   **Sprites del luchador:** abrir el prefab (`PlayerFighter` o
    `CPUFighter`), seleccionar el hijo **Visual** y cambiar el sprite del
    `SpriteRenderer`. Dibujar mirando a la **derecha** (el código voltea solo)
    con el pivote en el centro del cuerpo. Cuando haya animaciones, desmarcar
    *Use Placeholder Effects* en `FighterVisual`.
-   **Animaciones:** agregar un `Animator` al hijo **Visual**. Los parámetros
    que envía el código están documentados al inicio de
    `Scripts/Characters/FighterVisual.cs`.
-   **Escenario:** los objetos dentro de **Arena** en `FightScene` (fondo,
    pizarrón, ventanas, suelo) se pueden reemplazar por sprites. No borrar
    `Floor` (su collider es el piso) ni `LeftBoundary`/`RightBoundary`
    (límites de la arena).
-   **Sonidos:** música y clic de UI en `AudioManager` (objeto
    `GameSystems` de cada escena); sonidos del luchador en `FighterAudio`
    (raíz del prefab); sonido al lanzar/conectar cada ataque en los archivos
    de `Data/Combat`. Un campo vacío simplemente no suena.
-   Registrar todo recurso externo en `Docs/ASSET_SOURCES.md`.

## Documentación

-   [Documento maestro](Docs/PROJECT_MASTER.md)
-   [Inventario de recursos](Docs/ASSET_SOURCES.md)
-   [Plan de pruebas](Docs/TEST_PLAN.md)

## Nota sobre la carpeta

La carpeta local se llama `Flighting Rivals` por un error tipográfico
heredado. El nombre oficial del juego es **Fighting Rivals**.
