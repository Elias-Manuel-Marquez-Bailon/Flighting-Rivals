# Ficha: Elias (El Teletransportador)

> Versión prototipo · Estado: borrador para revisión · 23/11/2026 · Reglas comunes en `README.md`
> Referencia visual: `Referencias/Elias/Elias_Referencia.md` · Original: `Originales/Ficha_Elias_V2.md` (la V1 queda solo como archivo)

## 1. Perfil
- **Arquetipo:** Engañoso (Trickster) / pega y escapa. Confunde con el teleport y se escapa del peligro.
- **Concepto (1 línea):** pelea callejera y torpe, pero con súper fuerza: golpes cortos que empujan mucho.
- **Apariencia:** cabello castaño alborotado, sudadera gris con capucha y cierre, playera oscura, jeans azules, tenis negros con suela blanca. Destellos eléctricos azul claro al atacar y teletransportarse.
- **Paleta (hex, máx. 16):** [PENDIENTE — se saca del sprite final de PixelLab]

## 2. Estadísticas (`FighterData`)
| Campo | Valor |
| --- | --- |
| HP | 1000 |
| Velocidad de caminar (u/s) | 3.5 |
| Fuerza de salto | 12 |
| Gravity scale | 3 |

## 3. Cajas
- **Hurtbox (cuerpo):** 0.8 × 1.8 u. Se **desactiva** solo mientras está invisible en el Blink.
- **¿El arma tiene hurtbox?:** No tiene arma.

## 4. Ataques (`AttackData`)
| Ataque | Input | Daño | Inicio | Activo | Recup. | Hitbox (ancho×alto, desplazamiento X) | Efecto |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Puñetazo salvaje | L | 50 | 5 | 3 | 9 | 0.6×0.4, X+0.55 | Cuerpo a cuerpo, alcance corto |
| Empujón a dos manos | F | 120 | 14 | 4 | 18 | 0.8×0.7, X+0.7 | Cuerpo a cuerpo. Empuja al rival 2.5 u (súper fuerza) |
| **Blink Trasero** | E | 0 | 4 | — | 14 | — | Desaparece en un destello (4 f), queda invisible 8 f (sin hurtbox) y reaparece **detrás** del rival; luego 14 f de recuperación. Enfriamiento 3 s |
| **Sobredosis de Temp V** (Super) | S | 300 | 8 | 4 | 30 | 0.9×1.2, X+0.7 | Cuerpo a cuerpo. Golpe inicial; si conecta, se teletransporta **alrededor del rival** y le pega desde varios lados (izquierda, derecha, arriba): 10 golpes (25 c/u) con destellos y un remate de 50 |

**Combo:** L → L → F.

**Balance:**
- El Blink no ataca. Elias reaparece detrás, pero tiene que golpear después, y el rival puede darse vuelta y bloquear. Eso evita un cruce gratis sin riesgo.
- El bloqueo es solo frontal, así que reaparecer detrás sí crea una oportunidad.
- El enfriamiento evita que use el teleport sin parar.
- El empujón pega 120 (más que el Fuerte base) porque Elias tiene poco alcance.
- Su super se diferencia del de Ajex: no es una embestida en línea recta, sino teleports alrededor del rival.

## 5. Estados extra
- `Teleporting` (Blink: desaparece → invisible → reaparece).

## 6. Animaciones (además de las comunes)
- Desaparecer y reaparecer (destello, 3 + 3 frames). Se reutilizan en Blink y Super.
- Super: golpe inicial + destellos de teleport alrededor del rival (efecto reutilizado) + remate.

## 7. Visión final (después del prototipo)
- **Parry de Pánico** (recortado del prototipo): pose de cubrirse asustado; si lo golpean en la ventana (3 inicio / 10 ventana / 24 recup. si falla) no recibe daño y se teletransporta 4 u lejos. Enfriamiento 5 s, no funciona contra el super. Necesita el estado `Parry`.
- Golpe de Caída (teleport aéreo y caída en picada, overhead).
- Dash y backdash como micro-teleport que atraviesa proyectiles.
- Ataque fuerte con rebote en la pared (wall bounce).
- Guiño cómico: perder la sudadera al teletransportarse.
- Super con cinemática completa estilo Dragon Ball.
