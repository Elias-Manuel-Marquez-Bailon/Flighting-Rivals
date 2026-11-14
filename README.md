# Fighting Rivals

Juego de lucha 2D 1 contra 1 ambientado en una universidad pública
mexicana. Prototipo académico en Unity.

## Estado

Fase 1 completada: menú navegable, `FightScene` de prueba y pantalla de
resultado con reinicio. El combate, la arena y la CPU todavía no están
implementados.

## Requisitos

-   Unity **6000.6.0f1**
-   URP 2D
-   Unity Input System 1.20.0 (incluido en `Packages/manifest.json`)

## Cómo abrir

1.  Abrir esta carpeta desde Unity Hub con la versión indicada.
2.  Abrir `Assets/_Project/Scenes/MainMenu.unity`.
3.  Ejecutar Play mode y usar **Jugar**.
4.  En `FightScene`, usar los botones temporales **Ganaste** / **Perdiste**
    para probar el flujo de resultado.

## Herramientas de editor

-   `Fighting Rivals > Fase 1 > Construir esqueleto navegable` regenera
    `MainMenu.unity` y `FightScene.unity`. Solo debe usarse mientras no
    haya cambios manuales en esas escenas.
-   `Fighting Rivals > Fase 1 > Validar esqueleto navegable` comprueba
    Build Settings, componentes, referencias y botones.

## Documentación

-   [Documento maestro](Docs/PROJECT_MASTER.md)
-   [Inventario de recursos](Docs/ASSET_SOURCES.md)
-   [Plan de pruebas](Docs/TEST_PLAN.md)

## Nota sobre la carpeta

La carpeta local se llama `Flighting Rivals` por un error tipográfico
heredado. El nombre oficial del juego es **Fighting Rivals**.
