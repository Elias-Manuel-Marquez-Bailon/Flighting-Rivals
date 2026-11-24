# Ficha: Leo (El Músico)

> Versión prototipo · Estado: borrador para revisión · 23/11/2026 · **Primer personaje a implementar** · Reglas comunes en `README.md`
> Referencia visual: `Referencias/Leo/Leo_Referencia.md` · Original: `Originales/Ficha_Leo.md`

## 1. Perfil
- **Arquetipo:** Equilibrado / zona. Es el personaje de entrada: controla la media distancia con su proyectil y castiga los saltos.
- **Concepto (1 línea):** músico que pelea a puño y patada, y usa la guitarra en los ataques fuertes y los especiales.
- **Apariencia:** cabello negro corto y alborotado, piel morena clara, complexión delgada. Chamarra de mezclilla azul con las mangas un poco remangadas, **playera roja**, jeans azul oscuro, botas cafés. Guitarra acústica de madera clara a la espalda, con correa café cruzada al pecho (solo la saca en el Fuerte, el especial y el super).
- **Paleta (hex, máx. 16):** [PENDIENTE — se saca del sprite final de PixelLab]

## 2. Estadísticas (`FighterData`)
| Campo | Valor |
| --- | --- |
| HP | 1000 |
| Velocidad de caminar (u/s) | 3.5 |
| Fuerza de salto | 12 |
| Gravity scale | 3 |

## 3. Cajas
- **Hurtbox (cuerpo):** 0.8 × 1.8 u.
- **¿El arma tiene hurtbox?:** No. Si el rival golpea la guitarra durante el Fuerte, Leo no recibe daño.

## 4. Ataques (`AttackData`)
| Ataque | Input | Daño | Inicio | Activo | Recup. | Hitbox (ancho×alto, desplazamiento X) | Efecto |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Puñetazo | L | 50 | 5 | 3 | 8 | 0.7×0.4, X+0.6 | Cuerpo a cuerpo. Abre el combo |
| Guitarrazo | F | 110 | 12 | 4 | 18 | 1.3×0.6, X+0.9 | Cuerpo a cuerpo, alcance largo (con la guitarra) |
| **Onda Acústica** | E | 60 | 14 | — | 20 | Proyectil 0.6×0.6, 7 u/s | Proyectil. Solo 1 en pantalla; desaparece al chocar o salir de pantalla |
| **Concierto Final** (Super) | S | 300 | 8 | 4 | 30 | 1.0×1.2, X+0.8 | Cuerpo a cuerpo. Si conecta: pantalla oscura 1 s, 5 golpes de onda (60 c/u), el último lanza lejos al rival. Se puede bloquear |

**Combo:** L → L → F.

**Balance:**
- Es el único con proyectil normal: controla la distancia, pero hace daño medio.
- La onda pega más que el puñetazo (60 vs 50) porque es más lenta (14 f) y solo puede haber una en pantalla.
- Velocidad base (3.5): si fuera rápido y además tuviera proyectil, podría alejarse y disparar sin riesgo.
- La onda no sirve contra la Guardia del Dragón de Ajex (no la activa) y Panchito puede destruirla.

## 5. Estados extra
Ninguno.

## 6. Animaciones (además de las comunes)
- Rasgueo que lanza la onda + sprite de la nota (proyectil, 2 frames en bucle).
- Super: pose de solo de guitarra + efecto de ondas de sonido + luz de escenario (overlay).

## 7. Visión final (después del prototipo)
- **Acorde Ascendente** (recortado del prototipo): anti-aéreo, salto giratorio con la guitarra hacia arriba. 120 daño · 4/8/28 f · hitbox 0.9×1.8, X+0.5 · sin hurtbox en los frames 1–4, muy castigable si falla.
- Deslizamiento Rockero (golpe bajo, pasa debajo de proyectiles) cuando exista agacharse/guardia baja.
- Dash y backdash con invencibilidad.
- Super con cinemática completa.
