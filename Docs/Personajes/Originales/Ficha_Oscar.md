# Ficha de Personaje: Oscar (El Huésped Oscuro)

> Formato unificado v1 · Estado: personaje jugable

## 1. Contexto del Proyecto
* **Género:** Fighting Game 2D.
* **Estilo Visual:** Pixel Art.
* **Tipo de Movimiento:** Ejes X e Y con gran movilidad, saltos rápidos y *Dashes* agresivos.

## 2. Perfil del Personaje
* **Nombre:** Oscar.
* **Arquetipo:** Rushdown / Mix-up (asediar al oponente, acortar distancias y abrumar con ataques multi-golpe).
* **Arma principal:** Su propio cuerpo alterado por "materia oscura" o "biomasa" (referencia visual tipo simbionte).
* **Apariencia Base:** Ropa casual (camisa verde manga corta, jeans, tenis). Durante los ataques, sus extremidades se envuelven o transforman en materia negra viscosa con garras o tentáculos.
* **Paleta de colores:** [PENDIENTE: paleta final en hexadecimal].

## 3. Estadísticas Base
* **Salud (HP):** 1000 (estándar).
* **Velocidad de movimiento:** Muy alta.
* **Salto:** Rápido y de arco cerrado (difícil de reaccionar).
* **Peso:** Medio.

## 4. Especificaciones Técnicas (Físicas y Cajas de Colisión)
* **Resolución de Sprite:** [PENDIENTE].
* **Punto de anclaje (Pivot):** Centro inferior (Y=0, en los pies).
* **Hurtbox Base:** Tamaño estándar centrado en el cuerpo humano de Oscar. Medidas: [PENDIENTE].
* **Hitbox del Arma (Biomasa/Tentáculos):** Enorme al extender la materia oscura, pero NO expande su *Hurtbox*: da prioridad en los choques de ataques. Medidas: [PENDIENTE].
* **Cambios de caja por estado:** [PENDIENTE: dash fantasma, transformaciones de extremidades y super].

## 5. Mapeo de Controles
| Acción Lógica | Input | Detalle |
| --- | --- | --- |
| Mover | Izquierda / Derecha | Caminar |
| Saltar | Arriba | Arco cerrado |
| Agacharse | Abajo | [PENDIENTE: reducción de hurtbox] |
| Dash | Doble toque Adelante | Deslizamiento de cuerpo parcialmente líquido/sombra; cruza proyectiles en los primeros frames ([PENDIENTE: frames]) |
| Backdash | Doble toque Atrás | [PENDIENTE: propiedades e invencibilidad] |
| Defensa alta | [PENDIENTE: dirección opuesta al rival] | — |
| Defensa baja | [PENDIENTE: Diagonal Abajo-Atrás] | — |
| Ataque Ligero | Botón de ataque ligero | Zarpazos con garras negras; start-up de 3 a 4 frames |
| Ataque Fuerte | Botón de ataque fuerte | Látigo/mazo de biomasa; empuja al rival si es bloqueado |

## 6. Movimientos Básicos y Frame Data
* **Ataque Ligero:** Start-up de 3 a 4 frames. Active/recovery: [PENDIENTE].
* **Ataque Fuerte:** [PENDIENTE: startup / active / recovery]; empuja al rival al ser bloqueado.
* **Dash de cruce de proyectiles:** [PENDIENTE: frames de cruce].
* **Backdash:** [PENDIENTE: frames e invencibilidad].

## 7. Lista de Ataques Especiales
* **Zarpazo Depredador (Ataque de Avance)**
  * **Input:** Abajo, Diagonal Abajo-Adelante, Adelante + Ataque Ligero.
  * **Descripción:** Salto rasante hacia adelante cubierto de materia oscura con un zarpazo en "X"; excelente para cerrar distancias por sorpresa.
* **Tentáculo Atractor (Command Grab a distancia)**
  * **Input:** Abajo, Diagonal Abajo-Atrás, Atrás + Ataque Fuerte.
  * **Descripción:** Lanza un zarcillo negro; si golpea, no hace daño inmediato, enreda al oponente y lo atrae bruscamente hacia Oscar, dejándolo vulnerable a un combo.
  * [PENDIENTE: se denomina *command grab* pero usa un input de cuarto de círculo hacia atrás, no un 360; confirmar terminología o input].
* **Púas Subterráneas (Anti-Zoning)**
  * **Input:** Abajo, Abajo + Cualquier botón de ataque.
  * **Descripción:** Oscar entierra las manos y brota una estalagmita de materia oscura bajo el oponente. El botón determina la distancia (Ligero = cerca, Fuerte = lejos).

## 8. Movimiento Definitivo (Super / Ultimate)
* **Frenesí Simbiótico**
  * **Input:** Doble cuarto de luna hacia adelante + Ambos botones de ataque.
  * **Costo:** 100% de la Barra de Energía (Super Meter).
  * **Descripción:** Oscar ruge y la materia oscura cubre todo su cuerpo (forma monstruosa tipo Venom). Si conecta el abalanzamiento, la pantalla se oscurece y lanza una ráfaga brutal de latigazos y mordiscos multi-ángulo, terminando con un aplastamiento gigante que rompe el suelo del escenario.

## 9. Máquina de Estados (State Machine)
* `IDLE`, `WALK_FORWARD`, `WALK_BACKWARD`, `CROUCH`, `JUMP`.
* `GUARD_HIGH` [PENDIENTE], `GUARD_LOW` [PENDIENTE].
* `ATTACK_LIGHT`, `ATTACK_HEAVY`, `ATTACK_SPECIAL`, `HIT`, `KO`.
* [PENDIENTE: estados de dash que cruza proyectiles, transformación parcial de extremidades y super].
* **Implementación:** buffer de inputs, colisiones AABB y variables `move_speed`, `jump_force`, `gravity`, `mass` (convención de la plantilla).

## 10. Assets Requeridos
* Sprite sheet base con ropa casual.
* Variantes de extremidades transformadas: garras, látigo/mazo y tentáculo.
* Animaciones: caminar, saltar, agachar, dash con efecto fantasma/sombra, backdash.
* Zarpazo en "X", zarcillo atractor, estalagmita de biomasa.
* Forma monstruosa completa y pantalla oscurecida para la cinemática del super.
* [PENDIENTE: resolución, paleta y medidas de hitbox/hurtbox para el export].
