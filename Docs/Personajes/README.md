# Personajes — Reglas comunes (versión prototipo)

> Estado: **propuesta para revisión** · 18/11/2026
> Las fichas originales (la visión final de cada personaje) están en `Originales/`. Las fichas de esta carpeta son la **versión viable** para el prototipo de PC y Android. Lo que se recortó no se pierde: cada ficha lo lista en "Visión final (después del prototipo)".

## 1. Por qué se simplificó
- **Hay que exportar a teléfono:** los inputs de joystick (cuarto de luna, 360°, carga) casi no se pueden hacer en pantalla táctil. Por eso el especial es **un solo botón (E), sin dirección**.
- **El tiempo es poco:** cada mecánica rara (agarres, invencibilidad, cinemáticas) cuesta días de código y de arte. Cada personaje tiene **Ligero + Fuerte + 1 especial + 1 super** (decidido el 23/11/2026), y se reutiliza lo más posible. El segundo especial de cada uno quedó en "Visión final".
- **Un solo sistema para todos:** el código usa un `FighterController` común. Lo que cambia entre personajes son datos (`FighterData` y `AttackData`, ScriptableObjects en `Assets/_Project/Data/`) y un script pequeño de especiales por personaje.

## 2. Controles (iguales para todos)
| Acción lógica | Teclado (P1) | Táctil |
| --- | --- | --- |
| Mover | A / D | Botones ◀ ▶ |
| Saltar | W | Botón ▲ |
| Ataque Ligero (L) | J | Botón L |
| Ataque Fuerte (F) | K | Botón F |
| Especial (E) | L | Botón E |
| Bloquear (B) | Espacio (mantener) | Botón B (mantener) |
| Super (S) | I (solo con barra llena) | Botón S (se ilumina con barra llena) |

- **Especial** = botón E (la dirección no cambia el ataque en el prototipo).
- **Bloqueo:** botón dedicado, **solo frontal**, sin daño por bloqueo (chip). No hay agacharse ni guardia alta/baja en el prototipo.
- Total: 7 botones. Caben en pantalla horizontal (izquierda: ◀ ▶ ▲; derecha: L F E B, y S arriba).

## 3. Estados comunes (máquina de estados)
`Idle`, `Moving`, `Jumping`, `Attacking`, `Blocking`, `Hitstun`, `Blockstun`, `KO`.
Los especiales usan `Attacking` con su propio `AttackData`. Si un personaje necesita un estado extra (por ejemplo el teleport de Elias), su ficha lo indica.

## 4. Valores base (referencia de balance)
Frames a **60 fps**. Distancias en **unidades de Unity** (1 u = 64 px).

| Valor | Base | Notas |
| --- | --- | --- |
| HP | 1000 | Cada ficha ajusta ±100 según su arquetipo |
| Velocidad de caminar | 3.5 u/s | |
| Fuerza de salto | 12 | Gravedad común 3 (gravity scale) |
| Ligero | 5 inicio / 3 activo / 8 recuperación · 50 daño | Rápido, para interrumpir |
| Fuerte | 12 / 4 / 18 · 110 daño | Se puede castigar si falla |
| Hitstun | Ligero 14 f · Fuerte 20 f | |
| Blockstun | Ligero 10 f · Fuerte 14 f | |
| Super | ~300 daño | Cuesta la barra completa. **Todos los supers se pueden bloquear** (sin daño por bloqueo) |
| Barra de super | 100 | +8 al golpear, +5 al recibir golpe, +3 al bloquear |
| Combos | Ligero → Ligero → Fuerte | Única cadena del prototipo |

**Regla de balance:** lo que es rápido o seguro hace poco daño; lo que hace mucho daño es lento y castigable. Todos los números son **iniciales** y se ajustan probando el juego (playtest) desde los ScriptableObjects, sin tocar código.

## 5. Arte (común para todos)
- **Lienzo:** 128×128 px por frame, pivot en los pies (centro inferior), **PPU 64** → el personaje mide ~2 u de alto. Panchito puede usar 160×128 por el arma.
- **Paleta:** máximo 16 colores por personaje, en hexadecimal dentro de su ficha.
- **Animaciones mínimas (las mismas en los 4):**

| Animación | Frames sugeridos |
| --- | --- |
| Idle | 4 (bucle) |
| Caminar | 6 (bucle) |
| Saltar | 3 (subir / punto alto / caer) |
| Ligero | 4 |
| Fuerte | 6 |
| Bloquear | 1–2 |
| Recibir golpe | 2 |
| KO | 4 |
| Especial | 4–6 |
| Super | 6–8 + efecto |
| Victoria | 2–4 (opcional) |

≈ 40–50 frames por personaje. **Orden de producción:** Leo completo primero (sirve de prueba al equipo de código) y después los demás. Mientras no haya sprites, el código usa rectángulos de colores.

## 6. Elenco
| Personaje | Arquetipo | HP | Velocidad | Fortaleza | Debilidad |
| --- | --- | --- | --- | --- | --- |
| [Leo](Ficha_Leo.md) | Equilibrado / zona | 1000 | Media | Proyectil (zona) | Daño medio |
| [Elias](Ficha_Elias.md) | Engañoso (Trickster) | 1000 | Media | Teleport detrás del rival | Golpes de poco alcance |
| [Oscar](Ficha_Oscar.md) | Agresivo (Rushdown) | 950 | Muy alta | Presión y cerrar distancia | Menos vida |
| [Panchito](Ficha_Panchito.md) | Peso pesado | 1100 | Baja | Armadura, daño alto, destruye proyectiles | Lento, blanco grande |
| [Pedro](Ficha_Pedro.md) | Bravucón / carga y castigo | 1000 | Media | Embestida en moto, super de fuego de largo alcance | Embestida lenta y castigable |
| [Ajex](Ficha_Ajex.md) | Defensivo / contraataque | 1000 | Media | Contraataque y super cuerpo a cuerpo | Si adivina mal la contra, lo castigan |
| [El Sinodal](Ficha_Boss.md) (jefe, solo CPU) | Jefe / peso pesado | 1500 | Baja | Mucha vida y daño, 2 especiales | Lento y castigable |

**Si se agrega un personaje nuevo:** usar `Ficha_Plantilla.md` y elegir un arquetipo que no repita los de la tabla.

**Prioridad:** para la entrega basta con **1 personaje jugable contra la CPU (Leo)**. Cada personaje extra es un plus.
