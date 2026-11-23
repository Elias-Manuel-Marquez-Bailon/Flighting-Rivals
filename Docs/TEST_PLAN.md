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

## Fase 1 --- Esqueleto navegable (histórico)

La validación de la Fase 1 (`Phase1Validator`, `PHASE1_VALIDATION_OK`) fue
reemplazada por `GameSceneValidator` al llegar el combate real. El
simulador temporal "Ganaste / Perdiste" ya no existe.

## Fases 2 a 6 --- Prototipo jugable

Validación automática (`GameSceneValidator`, menú
`Fighting Rivals > Validar escenas del prototipo` o en batch):

-   [x] `MainMenu.unity` y `FightScene.unity` están en Build Settings.
-   [x] Existen los controles (`Fighter/Move`, `Jump`, `LightAttack`,
    `HeavyAttack`, `Block`, `Game/Pause`), los datos de luchadores, ataques,
    round y CPU, y los prefabs `PlayerFighter` / `CPUFighter`.
-   [x] `FightScene` tiene arena con suelo y límites, 2 luchadores con
    oponente asignado, `RoundManager`, HUD, 2 barras de vida, pausa y
    resultado conectados; paneles de pausa y resultado inician ocultos.
-   [x] Ya no existe el simulador temporal de resultado.
-   [x] Resultado: `SCENE_VALIDATION_OK`.

Prueba automática de pelea (ejecutada una vez en batch durante el
desarrollo, Play mode sin ventana): la CPU se acerca y ataca, ambos reciben
daño, el K.O. termina el round y se muestra el resultado (`SMOKE_OK`).

Pruebas manuales en Play mode (pendientes de hacer con teclado):

Movimiento y arena

-   [ ] A/D y flechas mueven al jugador; caminar hacia atrás es un poco más
    lento.
-   [ ] Espacio salta; no se puede saltar en el aire.
-   [ ] Ningún luchador atraviesa el suelo ni sale por los lados.
-   [ ] Los luchadores no se quedan uno dentro del otro, ni encima de la
    cabeza del otro.
-   [ ] Cada luchador gira para mirar al rival al cruzarse.

Ataques, daño y bloqueo

-   [ ] J lanza un ataque ligero; K uno pesado más lento. El rectángulo
    amarillo aparece solo durante la parte activa del golpe.
-   [ ] Un golpe quita vida una sola vez (50 ligero, 120 pesado).
-   [ ] El rival golpeado parpadea, retrocede y no puede actuar un momento.
-   [ ] Mantener L bloquea (luchador azulado): un golpe de frente no quita
    vida; un golpe por la espalda sí.
-   [ ] No se puede atacar ni moverse mientras se bloquea o se está
    aturdido.

CPU

-   [ ] La CPU se acerca, ataca con pausas, a veces bloquea y a veces
    retrocede.
-   [ ] La CPU no vibra en el sitio ni ataca en todos los frames.
-   [ ] La CPU puede ganar si el jugador no hace nada.

Round y resultado

-   [ ] Al iniciar aparece "¡PELEA!" y nadie se mueve hasta que desaparece.
-   [ ] El temporizador baja de 60 a 0.
-   [ ] Al llegar una vida a 0: "K.O.", el perdedor cae, nadie se mueve y
    luego aparece GANASTE o PERDISTE.
-   [ ] Al acabarse el tiempo: "¡TIEMPO!" y gana quien tenga más vida
    (EMPATE si es igual).
-   [ ] Reintentar reinicia la pelea con vida completa y controles
    funcionando; Menú principal regresa al menú.

Pausa y opciones

-   [ ] Esc o el botón PAUSA congelan la pelea (el tiempo no baja).
-   [ ] Continuar reanuda; Reintentar y Menú principal funcionan desde la
    pausa.
-   [ ] No se puede pausar con la pantalla de resultado visible.
-   [ ] En Opciones, los sliders de Música y Efectos se guardan al volver a
    entrar al juego (se escuchan cuando haya audio asignado).

## Fase 7 --- Android (pendiente)

Se añadirá cuando existan controles táctiles y una build de Android.
