# Ficha de Personaje: Elias (El Teletransportador / Estilo Hughie)

> Formato unificado v1 · Estado: variante alternativa (V2) — no reemplaza a `Ficha_Elias.md`

## 1. Contexto del Proyecto
* **Género:** Fighting Game 2D.
* **Estilo Visual:** Pixel Art.
* **Tipo de Movimiento:** Ejes X e Y con gran énfasis en reposicionamiento instantáneo (Teleport).

## 2. Perfil del Personaje
* **Nombre:** Elias.
* **Arquetipo:** Trickster / Hit-and-Run. Depende de confundir al rival atacando desde ángulos inesperados y escapando del peligro.
* **Estilo de Pelea:** Callejero/Torpe con Súper Fuerza. No tiene técnica marcial, tira puñetazos salvajes y empujones.
* **Apariencia Base:** Ropa casual (sudadera gris, jeans, tenis). *Nota de diseño:* Si quieres un guiño cómico a la serie, al hacer ciertos teletransportes podría perder la sudadera y quedar en camiseta, o simplemente usar un destello visual para omitir la regla de la ropa.
* **Paleta de colores:** [PENDIENTE: paleta final en hexadecimal].

## 3. Estadísticas Base
* **Salud (HP):** 1000 (estándar).
* **Velocidad de movimiento:** Media (camina y corre de forma normal, pero su movilidad real viene de sus poderes).
* **Salto:** Altura estándar, pero puede alterarlo en el aire con poderes.
* **Peso:** Medio.

## 4. Especificaciones Técnicas (Físicas y Cajas de Colisión)
* **Resolución de Sprite:** [PENDIENTE].
* **Punto de anclaje (Pivot):** Centro inferior (Y=0, en los pies).
* **Hurtbox Base:** Tamaño estándar centrado en el cuerpo. Medidas: [PENDIENTE].
* **Hurtbox durante teleport:** se desactiva (invencibilidad) mientras Elias está invisible y se reactiva al reaparecer. [PENDIENTE: frames exactos, ventana de invencibilidad y recuperación]. *Riesgo de balance: invencibilidad total en cada teleport es muy fuerte; valorar limitarla al arranque/desplazamiento.*
* **Hitbox (Golpes):** corto alcance, con gran *knockback* (empuje) simulando la súper fuerza. Medidas y frames: [PENDIENTE].
* **Hitbox del Arma:** no aplica (esta variante no usa sable ni arma; no hereda la regla de hitbox disjunta).
* **Cambios de caja por estado:** [PENDIENTE: agachado, salto, dive kick, parry y super].

## 5. Mapeo de Controles
| Acción Lógica | Input | Detalle |
| --- | --- | --- |
| Mover | Izquierda / Derecha | Caminar |
| Saltar | Arriba | Salto estándar; alterable en el aire con poderes |
| Agacharse | Abajo | [PENDIENTE: reducción de hurtbox] |
| Dash | Doble toque Adelante | *Micro-Teleport* corto que atraviesa proyectiles ([PENDIENTE: frames y distancia]) |
| Backdash | Doble toque Atrás | *Micro-Teleport* hacia atrás ([PENDIENTE: invencibilidad y frames]) |
| Defensa alta | Mantener dirección opuesta al rival | — |
| Defensa baja | Diagonal Abajo-Atrás | — |
| Ataque Ligero | Botón de ataque ligero | Puñetazo rápido y sin técnica; inicia combos |
| Ataque Fuerte | Botón de ataque fuerte | Gancho/empujón a dos manos; start-up largo y *Wall Bounce* |
| Especiales | Ver sección 7 | — |

## 6. Movimientos Básicos y Frame Data
* **Ataque Ligero:** [PENDIENTE: startup / active / recovery].
* **Ataque Fuerte:** start-up largo; hace rebotar al rival contra la pared (*Wall Bounce*). [PENDIENTE: startup / active / recovery].
* **Dash (Micro-Teleport adelante):** [PENDIENTE: duración, distancia, frames de invencibilidad y recovery].
* **Backdash (Micro-Teleport atrás):** [PENDIENTE: invencibilidad y frames].
* **Salto / salto alterado:** [PENDIENTE: altura, frames y control aéreo].

