# Fighting Rivals --- Documento maestro del proyecto

**Versión:** 0.3\
**Estado:** Fase 1 completada; esqueleto navegable sin combate\
**Motor:** Unity 6000.6.0f1 con URP 2D e Input System 1.20.0\
**Plataformas objetivo:** Windows y Android\
**Género:** Juego de lucha 2D, 1 contra 1\
**Idioma del documento:** Español\
**Nombre:** Fighting Rivals\
**Ubicación del documento:** `Docs/PROJECT_MASTER.md`\
**Repositorio:** carpeta local `Flighting Rivals` (nombre de carpeta heredado; no coincide con el nombre del juego)

------------------------------------------------------------------------

## 1. Propósito de este documento

Este archivo describe qué construir, cómo organizar el proyecto de
Unity, qué recursos hacen falta y en qué orden conviene desarrollarlo.
Está escrito para que una persona desarrolladora o una IA de
programación pueda comprender el contexto sin depender de conversaciones
anteriores.

**El proyecto ya tiene un esqueleto navegable, pero el combate todavía
no está implementado.** Las mecánicas, personajes y arena deben crearse
y validarse a partir de la Fase 2. La sección 1.1 registra el estado real
al cerrar la Fase 1.

La prioridad actual es conseguir un **prototipo pequeño, estable y
divertido**: abrir el juego, entrar a una pelea contra la computadora,
ganar o perder y poder reintentar. No se debe intentar completar el
Arcade entero antes de comprobar que el combate funciona.

### 1.1 Estado actual del proyecto (Fase 1 cerrada)

Verificado directamente en el proyecto local:

-   Unity **6000.6.0f1** con **URP 2D** e **Input System 1.20.0**
    activo (`activeInputHandler: 1`).
-   Estructura propia creada en `Assets/_Project/` (escenas, scripts,
    editor, prefabs, arte, audio, datos y settings).
-   Escenas **`MainMenu.unity`** y **`FightScene.unity`** creadas y
    añadidas a Build Settings. `SampleScene.unity` ya no forma parte de
    la build.
-   Scripts de Fase 1: `GameManager`, `MainMenuController` y
    `ResultPanelController`, en `Assets/_Project/Scripts/`.
-   Herramientas de editor: `Phase1SceneBuilder` (regenera el esqueleto)
    y `Phase1Validator` (comprueba escenas, referencias y botones).
-   Menú principal con **Jugar**, **Opciones** y **Créditos**; Opciones y
    Créditos son paneles dentro de `MainMenu`.
-   **Jugar** carga `FightScene`, que muestra un simulador temporal con
    botones **Ganaste** / **Perdiste** y un `ResultPanel` con
    **Reintentar** y **Menú principal**.
-   Validación automática en batch: `PHASE1_VALIDATION_OK` (Build
    Settings, componentes, Canvas 1920×1080, cámara ortográfica, fuentes,
    referencias y botones conectados).
-   **Pendiente de prueba manual:** recorrer el flujo en Play mode y
    revisar la legibilidad visual en la ventana Game.
-   **No existe todavía** movimiento, arena jugable, combate, CPU, vida,
    temporizador ni audio.
-   **Pendiente inmediato:** Fase 2 (arena y movimiento del jugador).

> Nota: la carpeta del repositorio se llama `Flighting Rivals` por un
> error tipográfico heredado. El nombre oficial del juego es
> **Fighting Rivals**. No renombrar la carpeta sin coordinar con el
> equipo, porque cambia rutas locales y proyectos de IDE.

## 2. Resumen del proyecto original

El documento del equipo presenta *Fighting Rivals*: un juego de pelea
humorístico ambientado en una universidad pública mexicana. La idea
original era un juego de lucha 3D con estudiantes caricaturizados,
escenarios del campus y un torneo de seis combates. Los personajes
representan personalidades y situaciones exageradas de un proyecto
académico.

La versión de este documento adapta ese concepto a un **fighter 2D de
vista lateral**, inspirado en la claridad de los juegos de lucha
clásicos. Esta decisión es intencional: reduce la complejidad de cámara,
profundidad, modelos 3D y animación, y permite concentrarse en
controles, impactos, defensa y comportamiento de la CPU.

### 2.1 Concepto actualizado

El jugador controla a un estudiante caricaturesco y pelea contra un
oponente controlado por la computadora. El combate sucede en una única
arena universitaria. El jugador puede moverse, saltar, atacar, bloquear
y esquivar mediante desplazamientos cortos. La pelea termina cuando
alguien pierde toda su vida o cuando el tiempo se agota. Al terminar, se
muestra una pantalla de victoria o derrota con la posibilidad de volver
a intentarlo.

### 2.2 Pilares de diseño

1.  **Combate comprensible:** los botones deben sentirse responsivos y
    cada golpe debe tener un resultado visible.
2.  **Lectura visual:** se distingue claramente al jugador, al rival,
    sus ataques, sus barras de vida y el estado del round.
3.  **Humor universitario:** personajes y efectos caricaturescos, sin
    necesitar cinemáticas largas.
4.  **Alcance realista:** primero una pelea completa; después se amplían
    personajes, escenarios y modos.
5.  **Código reutilizable:** los seis personajes compartirán el sistema
    de movimiento y combate; sus diferencias se configurarán mediante
    datos y animaciones.

## 3. Alcance actual y límites

### 3.1 Dentro del primer prototipo

-   Proyecto 2D de Unity.
-   Menú principal con **Jugar**, **Opciones** y **Créditos**.
-   Al pulsar **Jugar**, entrar directamente a la pelea. **No habrá
    selección de personaje todavía.**
-   Una sola arena de combate.
-   Un personaje controlado por el jugador y un rival controlado por
    CPU.
-   Movimiento horizontal, salto, ataque ligero, ataque pesado y
    bloqueo.
-   Esquiva o dash sencillo, si no compromete la estabilidad del combate
    básico.
-   Barras de vida, temporizador y estado del round.
-   Detección de golpes, daño, reacción al impacto y límites de la
    arena.
-   Condición de victoria y derrota.
-   Pantalla de resultado con **Reintentar** y **Menú principal**.
-   Efectos de sonido básicos y música temporal con controles de volumen
    o silencio.
