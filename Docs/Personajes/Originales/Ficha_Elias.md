# Ficha de Personaje: Elias (El Caballero de Energía)

> Formato unificado v1 · Estado: personaje jugable

## 1. Contexto del Proyecto
* **Género:** Fighting Game 2D.
* **Estilo Visual:** Pixel Art.
* **Tipo de Movimiento:** Ejes X e Y. Movimientos fluidos y saltos amplios impulsados por "energía".

## 2. Perfil del Personaje
* **Nombre:** Elias.
* **Arquetipo:** Espadachín / Híbrido (excelente control de media distancia y presión con arma).
* **Arma principal:** Sable de Energía (espada láser).
* **Apariencia Base:** Sudadera gris con capucha, jeans azules, tenis negros con azul. En postura de combate puede llevar la capucha puesta (aspecto más misterioso e intimidante).
* **Paleta de colores:** [PENDIENTE: paleta final en hexadecimal].

## 3. Estadísticas Base
* **Salud (HP):** 1000 (estándar).
* **Velocidad de movimiento:** Rápida; postura elegante y defensiva.
* **Salto:** Alto y flotante (simula un salto impulsado por telequinesis).
* **Peso:** Medio.

## 4. Especificaciones Técnicas (Físicas y Cajas de Colisión)
* **Resolución de Sprite:** [PENDIENTE].
* **Punto de anclaje (Pivot):** Centro inferior (Y=0, en los pies).
* **Hurtbox Base:** Tamaño estándar centrado en el cuerpo. Medidas: [PENDIENTE].
* **Hitbox del Arma (Sable):** Muy larga. La espada NO tiene *Hurtbox* (hitbox disjunta): golpea desde lejos sin riesgo de que le peguen en el arma. Medidas: [PENDIENTE].
* **Cambios de caja por estado:** [PENDIENTE: agachado, saltando y durante el lanzamiento del sable].

## 5. Mapeo de Controles
| Acción Lógica | Input | Detalle |
| --- | --- | --- |
| Mover | Izquierda / Derecha | Caminar |
| Saltar | Arriba | Salto alto y flotante |
| Agacharse | Abajo | [PENDIENTE: reducción de hurtbox] |
| Dash | Doble toque Adelante | Deslizamiento rápido hacia adelante |
| Backdash | Doble toque Atrás | Voltereta acrobática con invencibilidad ([PENDIENTE: frames]) |
| Defensa alta | [PENDIENTE: dirección opuesta al rival] | — |
| Defensa baja | [PENDIENTE: Diagonal Abajo-Atrás] | — |
| Ataque Ligero | Botón de ataque ligero | Cortes rápidos y controlados |
| Ataque Fuerte | Botón de ataque fuerte | Cortes amplios y pesados con estela de luz |

## 6. Movimientos Básicos y Frame Data
* **Ataque Ligero:** [PENDIENTE: startup / active / recovery].
* **Ataque Fuerte:** [PENDIENTE: startup / active / recovery].
* **Dash:** [PENDIENTE: duración y distancia].
* **Backdash:** invencibilidad al inicio; [PENDIENTE: frames].

## 7. Lista de Ataques Especiales

* **Empuje Telequinético (Proyectil / Defensa)**
  * **Input:** Abajo, Diagonal Abajo-Adelante, Adelante + Ataque Ligero.
  * **Descripción:** Onda de energía invisible (distorsión visual en el aire). Poco daño, empuja violentamente al rival y destruye proyectiles enemigos.
* **Lanzamiento de Sable (Control de Espacio)**
  * **Input:** Abajo, Diagonal Abajo-Atrás, Atrás + Ataque Fuerte.
  * **Descripción:** El sable sale girando como búmeran; golpea varias veces a la ida y a la vuelta. Elias no puede usar ataques de sable hasta que el arma regrese.
* **Corte Ascendente (Anti-aéreo)**
  * **Input:** Adelante, Abajo, Diagonal Abajo-Adelante + Ataque Ligero.
  * **Descripción:** Salto acrobático impulsado por energía con un tajo vertical de abajo hacia arriba; excelente para castigar saltos del rival.

## 8. Movimiento Definitivo (Super / Ultimate)
* **La Furia del Elegido**
  * **Input:** Doble cuarto de luna hacia adelante + Ambos botones de ataque.
  * **Costo:** 100% de la Barra de Energía (Super Meter).
  * **Descripción:** Tirón telequinético inicial; si conecta, cinemática con los ojos brillantes (o tono oscuro en pantalla), ráfaga implacable de cortes desde todos los ángulos y corte cruzado en "X" que hace estallar el suelo a espaldas del rival.

## 9. Máquina de Estados (State Machine)
* `IDLE`, `WALK_FORWARD`, `WALK_BACKWARD`, `CROUCH`, `JUMP`.
* `GUARD_HIGH` [PENDIENTE], `GUARD_LOW` [PENDIENTE].
* `ATTACK_LIGHT`, `ATTACK_HEAVY`, `ATTACK_SPECIAL`, `HIT`, `KO`.
* [PENDIENTE: estados para sable en vuelo/retorno, empuje y super].
* **Implementación:** buffer de inputs, colisiones AABB y variables `move_speed`, `jump_force`, `gravity`, `mass` (convención de la plantilla).

## 10. Assets Requeridos
* Sprite sheet base; variante con capucha puesta y quitada.
* Animaciones: caminar, saltar, agachar, dash y backdash con voltereta.
* Cortes ligero y fuerte (con estela de luz); defensa alta/baja [PENDIENTE].
* Onda de energía invisible (distorsión); sable girando a la ida y a la vuelta; salto ascendente.
* Cinemática del super: ojos brillantes, ráfaga multi-ángulo, corte en "X" y suelo estallado.
* [PENDIENTE: resolución, paleta y medidas de hitbox/hurtbox para el export].