## 7. Lista de Ataques Especiales
* **Blink Trasero (Cross-up)**
  * **Input:** Abajo, Diagonal Abajo-Atrás, Atrás + Ataque Ligero.
  * **Descripción:** Elias desaparece en un destello y reaparece instantáneamente *detrás* del oponente. Ideal para romper la guardia de alguien que solo bloquea hacia adelante.
  * [PENDIENTE: alcance, frames de invencibilidad y recuperación]. *Riesgo de balance: evitar que sea un cruce gratis sin riesgo.*
* **Golpe de Caída (Dive Kick / Teleport Aéreo)**
  * **Input:** Abajo, Diagonal Abajo-Adelante, Adelante + Ataque Fuerte. (Se puede hacer en el aire).
  * **Descripción:** Elias se teletransporta en el aire justo por encima del oponente y cae en picada con un puñetazo potenciado por la gravedad. Debe ser bloqueado con Defensa Alta.
  * [PENDIENTE: altura de aparición, ángulo, frames y confirmar si es overhead].
* **Parry de Pánico (Counter defensivo)**
  * **Input:** Abajo, Abajo + Cualquier Botón.
  * **Descripción:** Elias se cubre la cara asustado por medio segundo. Si recibe un golpe en este estado, no toma daño; en su lugar, se teletransporta automáticamente lejos del rival, reseteando la pelea a una posición neutral.
  * [PENDIENTE: ventana activa, coste/cooldown, si cubre proyectiles y agarres, y recuperación si falla]. *Riesgo de balance: reinicio a neutral gratis ante cualquier golpe es muy seguro; valorar coste de meter o límite de usos.*

## 8. Movimiento Definitivo (Super / Ultimate)
* **Sobredosis de "Temp V"**
  * **Input:** Doble cuarto de luna hacia adelante + Ambos botones de ataque.
  * **Costo:** 100% de la Barra de Energía (Super Meter).
  * **Descripción:** Elias se inyecta/activa su poder al máximo. Se abalanza hacia adelante. Si conecta, el rival queda aturdido mientras Elias se teletransporta a su alrededor a súper velocidad (estilo Dragon Ball), conectando 10 golpes desde todos los ángulos (arriba, abajo, izquierda, derecha) antes de aparecer arriba para un remate que estrella al rival contra el suelo.

## 9. Máquina de Estados (State Machine)
* `IDLE`, `WALK_FORWARD`, `WALK_BACKWARD`, `CROUCH`, `JUMP`.
* `GUARD_HIGH`, `GUARD_LOW`.
* `ATTACK_LIGHT`, `ATTACK_HEAVY`, `ATTACK_SPECIAL`, `HIT`, `KO`.
* [PENDIENTE: `TELEPORT_STARTUP`, `TELEPORT_INVISIBLE`, `TELEPORT_RECOVERY`, `DIVE_KICK`, `PARRY`, `PARRY_SUCCESS`, `SUPER` y sus transiciones].
* **Implementación:** buffer de inputs, colisiones AABB y variables `move_speed`, `jump_force`, `gravity`, `mass`. El hurtbox debe desactivarse/reactivarse según el estado de teletransporte.

## 10. Assets Requeridos
* Sprite sheet base de Elias (sudadera gris, jeans, tenis).
* Animaciones: caminar, saltar, agacharse, dash/backdash en micro-teleport, defensa alta y baja, `HIT` y `KO`.
* Normales: puñetazo ligero y gancho/empujón fuerte con *wall bounce*.
* Especiales: desaparición/reaparición del Blink Trasero; aparición aérea y caída del Golpe de Caída; pose del Parry de Pánico y destello de huida.
* Efecto de destello/estela para teletransportes (y variante cómica sin sudadera, si se aprueba).
* Cinemática del super: aturdimiento, teletransportes multi-ángulo, 10 golpes y remate contra el suelo.
* [PENDIENTE: resolución, paleta y medidas de hitbox/hurtbox para el export].