-   Build de Windows.
-   Configuración y prueba de controles táctiles para Android durante el
    desarrollo; Android debe considerarse una meta explícita, no algo
    que se deje para el último día.

### 3.2 Diseñado para fases posteriores, no para el primer prototipo

-   Pantalla de selección de personaje.
-   Los seis personajes con arte y animaciones definitivos.
-   Arcade de seis combates, progreso entre rivales y jefe final.
-   Varias arenas.
-   Movimientos especiales únicos y medidor de energía.
-   Dificultades, entrenamiento, desbloqueables y guardado de progreso.
-   Versus local para dos personas.
-   Cinemáticas, doblaje completo y diálogos extensos.
-   Juego en línea, matchmaking, cuentas, tienda, mundo abierto o
    sistemas RPG.

**Regla de alcance:** no añadir funciones de la lista posterior hasta
que una pelea completa contra la CPU funcione de principio a fin.

## 4. Diseño de la pelea

### 4.1 Cámara y espacio

-   Cámara 2D ortográfica.
-   Vista lateral: ambos luchadores se enfrentan en un plano horizontal.
-   Movimiento principal en el eje X.
-   Salto en el eje Y.
-   No existe desplazamiento de profundidad ni sidestep 3D.
-   La arena limita cuánto puede alejarse cada luchador.
-   Los personajes deben girarse automáticamente para mirar al oponente.

### 4.2 Controles iniciales sugeridos para Windows

Controles propuestos para el prototipo:

  Acción                           Tecla inicial
  -------------------------------- --------------------------------------
  Mover a la izquierda / derecha   A / D o flechas
  Saltar                           Espacio
  Ataque ligero                    J
  Ataque pesado                    K
  Bloquear                         L
  Dash / esquiva                   Shift
  Pausa                            Escape

Decisiones cerradas en la Fase 0:

-   El bloqueo se activa con **botón dedicado** (L en teclado y un botón
    táctil en Android); no se implementa bloquear manteniendo atrás.
-   El bloqueo **anula el daño frontal** en el prototipo, sin chip
    damage. Los golpes por la espalda no se bloquean.
-   El **remapeo de teclas queda fuera del alcance** del prototipo. Las
    teclas se pueden cambiar desde el Input Action Asset, pero no habrá
    pantalla de configuración de controles.
-   El dash sigue siendo opcional y se decide al llegar a la Fase 5.

### 4.3 Controles Android

-   Botones táctiles para izquierda y derecha.
-   Botón de salto.
-   Botones de ataque ligero y pesado.
-   Botón de bloqueo.
-   Botón de dash, si se incluye.
-   Pausa.
-   La interfaz debe adaptarse a distintas proporciones de pantalla y no
    tapar barras ni temporizador.
-   **Orientación horizontal bloqueada** (decisión cerrada en Fase 0).
-   **Resolución de referencia 1920×1080**, con escalado por proporción.
    Probar al menos 16:9 y una pantalla alargada (19.5:9) para verificar
    que el HUD respeta el área segura y los recortes.
-   Los controles táctiles no deben tapar las barras de vida, el
    temporizador ni el botón de pausa.

Los controles de teclado y táctiles deben enviar acciones al mismo
sistema de combate. No se deben crear dos implementaciones diferentes de
las mecánicas.

### 4.4 Reglas iniciales del round

Usar valores de configuración editables, no números dispersos en muchos
scripts:

-   Vida inicial sugerida: 1000 puntos por luchador.
-   Tiempo inicial sugerido: 60 segundos.
-   El round termina si la vida llega a cero o si el tiempo llega a
    cero.
-   Si se agota el tiempo, gana quien tenga más vida.
-   Si ambos tienen la misma vida al agotarse el tiempo, declarar empate
    y aplicar una regla sencilla definida por el equipo; evitar que el
    combate se quede bloqueado.
-   Si ambos llegan a cero de vida casi al mismo tiempo (doble KO),
    declarar empate y detener el combate. No reiniciar automáticamente.
-   Para el prototipo se juega **un solo round** (decisión cerrada en
    Fase 0). `RoundManager` debe quedar preparado para ampliarse, pero
    sin implementar todavía el mejor de tres.
-   Cuando la pelea básica sea estable, pasar a formato de mejor de tres
    rounds: el primero que gane dos rounds gana el combate.

Estos valores son recomendaciones iniciales tomadas como referencia del
GDD original, no balance definitivo.

## 5. Personajes previstos

El roster conceptual pasa de cinco a **seis personajes**, porque se
añade el personaje del usuario. La primera versión no requiere selección
de luchador: se pueden utilizar un personaje provisional como jugador y
otro como CPU.

Los nombres y estilos siguientes conservan la intención del GDD. Deben
tratarse como conceptos que el equipo puede ajustar, no como diseños
finales cerrados.

  -----------------------------------------------------------------------
  Personaje               Concepto                Estilo posible
  ----------------------- ----------------------- -----------------------
  Fran --- «El            Ataques rápidos y       Presión ofensiva,
  Compilador»             referencias a           golpes rápidos y un
                          programación            proyectil temático a
                                                  futuro

  Ale --- «La Rúbrica»    Control del espacio con Alcance medio y ataques
                          carpeta y notas         para mantener distancia
                          adhesivas               

  Leo --- «Ctrl+Z»        Contraataques y         Defensa y castigo
                          reversas                después de bloquear

  Montes --- «El Main»    Personaje equilibrado   Ataques sencillos,
                          con mochila grande      alcance medio y buen
                                                  equilibrio

  Ocampo --- «Ping 999»   Movilidad y             Dash y desplazamientos
                          fallos/glitches         rápidos
                          visuales                

  Personaje del usuario   Luchador con presencia  Peleador técnico de
  --- nombre por decidir  y estilo más            corto/medio alcance,
                          serio/intimidante para  con golpes precisos,
                          contrastar con los      una postura reconocible
                          personajes cómicos      y efectos visuales
                                                  propios
  -----------------------------------------------------------------------

### 5.1 Dirección para el sexto personaje

La referencia deseada es la sensación de un luchador protagonista de
juegos de pelea ---por ejemplo, la energía de Kyo Kusanagi--- o una
presencia intimidante de ciencia ficción ---por ejemplo, Darth Vader---.
**Se deben tomar esas referencias como inspiración de tono, silueta o
ritmo, no copiar sus personajes, nombres, vestuario, logos, música,
movimientos distintivos o sprites.**

