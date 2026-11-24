# Ficha de Personaje: Leo (El Músico)

> Formato unificado v1 · Estado: personaje jugable · **Ficha canónica de Leo**

## 1. Contexto del Proyecto
* **Género:** Lucha 2D (estilo Street Fighter).
* **Estilo Visual:** Pixel Art.
* **Tipo de Movimiento:** Ejes X e Y.

## 2. Perfil del Personaje
* **Nombre:** Leo.
* **Arquetipo:** Shoto / Zoner Híbrido (personaje de entrada / honesto; equilibrado, excelente control de media distancia).
* **Arma principal:** Guitarra acústica.
* **Apariencia Base:** Joven de complexión delgada; chaqueta de mezclilla, pantalón de mezclilla, botas y guitarra colgada a la espalda.
* **Paleta de colores:** [PENDIENTE: paleta final en hexadecimal].

## 3. Estadísticas Base
* **Salud (HP):** 1000 (estándar).
* **Velocidad de movimiento:** Rápida (caminata fluida, dash ágil).
* **Salto:** Altura media, trayectoria rápida.
* **Peso:** Ligero-Medio (cae a velocidad estándar al ser lanzado al aire).

## 4. Especificaciones Técnicas (Físicas y Cajas de Colisión)
* **Resolución de Sprite:** [PENDIENTE].
* **Punto de anclaje (Pivot):** Centro inferior (entre los dos pies).
* **Hurtbox Base:** Centrada en el cuerpo; se reduce al agacharse. Medidas: [PENDIENTE].
* **Hitbox del Arma (Guitarra):** Extendida. La guitarra NO tiene *Hurtbox*: si el rival la golpea durante un ataque, Leo no recibe daño. Medidas: [PENDIENTE].
* **Cambios de caja por estado:** [PENDIENTE: agachado, saltando y deslizamiento].

## 5. Mapeo de Controles
| Acción Lógica | Input | Detalle |
| --- | --- | --- |
| Mover | Izquierda / Derecha | Caminar |
| Saltar | Arriba | — |
| Agacharse | Abajo | Reduce la hurtbox |
| Dash | Doble toque Adelante | Dash rápido |
| Backdash | Doble toque Atrás | Frames de invencibilidad al inicio ([PENDIENTE: frames]) |
| Defensa alta | Mantener dirección opuesta al rival | — |
| Defensa baja | Diagonal Abajo-Atrás | — |
| Ataque Ligero (puño/patada) | Botón de ataque ligero | Corto alcance, poco daño, recuperación rápida |
| Ataque Fuerte (guitarra) | Botón de ataque fuerte | Largo alcance, gran daño; vulnerable si falla |

## 6. Movimientos Básicos y Frame Data
* **Ataque Ligero:** Start-up de 3 a 5 frames. Active/recovery: [PENDIENTE].
* **Ataque Fuerte:** Start-up de 10+ frames; deja a Leo vulnerable si falla. Active/recovery: [PENDIENTE].
* **Backdash:** invencibilidad inicial; [PENDIENTE: frames].
* **Acorde Ascendente:** invencibilidad en los primeros frames; [PENDIENTE: frames].

## 7. Lista de Ataques Especiales
* **Onda Acústica (Proyectil)**
  * **Input:** Abajo, Diagonal Abajo-Adelante, Adelante + Ataque Ligero.
  * **Descripción:** Leo rasguea la guitarra y lanza una nota musical de energía (proyectil de velocidad media) que recorre la pantalla; ideal para control de espacio (zoning).
* **Acorde Ascendente (Anti-aéreo)**
  * **Input:** Adelante, Abajo, Diagonal Abajo-Adelante + Ataque Fuerte.
  * **Descripción:** Salto giratorio con la guitarra apuntando al cielo (estilo *Shoryuken*). Invencibilidad en los primeros frames; castiga saltos del rival.
* **Deslizamiento Rockero (Ataque de Avance)**
  * **Input:** Abajo, Diagonal Abajo-Atrás, Atrás + Patada.
  * **Descripción:** Leo se desliza de rodillas golpeando bajo. Pasa por debajo de proyectiles altos y debe bloquearse con Defensa Baja.

## 8. Movimiento Definitivo (Super / Ultimate)
* **Concierto Final**
  * **Input:** Doble cuarto de luna hacia adelante + Ambos botones de ataque.
  * **Costo:** 100% de la Barra de Energía (Super Meter).
  * **Descripción:** Golpe demoledor de corto alcance con la guitarra; si conecta, cinemática breve con fondo oscurecido y luces de escenario, solo frenético que emite ondas de sonido multi-hit y un último acorde que lanza al rival al otro extremo del escenario.

## 9. Máquina de Estados (State Machine)
* `IDLE`, `WALK_FORWARD`, `WALK_BACKWARD`, `CROUCH`, `JUMP`.
* `GUARD_HIGH`, `GUARD_LOW`.
* `ATTACK_LIGHT`, `ATTACK_HEAVY`, `ATTACK_SPECIAL`, `HIT`, `KO`.
* [PENDIENTE: estados específicos de deslizamiento, super y transiciones].
* **Implementación:** buffer de inputs, colisiones AABB y variables `move_speed`, `jump_force`, `gravity`, `mass` (convención de la plantilla).

## 10. Assets Requeridos
* Sprite sheet base con guitarra a la espalda.
* Animaciones: caminar, saltar, agachar, dash, backdash, defensa alta y baja.
* Puño/patada y golpes de guitarra (ligero y fuerte).
* Onda acústica (proyectil), giro ascendente con guitarra y deslizamiento de rodillas.
* Cinemática del super: fondo oscuro, luces de escenario, solo y ondas de sonido multi-hit.
* [PENDIENTE: resolución, paleta y medidas de hitbox/hurtbox para el export].
