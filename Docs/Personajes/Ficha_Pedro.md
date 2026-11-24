# Ficha: Pedro (El Motociclista)

> Versión prototipo · Estado: revisada · 24/11/2026 · Reglas comunes en `README.md`
> Referencia visual: `Referencias/Pedro/Pedro_Referencia.md`

## 1. Perfil
- **Arquetipo:** Bravucón / carga y castigo. Pega duro de cerca con la botella y cruza la pantalla de golpe con su moto.
- **Concepto (1 línea):** el motociclista rudo de la uni; pelea a botellazos, escupe fuego con un encendedor y, cuando se enoja, se sube a su moto y atropella al rival.
- **Apariencia:** robusto, barba y cabello negro corto, brazos tatuados; chaleco de mezclilla sin mangas con parches, playera negra, jeans oscuros, cadena de cartera, botas cafés, muñequeras. Lleva en la mano izquierda una botella de cerveza **«Chela Brava»** (marca inventada, sin logos reales) y un encendedor.
- **Paleta (hex, máx. 16):** [PENDIENTE]

## 2. Estadísticas (`FighterData`)
| Campo | Valor |
| --- | --- |
| HP | 1000 |
| Velocidad de caminar (u/s) | 3.5 (normal; la moto es un acelerón ocasional) |
| Fuerza de salto | 11 |
| Gravity scale | 3 |

## 3. Cajas
- **Hurtbox (cuerpo):** 0.9 × 1.8 u.
- **¿El arma tiene hurtbox?:** No (ni la botella, ni el fuego, ni la moto). Durante la Embestida, Pedro conserva su hurtbox normal: se le puede golpear de frente si el rival lo ve venir. **Atraviesa al rival** (no choca con su cuerpo mientras va en la moto); por eso, si lo bloquean, termina detrás y de espaldas.

## 4. Ataques (`AttackData`)
| Ataque | Input | Daño | Inicio | Activo | Recup. | Hitbox (ancho×alto, desplazamiento X) | Efecto |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Puñetazo | L | 55 | 6 | 3 | 9 | 0.7×0.4, X+0.6 | Abre el combo |
| Botellazo | F | 120 | 14 | 4 | 20 | 1.0×0.6, X+0.8 | Golpe amplio con la botella. Castigable si falla |
| **Embestida en Moto** | E | 100 | 16 | 20 | 26 | 1.2×1.2, centrada en la moto | Aparece la moto, Pedro se sube y avanza 5 u en línea recta. Si conecta: derriba. Si lo bloquean o falla: queda de espaldas al rival y es castigable. Enfriamiento 4 s |
| **Aliento de Dragón** (Super) | S | 300 | 10 | 6 | 32 | 3.0×0.8, X+1.8 (llamarada) | Da un trago, acerca el encendedor y escupe una llamarada larga. La llamarada no hace daño por sí sola: si conecta, atrapa al rival → pantalla oscura 1 s, 4 golpes de fuego (45 c/u) y remata pasando encima en la moto (120). Total 300 |

**Combo:** L → L → F.

**Balance:**
- La Embestida cruza mucho terreno, pero es lenta de inicio (16 f) y se ve venir: el rival puede bloquearla o saltarla.
- Si la bloquean, Pedro termina del otro lado y de espaldas, y el rival tiene tiempo de castigarlo.
- Solo 1 moto a la vez y con enfriamiento, para que no la use sin parar.
- Es diferente a los demás: Leo controla desde lejos con proyectil, Oscar jala al rival y Pedro **se lanza** él mismo.

## 5. Estados extra
Ninguno. La Embestida es un `Attacking` que mueve al personaje con velocidad fija mientras dura la ventana activa.

## 6. Animaciones (además de las comunes)
- Botellazo (F).
- Invocar la moto, subirse, avanzar (bucle de 2–3 frames) y bajarse.
- **Moto como sprite aparte** (objeto de PixelLab, vista lateral, 2 frames de ruedas girando + humo del escape).
- Super: trago + encendedor + llamarada (efecto de fuego aparte, reutilizable) + pasada en moto.
- Burla y victoria brindando con la Chela Brava.

## 7. Visión final (después del prototipo)
- Aliento de fuego como especial propio (proyectil corto) si se decide darle 2 especiales.
- Arrancón: cargar la moto manteniendo E para avanzar más lejos.
- Lanzar la botella como proyectil.
- Super con cinemática completa (encender la moto, acelerar, salto con rampa).