Propuesta original provisional: un estudiante serio, reservado y
calculador, con paleta oscura, acentos rojos o violetas y una postura
firme. Su identidad podría basarse en precisión y control, para
diferenciarlo de los personajes más cómicos. El tema visual podría
vincularse con el "arquitecto del código", el "núcleo", el "debugger" o
un rival misterioso del equipo. El nombre se decide más adelante.

No diseñar seis kits completos ahora. Primero hacer que dos personajes
compartan el mismo sistema de combate y que el juego permita cambiar sus
estadísticas y recursos sin duplicar código.

## 6. Arte 2D y animación

### 6.1 ¿Conviene usar IA para el pixel art?

Puede utilizarse IA para explorar conceptos, paletas, vestuario y
siluetas, pero **una ilustración estática no es automáticamente una
animación utilizable en Unity**. Para que el luchador se mueva
correctamente se necesita una de estas opciones:

1.  **Sprite sheets ya animadas:** imágenes que contienen varios
    fotogramas ordenados para caminar, saltar, atacar, recibir golpes,
    bloquear y caer.
2.  **Sprites individuales dibujados a mano o con ayuda de IA:** cada
    pose se prepara, limpia y organiza para formar una animación.
3.  **Paquetes 2D con licencia adecuada:** suele ser el camino más
    rápido para un prototipo; se puede utilizar arte provisional y
    sustituirlo después.
4.  **Animación 2D por rig:** separar partes del personaje y animarlas
    con huesos. Puede ahorrar dibujos, pero requiere configuración y no
    siempre luce bien para impactos fuertes de un juego de pelea.

**Recomendación:** para el primer prototipo, utilizar sprites
provisionales de un paquete 2D coherente y con licencia clara. En
paralelo, crear concept art original para definir la apariencia final.
No detener la programación esperando que una IA genere todos los sprites
perfectos.

### 6.2 Animaciones mínimas por luchador

-   Idle (reposo).
-   Walk forward / walk backward (caminar).
-   Jump up / fall / land (saltar, caer y aterrizar), si se separan.
-   Light attack (ataque ligero).
-   Heavy attack (ataque pesado).
-   Block (bloqueo).
-   Hit reaction / hitstun (recibir un golpe).
-   Knockdown / KO (caída y derrota).
-   Dash (si se incluye).
-   Victory (victoria; puede esperar).

Para probar las mecánicas, no es obligatorio tener todas desde el primer
día. Se puede empezar con idle, caminar, salto, un ataque y reacción al
golpe; usar figuras simples o sprites temporales.

### 6.3 Requisitos de calidad para los sprites

-   Mantener una escala y altura consistentes entre luchadores.
-   Tener un punto de pivote coherente, preferiblemente cerca de los
    pies.
-   Asegurar que el origen de los sprites y sus colliders no cambien
    inesperadamente entre fotogramas.
-   Separar la animación visual de la lógica de daño.
-   Usar una dirección base y voltear el sprite horizontalmente cuando
    el luchador mire al otro lado, siempre que el arte lo permita.
-   Revisar que los efectos visuales no oculten las barras de vida ni
    los estados del combate.

## 7. Audio y música

### 7.1 Audio mínimo del prototipo

-   Música de menú.
-   Música de combate.
-   Sonido de golpe ligero.
-   Sonido de golpe pesado.
-   Sonido al recibir daño.
-   Sonido de bloqueo.
-   Sonido de salto o dash, si aplica.
-   Sonido de K.O. o resultado.
-   Sonidos de interfaz para confirmar y cancelar.

Se puede reutilizar música y efectos descargados **solo si la licencia
permite su uso en el proyecto académico y en la entrega que se
realizará**. Registrar la fuente, autor, licencia y atribución
requerida. No asumir que algo disponible en Internet es libre de
derechos.

### 7.2 Organización y mezcla

-   Guardar música en `Assets/_Project/Audio/Music/`.
-   Guardar efectos en `Assets/_Project/Audio/SFX/`.
-   Mantener la música más baja que los golpes y efectos importantes.
-   Crear controles de volumen para música y efectos en Opciones.
-   Evitar sonidos demasiado largos o repetitivos.
-   Usar Audio Mixer de Unity si resulta útil, pero no convertirlo en un
    bloqueo inicial.

## 8. Escenario inicial

Crear solo una arena: **salón de clases universitario**.

Elementos visuales posibles:

-   Fondo con pizarrón o proyector.
-   Ventanas, escritorios y sillas al fondo.
-   Suelo claramente visible donde luchan los personajes.
-   Algunos detalles de programación o vida universitaria.
-   Iluminación y paleta de colores que mantengan a los luchadores
    legibles.

La arena no necesita ser interactiva. El primer prototipo no requiere
objetos destructibles, peligros, varias capas jugables ni transiciones
entre escenarios.

Organizar el fondo en capas si se desea un efecto parallax, pero esto es
opcional. Primero deben funcionar los límites de la arena, las
colisiones y la cámara.

## 9. Estructura recomendada de carpetas

La siguiente estructura es la **canónica desde la Fase 0**. Todo el
contenido propio del juego vive dentro de `Assets/_Project/`; los
paquetes, plantillas y recursos importados se quedan fuera de esa
carpeta. Esto evita mezclar assets del juego con material de terceros y
facilita borrar o actualizar paquetes sin tocar el proyecto.

