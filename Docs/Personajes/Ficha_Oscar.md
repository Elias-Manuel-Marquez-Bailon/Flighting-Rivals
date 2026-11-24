# Ficha: Oscar (El Huésped Oscuro)

> Versión prototipo · Estado: borrador para revisión · 24/11/2026 · Reglas comunes en `README.md`
> Referencia visual: `Referencias/Oscar/Oscar_Referencia.md` · Original: `Originales/Ficha_Oscar.md`

## 1. Perfil
- **Arquetipo:** Agresivo (Rushdown). Se acerca rápido y no deja respirar al rival.
- **Concepto (1 línea):** sus brazos se transforman en materia oscura (garras, látigo, tentáculo).
- **Apariencia:** cabello negro corto y rizado, piel morena, complexión atlética y delgada. Camisa verde de botones manga corta, jeans azul claro, tenis claros, **pulsera naranja** en la muñeca derecha. Biomasa **negra con brillos morados**, viscosa, con gotas que salpican: aparece **solo al atacar y bloquear**; en idle, caminar y saltar tiene brazos normales.
- **Paleta (hex, máx. 16):** [PENDIENTE — se saca del sprite final de PixelLab]

## 2. Estadísticas (`FighterData`)
| Campo | Valor |
| --- | --- |
| HP | **950** (compensa su velocidad) |
| Velocidad de caminar (u/s) | 4.5 |
| Fuerza de salto | 13 |
| Gravity scale | 3.5 (arco cerrado, cae rápido) |

## 3. Cajas
- **Hurtbox (cuerpo):** 0.8 × 1.8 u.
- **¿El arma tiene hurtbox?:** No. Las extremidades de biomasa pegan lejos sin agrandar su hurtbox.

## 4. Ataques (`AttackData`)
| Ataque | Input | Daño | Inicio | Activo | Recup. | Hitbox (ancho×alto, desplazamiento X) | Efecto |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Zarpazo | L | 40 | 4 | 3 | 7 | 0.7×0.5, X+0.6 | Cuerpo a cuerpo (garra). El más rápido del elenco |
| Látigo de biomasa | F | 100 | 12 | 4 | 18 | 1.4×0.5, X+1.0 | Cuerpo a cuerpo, alcance largo. Empuja 1.5 u aunque lo bloqueen |
| **Tentáculo Atractor** | E | 20 | 14 | 4 | 26 | 3.0×0.4, X+1.8 | Cuerpo a cuerpo de largo alcance (un solo tentáculo recto). Si conecta, jala al rival junto a Oscar (queda a distancia para L). **Si lo bloquean, no jala** y Oscar queda expuesto. Muy castigable si falla |
| **Frenesí Simbiótico** (Super) | S | 300 | 8 | 4 | 30 | 1.0×1.4, X+0.8 | Cuerpo a cuerpo. Si conecta: pantalla oscura, los tentáculos **atrapan al rival, lo levantan y lo azotan contra el piso** 3 veces (70 c/u) y un azotón final de 90. Se puede bloquear |

**Combos:** L → L → F, y Tentáculo → L → L → F.

**Balance:**
- Es el más rápido y pega primero, pero tiene menos vida (950) y daño un poco menor.
- Su especial es muy castigable si falla o si lo bloquean (no jala).
- El tentáculo no es un "agarre": es un golpe que jala. Así no hace falta un sistema de agarres.
- Su super se diferencia de los de Ajex y Elias: no es ráfaga de golpes, sino atrapar y azotar (se programa como golpe + animación, sin sistema de agarres).

## 5. Estados extra
Ninguno (el jalón y el azotón se aplican como efecto del golpe).

## 6. Animaciones (además de las comunes)
- Brazo transformado en garra (L), látigo (F) y tentáculo extendido (E).
- Super: forma monstruosa + tentáculos que levantan y azotan al rival + azotón final.
- Salpicadura negra como efecto aparte (golpes y super).

## 7. Visión final (después del prototipo)
- **Zarpazo Depredador** (recortado del prototipo): salto rasante con zarpazo en "X", avanza 3 u en el aire. 90 daño · 10/6/20 f · hitbox 0.9×0.8, X+0.6 · castigable si lo bloquean.
- Púas Subterráneas (estalagmita a distancia, anti-zoning).
- Dash de sombra que atraviesa proyectiles.
- Tentáculo como agarre de comando real cuando exista un sistema de agarres.
