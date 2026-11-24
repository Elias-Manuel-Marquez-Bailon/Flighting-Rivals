# Ficha de Personaje: Panchito (Juggernaut / Peso Pesado)

> Formato unificado v1 · Estado: personaje jugable con datos pendientes

## 1. Contexto del Proyecto
* **Género:** Fighting Game 2D.
* **Estilo Visual:** Pixel Art.
* **Tipo de Movimiento:** Ejes X (caminar) e Y (salto pesado). Sin *Dash* rápido.

## 2. Perfil del Personaje
* **Nombre:** Panchito.
* **Arquetipo:** "Juggernaut" / Peso Pesado.
* **Estilo de Pelea:** Lento, con "Super Armor" (resiste golpes sin detener su animación) y daño masivo; control de espacio y fuerza bruta.
* **Arma principal:** [PENDIENTE: elegir una opción — bola de demolición casera / mochila llena de rocas / cadena pesada inspirada en Chang]. Bloquea los assets.
* **Apariencia Base:** [PENDIENTE: ropa, colores y silueta].
* **Paleta de colores:** [PENDIENTE: paleta final en hexadecimal].

## 3. Estadísticas Base
* **Salud (HP):** 1100 (mayor al estándar del elenco).
* **Velocidad de movimiento:** Baja; caminata reducida un 30% frente a personajes ágiles. Referencia `move_speed`: 2.0.
* **Salto:** Corto y gravedad alta (cae rápido). Referencia `jump_force`: 5.0.
* **Peso:** Pesado; `weight/gravity_scale` alta.

## 4. Especificaciones Técnicas (Físicas y Cajas de Colisión)
* **Resolución de Sprite:** [PENDIENTE: 128x128px o 192x192px, según anchura y longitud del arma]. Bloquea los assets.
* **Punto de anclaje (Pivot):** Centro inferior (en los pies, Y=0).
* **Hurtbox Base:** Rectángulo ancho y alto; blanco fácil. Medidas: [PENDIENTE].
* **Hitbox del Arma:** Extremadamente amplio durante ataques pesados, con recuperación larga como balance. Medidas: [PENDIENTE].
* [PENDIENTE: confirmar si el arma tiene o no *Hurtbox*, como ocurre en Elias, Leo y Oscar].

## 5. Mapeo de Controles
| Acción Lógica | Input | Detalle |
| --- | --- | --- |
| Mover | Izquierda / Derecha | Pasos pesados; sin dash |
| Saltar | Arriba | Salto corto, gravedad alta |
| Agacharse | Abajo | Se sienta o inclina; hurtbox más ancha |
| Dash | No disponible | — |
| Defensa alta | [PENDIENTE: dirección opuesta al rival] | — |
| Defensa baja | [PENDIENTE: Diagonal Abajo-Atrás] | Mencionada en "Plancha Sísmica" ("bloqueando hacia abajo") |
| Ataque Ligero | Botón de ataque ligero | Manotazo/empujón de barriga; empuja al rival |
| Ataque Fuerte | Botón de ataque fuerte | Martillazo al suelo; *Hard Knockdown* |

## 6. Movimientos Básicos y Frame Data
* **Ataque Ligero:** 3 a 5 frames; corto alcance, empuja al rival.
* **Ataque Fuerte:** *Windup* muy largo y recuperación lenta; derriba al rival. [PENDIENTE: frames exactos].
* **Caminata:** velocidad reducida un 30% (`move_speed` 2.0 de referencia).
* **Salto:** `jump_force` 5.0 de referencia; [PENDIENTE: frames].

## 7. Lista de Ataques Especiales
* **Tornado Destructor** (referencia a Chang Koehan)
  * **Input:** Rotación del Joystick (medio círculo hacia adelante) + Puño Fuerte.
  * **Descripción:** Gira sobre su eje sosteniendo el arma pesada; escudo anti-proyectiles y multi-hit a quien se acerque.
* **Plancha Sísmica**
  * **Input:** Abajo, Arriba + Puño (carga de medio segundo hacia abajo).
  * **Descripción:** Salto corto y pesado; al caer crea un temblor. Daña si el rival no bloquea hacia abajo ni salta.
* **Agarre Quebrantahuesos (Command Grab)**
  * **Input:** Rotación completa (360°) + Puño Ligero.
  * **Descripción:** Inbloqueable de muy corto alcance; levanta al oponente y lo azota contra el suelo. Daño altísimo.

## 8. Movimiento Definitivo (Super / Ultimate)
* **[PENDIENTE: falta diseñar el Super/Ultimate de Panchito.]**
  * **Input sugerido para alinear con el elenco:** Doble cuarto de luna hacia adelante + Ambos botones de ataque.
  * **Costo sugerido:** 100% de la Barra de Energía (Super Meter).
  * **Descripción:** [PENDIENTE].

## 9. Máquina de Estados (State Machine)
* `IDLE` (respiración pesada, balanceando el arma), `WALK_FORWARD`, `WALK_BACKWARD`, `CROUCH`, `JUMP`.
* `GUARD_HIGH` [PENDIENTE], `GUARD_LOW` [PENDIENTE].
* `ATTACK_LIGHT`, `ATTACK_HEAVY`, `SPECIAL_SPIN`, `HIT_ARMOR`.
* `HIT` [PENDIENTE], `KO` [PENDIENTE].
* **Implementación:** booleano `has_super_armor` durante `ATTACK_HEAVY` (resta vida pero ignora la transición a `HIT` con ataques débiles); buffer de inputs y colisiones AABB. Ajustar `recovery_frames` alto en ataques fuertes para permitir el castigo.

## 10. Assets Requeridos
* Sprite sheet en lienzo grande; variantes según el arma elegida [PENDIENTE].
* Animaciones: respiración balanceando el arma, caminar pesado, salto corto, agacharse, defensa alta/baja [PENDIENTE].
* Manotazo/empujón, martillazo con derribo, giro con arma en bucle, salto sísmico y onda de temblor, agarre y azote.
* [PENDIENTE: cinemática del super, resolución, paleta y medidas de hitbox/hurtbox para el export].