``` text
FightingRivals/
├── Assets/
│   ├── _Project/
│   │   ├── Art/
│   │   │   ├── Characters/
│   │   │   │   ├── Player/
│   │   │   │   │   ├── Sprites/
│   │   │   │   │   └── Concepts/
│   │   │   │   ├── CPU/
│   │   │   │   │   ├── Sprites/
│   │   │   │   │   └── Concepts/
│   │   │   │   └── Shared/
│   │   │   ├── Environments/
│   │   │   │   └── Classroom/
│   │   │   ├── UI/
│   │   │   │   ├── Icons/
│   │   │   │   ├── Panels/
│   │   │   │   └── Fonts/
│   │   │   ├── VFX/
│   │   │   └── Materials/
│   │   ├── Animations/
│   │   │   ├── Characters/
│   │   │   └── UI/
│   │   ├── Audio/
│   │   │   ├── Music/
│   │   │   ├── SFX/
│   │   │   └── Voice/
│   │   ├── Data/
│   │   │   ├── Characters/
│   │   │   └── Combat/
│   │   ├── Prefabs/
│   │   │   ├── Characters/
│   │   │   ├── Combat/
│   │   │   ├── UI/
│   │   │   ├── Environment/
│   │   │   └── VFX/
│   │   ├── Scenes/
│   │   │   ├── MainMenu.unity
│   │   │   └── FightScene.unity
│   │   ├── Scripts/
│   │   │   ├── Core/
│   │   │   ├── Input/
│   │   │   ├── Characters/
│   │   │   ├── Combat/
│   │   │   ├── AI/
│   │   │   ├── UI/
│   │   │   ├── Audio/
│   │   │   └── Data/
│   │   ├── Settings/
│   │   └── ThirdParty/
│   └── (paquetes, plantillas y recursos importados, fuera de _Project)
├── Docs/
│   ├── PROJECT_MASTER.md
│   ├── ASSET_SOURCES.md
│   └── TEST_PLAN.md
├── Packages/
├── ProjectSettings/
└── README.md
```

`Data/` contendrá los ScriptableObjects (vida, daño, tiempo, personajes)
y `Scripts/Data/` las clases que los definen.

No se deben crear todas las carpetas y clases de golpe si aún no se
necesitan. Es válido comenzar con una estructura pequeña y ampliarla al
añadir sistemas. La Fase 1 creó el esqueleto base de carpetas
(escenas, scripts, editor, prefabs, arte, audio, datos y settings); las
subcarpetas de personajes y combate se añadirán cuando se necesiten.

### 9.1 Escenas

-   `MainMenu.unity`: menú principal, opciones y créditos.
-   `FightScene.unity`: arena, jugador, CPU, HUD y control del
    resultado.

No hace falta crear una escena por personaje ni una escena por round.
Los personajes se instancian como prefabs dentro de `FightScene`.

### 9.2 Prefabs

Los prefabs son objetos configurados que pueden reutilizarse. Propuesta:

-   `PlayerFighter.prefab`: luchador controlado por el usuario.
-   `CPUFighter.prefab`: luchador controlado por la computadora.
-   `Arena_Classroom.prefab`: fondo, suelo y límites.
-   `CombatHUD.prefab`: barras de vida, temporizador y round.
-   `MobileControls.prefab`: controles táctiles.
-   `HitEffect.prefab`: efecto de impacto.
-   `PausePanel.prefab`: panel de pausa.
-   `ResultPanel.prefab`: panel de victoria o derrota.

Player y CPU pueden compartir un prefab base configurable; no duplicar
toda la lógica solo por ser controlados por entradas diferentes.

## 10. GameObjects de la escena de combate

Ejemplo de jerarquía sugerida en `FightScene`:

``` text
FightScene
├── Main Camera
├── EventSystem
├── GameSystems
│   ├── GameManager
│   ├── RoundManager
│   ├── AudioManager
│   └── InputRouter
├── Arena
│   ├── Background
│   ├── Floor
│   ├── LeftBoundary
│   └── RightBoundary
├── Fighters
│   ├── PlayerFighter
│   │   ├── Visual
│   │   ├── GroundCheck
│   │   └── AttackOrigin
│   └── CPUFighter
│       ├── Visual
│       ├── GroundCheck
│       └── AttackOrigin
├── CombatUI
│   ├── PlayerHealthBar
│   ├── CPUHealthBar
│   ├── TimerText
│   ├── RoundText
│   ├── PauseButton
│   └── ResultPanel
└── MobileControls
    ├── DirectionButtons
    ├── JumpButton
    ├── LightAttackButton
    ├── HeavyAttackButton
    ├── BlockButton
    └── DashButton
```

La jerarquía puede cambiar según la implementación. Los nombres son
descriptivos, no requisitos rígidos. Los elementos táctiles se pueden
ocultar en Windows y mostrar en Android.

## 11. Arquitectura de código propuesta

Mantener los scripts pequeños y con una responsabilidad clara. No hace
falta crear una arquitectura empresarial ni usar patrones complejos para
un prototipo.

### 11.1 Sistemas y responsabilidades

  -----------------------------------------------------------------------
  Script o componente propuesto       Responsabilidad
  ----------------------------------- -----------------------------------
  `GameManager`                       Coordinar el estado general: menú,
                                      pelea, pausa y resultado

  `FighterController`                 Movimiento, salto, orientación y
                                      acciones del luchador

  `FighterInput` o                    Convertir teclado/touch en acciones
  `PlayerInputHandler`                del jugador

  `FighterStats`                      Valores configurables de vida,
                                      velocidad y daño

  `FighterStateController`            Controlar estados como idle,
                                      moving, attacking, blocking,
                                      hitstun y KO

  `AttackData`                        Datos de un ataque: daño, alcance,
                                      duración y recuperación

  `AttackHitbox`                      Detectar si un ataque conecta con
                                      el rival

  `Health`                            Vida actual, daño recibido y evento
                                      de derrota

  `CombatResolver`                    Resolver daño, bloqueo y reglas de
                                      impacto

  `CPUController`                     Decidir cuándo acercarse, atacar,
                                      bloquear o retroceder

  `RoundManager`                      Temporizador, fin del round y
                                      resultado

  `CombatHUD`                         Mostrar vida, timer, round y
                                      resultado

  `MenuController`                    Navegación del menú principal

  `OptionsController`                 Volumen y ajustes sencillos

  `AudioManager`                      Reproducir música y efectos

  `MobileControlsUI`                  Enviar acciones desde botones
                                      táctiles

  `CharacterDefinition`               Datos configurables de un
                                      personaje, idealmente con
                                      ScriptableObject cuando haga falta
  -----------------------------------------------------------------------

Estos nombres son una propuesta. La IA que implemente el proyecto debe
inspeccionar primero los scripts existentes y reutilizarlos si ya
cumplen la misma responsabilidad. **No debe crear clases duplicadas por
no haber leído el proyecto.**

### 11.2 Principios de implementación

-   Separar el control del jugador del control de la CPU, pero compartir
    las mecánicas del luchador.
-   Evitar que la UI calcule o aplique daño directamente.
-   El ataque debe tener ventanas claras: inicio, activo y recuperación.
-   Un solo ataque no debe causar daño en cada frame mientras la hitbox
    permanece dentro del rival.
-   El personaje no debe poder moverse, atacar o bloquear en estados
    incompatibles, como KO o hitstun, salvo que una mecánica lo permita
    explícitamente.
-   Usar eventos o llamadas claras para comunicar vida, round terminado
    y resultado.
-   Configurar valores de daño y velocidad en un lugar central.
-   No añadir dependencias o paquetes de terceros sin explicar su
    utilidad.
-   Mantener nombres y código fáciles de entender para una persona que
    está aprendiendo Unity.

## 12. Máquina de estados del luchador

Estados iniciales recomendados:

-   `Idle`: quieto.
-   `Moving`: caminando.
-   `Jumping`: en el aire.
-   `Attacking`: ejecutando un ataque.
-   `Blocking`: bloqueando.
-   `Dashing`: ejecutando dash, si está implementado.
-   `Hitstun`: no puede actuar durante la reacción a un golpe.
-   `Knockdown`: derribado, si se implementa.
-   `KO`: derrotado.

La máquina de estados evita combinaciones imposibles, por ejemplo atacar
y estar derrotado al mismo tiempo. No hace falta implementar todas las
transiciones complejas al inicio. Comenzar con `Idle`, `Moving`,
`Attacking`, `Hitstun` y `KO`; añadir salto y bloqueo de manera
incremental.

## 13. Sistema de combate

### 13.1 Movimiento y colisiones

-   Utilizar componentes de física 2D apropiados, como `Rigidbody2D` y
    `Collider2D`, según el diseño.
-   El jugador y la CPU deben permanecer dentro de la arena.
-   Evitar que los personajes atraviesen el suelo o caigan fuera de la
    pantalla.
-   No permitir que ambos luchadores se queden permanentemente
    superpuestos.
-   La cámara debe mostrar a los dos personajes y la zona útil de
    combate.

### 13.2 Ataques

Cada ataque necesita, como mínimo: - Identificador o nombre. - Daño. -
Alcance y tamaño de la hitbox. - Momento en que la hitbox se activa. -
Duración activa. - Tiempo de recuperación. - Reacción del rival al
impacto. - Efecto visual y sonido, aunque al principio sean
provisionales.

Para el prototipo:

-   Ataque ligero: rápido y de daño bajo.
-   Ataque pesado: más lento y de daño mayor.
-   Bloqueo: anula el daño frontal, sin chip damage.
-   Dash: desplazamiento breve con tiempo de recuperación, si se
    implementa.

No es necesario implementar combos, cancelaciones, agarres, proyectiles
o especiales complejos para validar la primera pelea.

### 13.3 Hitboxes y daño

-   Separar el collider físico del luchador de las hitboxes ofensivas.
-   Activar la hitbox solo durante la ventana activa del ataque.
-   Registrar si ese ataque ya golpeó a ese objetivo para impedir daño
    repetido cada frame.
-   Desactivar la hitbox al terminar el ataque.
-   Aplicar daño mediante un método centralizado, no desde múltiples
    scripts independientes.
-   Mostrar feedback al conectar: reacción de animación, sonido y
    opcionalmente partículas.

### 13.4 Vida y final de round

-   La vida nunca debe bajar por debajo de cero en la UI.
-   Al llegar a cero, bloquear nuevas acciones y declarar KO.
-   Al agotarse el tiempo, comparar vida restante.
-   Detener o congelar el combate durante el resultado.
-   El resultado debe poder reiniciarse sin duplicar jugadores,
    suscripciones o UI.
-   No mantener el temporizador activo detrás de la pantalla de
    resultado.

## 14. IA de la computadora

La CPU inicial no necesita aprendizaje automático ni árboles de
comportamiento complejos. Una máquina de estados sencilla es suficiente.

### Comportamiento mínimo

1.  Detectar la posición del jugador.
2.  Si está demasiado lejos, acercarse.
3.  Si está dentro del alcance, esperar brevemente o atacar.
4.  Elegir entre ataque ligero y pesado con una probabilidad sencilla.
5.  Ocasionalmente bloquear si el jugador ataca, si esta función ya está
    estable.
6.  Retroceder o reposicionarse si queda pegada al jugador o cerca de un
    límite.
7.  No salir de la arena.
8.  Respetar recuperación de ataques, hitstun y KO.
9.  Evitar atacar continuamente sin pausas ni respuesta posible del
    jugador.

### Dificultad inicial

Solo implementar una dificultad normal. Ajustar velocidad de decisión,
pausas entre ataques y probabilidad de bloqueo mediante datos
configurables. Las dificultades Fácil y Difícil pueden esperar.

**Criterio de éxito:** la CPU completa una pelea, se acerca al jugador,
ataca, recibe daño y puede ganar o perder. No debe quedarse inmóvil,
vibrar en el sitio ni intentar atacar en todos los frames.

## 15. UI y flujo de pantallas

### 15.1 Flujo actual

``` text
Main Menu
├── Jugar ───────────────> Fight Scene
├── Opciones ────────────> Options Panel
└── Créditos ────────────> Credits Panel

Fight Scene
├── Pausa ───────────────> Pause Panel
│   ├── Continuar
│   ├── Reintentar
│   └── Menú principal
└── Fin del combate ─────> Result Panel
    ├── Reintentar
    └── Menú principal
```

No existe selección de personaje en este flujo inicial. Cuando el
prototipo sea estable, se puede insertar `CharacterSelect` entre Jugar y
Fight Scene.

### 15.2 Elementos del HUD

-   Barra de vida del jugador.
-   Barra de vida de la CPU.
-   Temporizador.
-   Indicador de round, cuando se implemente mejor de tres.
-   Mensaje breve de resultado.
-   Botón de pausa o acceso equivalente en móvil.

### 15.3 Pantalla de resultado

-   Victoria: mensaje claro y botón **Reintentar** / **Menú principal**.
-   Derrota: mensaje claro y botón **Reintentar** / **Menú principal**.
-   No permitir que el jugador continúe atacando detrás del panel.
-   Evitar que se invoque el resultado dos veces por múltiples eventos
    de daño.

### 15.4 Opciones

Para el prototipo basta con:

-   Volumen de música.
-   Volumen de efectos.
-   Silenciar audio, si resulta sencillo.
-   Guardar los valores de forma local cuando se cambie de escena.

### 15.5 Créditos

Mostrar integrantes del equipo y créditos de assets externos utilizados.
No es necesario incluir animaciones o una escena cinematográfica para
esta versión.

## 16. Entrada multiplataforma: Windows y Android

Recomendación: evaluar el **Unity Input System** y utilizar un único
conjunto de acciones lógicas (mover, saltar, ataque ligero, ataque
pesado, bloquear, dash y pausa). Teclado y controles táctiles deben
activar esas mismas acciones.

-   En Windows, probar primero con teclado.
-   En Android, mostrar el pad virtual y verificar que los botones
    respondan mientras se mantiene otra acción.
-   Evitar que un botón UI se quede "presionado" después de soltarlo o
    salir del área táctil.
-   Probar tamaños de pantalla diferentes.
-   No esperar hasta el final para crear la primera build Android.

Si el tiempo es limitado, priorizar que Windows funcione correctamente,
pero preparar la arquitectura de entrada desde el principio para no
reescribir el combate al añadir controles táctiles.

## 17. Recursos y búsqueda de assets

Crear un inventario antes de importar paquetes. Los recursos externos se
guardarán en las carpetas adecuadas y se documentará su origen en
`Docs/ASSET_SOURCES.md`.

### 17.1 Lista de recursos

  ------------------------------------------------------------------------
  Recurso                           Cantidad inicial Uso
  --------------------- ---------------------------- ---------------------
  Sprite del luchador                  1 provisional Control del jugador
  del jugador                                        

  Sprite del luchador                  1 provisional Oponente
  CPU                                                

  Animaciones básicas      Las mínimas para caminar, Validar combate
                              atacar y recibir golpe 

  Fondo de salón                                   1 Arena

  Suelo y límites                         1 conjunto Colisión y límites

  UI de vida y timer                      1 conjunto HUD

  Botones de UI                           1 conjunto Menú, pausa y
                                                     resultado

  Música                 1 tema temporal de menú y/o Ambiente
                                             combate 

  SFX                     Paquete pequeño de golpes, Feedback
                                        bloqueo y UI 

  Efecto de impacto                    1 provisional Confirmar golpes

  Fuente tipográfica         1 con licencia adecuada UI

  Iconos                                    Según UI Opciones y controles
  ------------------------------------------------------------------------

### 17.2 Ficha de licencia

Para cada recurso externo registrar: - Nombre del recurso. - Autor o
creador. - URL exacta de origen. - Licencia y condiciones. - Si requiere
atribución. - Fecha de descarga. - En qué parte del juego se utiliza. -
Cambios realizados, si los hubo.

No incluir sprites, música, logos, sonidos ni personajes extraídos de
juegos comerciales como Street Fighter, The King of Fighters, Tekken o
Star Wars. Que el proyecto sea académico no significa que se pueda
asumir permiso para redistribuir material comercial. Las referencias
pueden orientar el estilo; los recursos incluidos deben tener licencia
compatible o ser originales.

## 18. Plan de desarrollo por fases

Las fases son hitos funcionales. No son una promesa de duración exacta:
el tiempo dependerá del estado del proyecto, la experiencia con Unity y
los recursos disponibles.

### Fase 0 --- Inspección y preparación (completada)

-   [x] Abrir el proyecto de Unity y comprobar la versión (6000.6.0f1).
-   [x] Revisar escenas, scripts, paquetes y configuración existentes.
-   [x] Confirmar que el proyecto usa URP 2D e Input System 1.20.0.
-   [x] Crear o actualizar este documento en `Docs/PROJECT_MASTER.md`.
-   [x] Registrar la estructura real del proyecto antes de modificarla.
-   [x] Inicializar repositorio git y añadir `.gitignore` de Unity.
-   [x] Definir `Assets/_Project/` como carpeta canónica del juego.
-   [x] Ajustar nombre del producto, identificador Android y orientación
    horizontal.
-   [x] Crear la lista inicial de assets en `Docs/ASSET_SOURCES.md`.

**Salida:** proyecto limpio, entendible y ejecutable. La compilación se
verificó en batch durante la Fase 1.

### Fase 1 --- Menú, escenas y esqueleto navegable (completada)

-   [x] Crear las carpetas de `Assets/_Project/` que usa esta fase.
-   [x] Crear `MainMenu.unity` y `FightScene.unity` y añadirlas a Build
    Settings; retirar `SampleScene` de la lista.
-   [x] Implementar el menú principal con Jugar, Opciones y Créditos.
-   [x] Hacer que Jugar abra `FightScene`.
-   [x] Añadir un `GameManager` mínimo que solo cambie de escena.
-   [x] Incluir un simulador **temporal** de resultado en `FightScene`
    (botones de prueba "Ganaste" y "Perdiste") que será eliminado cuando
    exista combate real.
-   [x] Añadir `ResultPanel` con Reintentar y Menú principal.
-   [x] Permitir regresar al menú.
-   [x] Evitar pantallas vacías o referencias rotas.
-   [x] Validar automáticamente escenas, referencias y botones
    (`PHASE1_VALIDATION_OK`).

**Salida:** menú → pelea vacía → resultado → reintentar o volver al
menú. La comprobación visual en Play mode queda como prueba manual
pendiente.

### Fase 2 --- Arena y movimiento del jugador

-   Crear una arena con suelo y límites.
-   Instanciar el jugador con un sprite provisional.
-   Implementar movimiento horizontal y salto.
-   Ajustar colisiones y orientación hacia la CPU.
-   Verificar que no se caiga ni salga de la arena.

**Salida:** el personaje se controla de forma estable.

### Fase 3 --- Primer ataque y daño

-   Crear un ataque ligero.
-   Configurar la hitbox y su ventana activa.
-   Hacer que el rival reciba daño una sola vez por golpe.
-   Añadir reacción al impacto.
-   Mostrar una barra de vida que disminuya correctamente.

**Salida:** un golpe conecta, causa daño y produce feedback.

### Fase 4 --- Segundo luchador y CPU básica

-   Añadir el rival CPU.
-   Reutilizar los componentes comunes del luchador.
-   Implementar acercamiento, distancia de ataque y pausas.
-   Hacer que el rival también ataque y reciba daño.
-   Evitar que quede atrapado en límites o superpuesto.

**Salida:** el jugador puede pelear contra una CPU funcional.

### Fase 5 --- Reglas y resultado

-   Añadir ataque pesado.
-   Añadir bloqueo.
-   Añadir temporizador.
-   Resolver victoria por KO o tiempo.
-   Mostrar pantalla de victoria o derrota.
-   Añadir reintentar y regresar al menú.
-   Reiniciar el combate sin duplicar objetos.

**Salida:** una pelea completa se puede ganar, perder y reiniciar.

### Fase 6 --- Pausa, audio y pulido mínimo

-   Implementar pausa y reanudación.
-   Añadir música y efectos.
-   Implementar volumen o silencio.
-   Añadir animaciones provisionales para los estados esenciales.
-   Ajustar HUD, textos y feedback de impactos.

**Salida:** prototipo presentable con controles y feedback claros.

### Fase 7 --- Android y pruebas

-   Crear build de desarrollo para Android.
-   Añadir controles táctiles.
-   Probar orientación, proporciones y escalado.
-   Comprobar que no haya botones tapando el HUD.
-   Medir rendimiento en un dispositivo real.
-   Corregir errores bloqueantes.

**Salida:** build Android evaluable o una lista clara de impedimentos
pendientes.

### Fase 8 --- Preparación para ampliación

Solo después de estabilizar el prototipo:

-   Convertir los datos comunes en configuraciones reutilizables.
-   Crear variantes para los seis personajes.
-   Añadir selección de personaje.
-   Añadir especiales y medidor.
-   Implementar mejor de tres rounds.
-   Construir el modo Arcade y las arenas restantes.

**Salida:** base preparada para crecer sin rehacer todo el combate.

## 19. Criterios de aceptación del prototipo

El prototipo inicial se considera funcional cuando:

-   [ ] El juego inicia sin errores bloqueantes.
-   [ ] Jugar lleva a la arena.
-   [ ] El jugador se mueve, salta y no atraviesa el suelo.
-   [ ] La CPU se mueve y ataca.
-   [ ] Los ataques conectan y causan daño correctamente.
-   [ ] Un mismo ataque no quita vida repetidamente en cada frame.
-   [ ] Las barras de vida muestran valores correctos.
-   [ ] El timer llega a cero y resuelve el resultado.
-   [ ] El combate termina al cumplirse una condición de victoria o
    derrota.
-   [ ] Las pantallas de victoria y derrota ofrecen reintentar.
-   [ ] Reintentar reinicia la pelea correctamente.
-   [ ] Regresar al menú funciona.
-   [ ] Pausar no permite que el combate siga avanzando detrás del
    panel.
-   [ ] La música y los efectos pueden escucharse y regularse.
-   [ ] La UI es legible en Windows.
-   [ ] Existe una build Windows probada.
-   [ ] Los controles táctiles funcionan en un dispositivo Android, si
    Android forma parte de la entrega comprometida.
-   [ ] Los assets externos están documentados y sus licencias
    revisadas.

## 20. Riesgos y mitigaciones

  -----------------------------------------------------------------------
  Riesgo                              Mitigación
  ----------------------------------- -----------------------------------
  Querer crear seis personajes        Probar solo dos luchadores
  completos desde el inicio           provisionales y reutilizar lógica

  Arte generado por IA sin            Usar spritesheets existentes con
  animaciones coherentes              licencia o prototipos simples;
                                      generar concept art por separado

  IA de combate demasiado compleja    Comenzar con una máquina de estados
                                      simple y reglas basadas en
                                      distancia

  Ataques que hacen daño cada frame   Registrar objetivos golpeados
                                      durante cada ataque y usar ventanas
                                      de hitbox

  Código duplicado entre jugador y    Compartir el controlador de
  CPU                                 luchador y cambiar únicamente la
                                      fuente de entrada

  Android se deja para el final       Probar una build temprano y
                                      preparar la entrada multiplataforma
                                      desde el principio

  Importar recursos sin revisar       Mantener inventario con autor, URL
  licencias                           y licencia

  La IA de programación cambia        Trabajar por tareas pequeñas,
  demasiados archivos                 revisar el diff y probar tras cada
                                      cambio

  El prototipo se amplía antes de ser Usar los criterios de aceptación
  estable                             como puerta para avanzar de fase
  -----------------------------------------------------------------------

## 21. Instrucciones para cualquier IA que trabaje en este proyecto

Este documento debe servir como contexto inicial para Claude, ChatGPT,
OpenCode u otra herramienta. La IA debe respetar estas reglas:

1.  **Inspeccionar antes de modificar.** Revisar archivos, escenas,
    versión de Unity, paquetes y scripts existentes antes de proponer
    cambios.
2.  **No asumir que las carpetas propuestas ya existen.** Confirmar la
    estructura real del proyecto.
3.  **Trabajar por fases.** No implementar todo el GDD de una sola vez.
4.  **No saltar al Arcade completo.** La prioridad es una pelea 1 contra
    1 contra CPU que pueda ganarse, perderse y reiniciarse.
5.  **No implementar todavía selección de personaje.** El botón Jugar
    entra directamente a `FightScene`.
6.  **No añadir funciones no solicitadas.** Sin online, tienda, RPG,
    inventarios, matchmaking ni arquitectura innecesariamente compleja.
7.  **Explicar cada cambio.** Indicar qué archivos se crearán o
    editarán, para qué sirven y cómo probarlos.
8.  **Respetar la experiencia del desarrollador.** Explicar Unity,
    GameObjects, componentes, prefabs y scripts con claridad; evitar
    grandes bloques de código sin explicar cómo integrarlos.
9.  **Evitar reemplazos masivos.** Antes de sobrescribir un archivo,
    leerlo completo y preservar la funcionalidad existente.
10. **Usar recursos con licencia compatible.** No descargar ni
    incorporar material comercial extraído de juegos.
11. **No afirmar que algo funciona sin verificarlo.** Ejecutar
    comprobaciones disponibles o describir honestamente qué falta probar
    en el editor.
12. **No crear clases duplicadas.** Si existe un sistema equivalente,
    reutilizarlo o explicar por qué debe cambiarse.
13. **Mantener Windows y Android en mente.** No usar controles
    exclusivos de teclado directamente dentro de la lógica del luchador.
14. **Al terminar una fase**, informar: archivos modificados,
    funcionalidad añadida, pasos para probar, errores conocidos y
    siguiente fase recomendada.
15. **Actualizar este documento** solo cuando cambie de forma relevante
    el alcance, la arquitectura o el estado real del proyecto. No
    convertirlo en un registro de cada línea de código.

### Formato recomendado de respuesta de la IA por tarea

-   **Objetivo de la fase**
-   **Qué encontró al inspeccionar el proyecto**
-   **Plan de cambios**
-   **Archivos que creará o modificará**
-   **Implementación**
-   **Cómo probarlo en Unity**
-   **Resultado de las comprobaciones**
-   **Pendientes y siguiente paso**

La IA debe detenerse y pedir aclaración si encuentra decisiones que
cambian el alcance o si no puede identificar con seguridad una escena o
sistema existente. No debe inventar el contenido de archivos que no haya
inspeccionado.

## 22. Primera solicitud recomendada para Claude

Después de abrir la carpeta real del proyecto en el editor, entregar
este documento a Claude y usar una solicitud como esta:

> Lee `Docs/PROJECT_MASTER.md` completo y analiza el proyecto Unity
> abierto. Por ahora no implementes el juego entero ni hagas cambios
> grandes. Primero inspecciona la versión de Unity, las escenas, los
> scripts, los paquetes, el Input System y la configuración actual de
> Windows/Android. Compara el estado real con el documento.
>
> Devuélveme: 1. Qué existe actualmente y qué falta. 2. Qué problemas
> técnicos o decisiones conviene resolver antes de programar. 3. Una
> propuesta de fases pequeñas para alcanzar el prototipo descrito. 4.
> Qué recursos provisionales necesitamos y cuáles podemos posponer. 5.
> La primera tarea concreta que deberíamos ejecutar.
>
> No crees selección de personaje todavía. Al pulsar Jugar se debe
> entrar directamente a una pelea 1 contra 1. No implementes Arcade,
> varios escenarios ni los seis personajes completos todavía. Prioriza
> una sola arena, un luchador del jugador, una CPU y un combate que
> termine correctamente. Antes de editar, explícame los archivos que
> pretendes cambiar y espera mi aprobación.

## 23. Decisiones cerradas y abiertas

### 23.1 Cerradas en la Fase 0

No reabrir estas decisiones sin actualizar este documento y avisar al
equipo:

-   **Motor:** Unity 6000.6.0f1 con URP 2D.
-   **Entrada:** Unity Input System 1.20.0, ya instalado y activo.
-   **Estructura:** todo el contenido propio vive en `Assets/_Project/`.
-   **Resolución y orientación:** 1920×1080 de referencia y orientación
    horizontal bloqueada en Android.
-   **Bloqueo:** botón dedicado; anula el daño frontal sin chip damage.
-   **Formato del prototipo:** un solo round. El mejor de tres queda para
    después de estabilizar el combate.
-   **Identidad técnica:** producto `Fighting Rivals`, paquete Android
    `com.fightingrivals.game`.
-   **Android:** es una meta explícita del prototipo y debe probarse
    durante el desarrollo, no al final. No bloquea avanzar en las fases
    de Windows.
-   **Remapeo de controles:** fuera del alcance del prototipo.

### 23.2 Abiertas

Estas decisiones se pueden cerrar durante las primeras fases; no deben
impedir que se empiece con sprites provisionales:

-   Paquete de sprites temporal.
-   Diseño definitivo del sexto personaje y su nombre.
-   Valores finales de daño, velocidad, recuperación y vida.
-   Si se incluye el dash en el prototipo.
-   Si el arte final será pixel art, ilustración 2D de alta resolución o
    rig 2D, y su escala.
-   Licencias y atribuciones definitivas de música, efectos, fuentes y
    sprites. Se registran en `Docs/ASSET_SOURCES.md`.

Mientras se deciden, usar valores y recursos temporales fácilmente
sustituibles.

## 24. Resumen ejecutivo

La meta inmediata no es terminar el juego completo descrito en el GDD
original. Es demostrar que *Fighting Rivals* puede funcionar como un
juego de pelea 2D: menú → combate contra CPU → victoria/derrota →
reintentar o volver al menú.

El primer hito importante es una pelea que se sienta consistente, donde
el movimiento, los ataques, el daño, la CPU y el resultado funcionen sin
errores bloqueantes. Una vez que esa base sea estable, se amplía el
roster a seis personajes, se añade selección de luchador y después se
desarrolla el Arcade.

**Prioridad absoluta: un prototipo pequeño, jugable, comprobable y fácil
de ampliar.**

## 25. Registro de cambios

### Versión 0.3 --- Fase 1

-   Estructura `Assets/_Project/` creada.
-   `MainMenu.unity` y `FightScene.unity` generadas y añadidas a Build
    Settings; `SampleScene` queda fuera de la build.
-   Menú con Jugar, Opciones y Créditos; paneles de Opciones y Créditos
    dentro de `MainMenu`.
-   Flujo Jugar → `FightScene` → resultado → Reintentar / Menú.
-   Simulador temporal de resultado (Ganaste / Perdiste) y
    `ResultPanel` definitivo para la fase.
-   `GameManager`, `MainMenuController` y `ResultPanelController`
    implementados.
-   Herramientas de editor `Phase1SceneBuilder` y `Phase1Validator`
    añadidas.
-   Validación automática completada con `PHASE1_VALIDATION_OK`.

### Versión 0.2 --- Fase 0

-   Proyecto existente registrado: Unity 6000.6.0f1, URP 2D, Input
    System 1.20.0 activo.
-   Documento movido a `Docs/PROJECT_MASTER.md`.
-   Repositorio git inicializado y `.gitignore` de Unity añadido.
-   Estructura canónica definida en `Assets/_Project/`.
-   Orientación horizontal, resolución de referencia 1920×1080,
    bloqueo con botón, un round y remapeo fuera de alcance.
-   Ajustes de producto: `Fighting Rivals` y
    `com.fightingrivals.game`.
-   Inventario inicial de assets creado en `Docs/ASSET_SOURCES.md`.
-   Fase 1 redefinida como esqueleto navegable con resultado temporal.
