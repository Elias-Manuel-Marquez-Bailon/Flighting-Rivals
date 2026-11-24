# Ficha: Panchito (Peso Pesado)

> Versión prototipo · Estado: revisada · 24/11/2026 · Reglas comunes en `README.md` · Original: `Originales/Ficha_Panchito.md`
> Referencia visual: `Referencias/Panchito/Panchito_Referencia.md`

## 1. Perfil
- **Arquetipo:** Peso pesado (Juggernaut). Lento, aguanta golpes y cada acierto duele mucho.
- **Concepto (1 línea):** fuerza bruta con una bola de demolición casera; avanza aguantando golpes y aplasta al rival.
- **Apariencia:** grandote, musculoso y panzón; cabello negro corto, barba, lentes; tank top azul claro, shorts cargo verde olivo, botas, muñequeras negras. Silueta ancha y redonda. Carga al hombro una **bola de demolición con cadena** y la azota o la gira.
- **Paleta (hex, máx. 16):** [PENDIENTE — se saca del sprite final de PixelLab]

## 2. Estadísticas (`FighterData`)
| Campo | Valor |
| --- | --- |
| HP | 1100 (el más alto: compensa que es el más lento) |
| Velocidad de caminar (u/s) | 2.4 |
| Fuerza de salto | 10 |
| Gravity scale | 4 (salto corto, cae rápido) |

## 3. Cajas
- **Hurtbox (cuerpo):** 1.2 × 2.0 u (blanco grande).
- **¿El arma tiene hurtbox?:** No. La bola y la cadena no reciben golpes, igual que en los demás personajes.
- **Lienzo:** 160×128 px (por la bola).

## 4. Ataques (`AttackData`)
| Ataque | Input | Daño | Inicio | Activo | Recup. | Hitbox (ancho×alto, desplazamiento X) | Efecto |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Puñetazo | L | 60 | 6 | 3 | 10 | 0.8×0.6, X+0.7 | Empuja al rival 1 u |
| Demoledor | F | 160 | 18 | 5 | 26 | 1.6×0.8, X+1.0 | Alza la bola y la azota. **Armadura** del frame 1 al 18: aguanta 1 golpe (recibe el daño, pero no se interrumpe). Derriba |
| **Tornado** | E | 4 × 35 | 10 | 40 | 24 | 2.2×1.2, centrada | Gira con la bola y **destruye proyectiles**. Castigable al terminar |
| **Demolición** (Super) | S | 320 | 12 | 6 | 34 | 1.2×1.6, X+0.8 | Se agacha, arranca a correr con **armadura de 1 golpe** y, si alcanza al rival, gira la bola: 3 golpes (60 c/u) + azote al piso que sacude la pantalla (140). Bloqueable. No agarra al rival (eso lo distingue de Oscar) |

**Combo:** L → F (no tiene cadena de 3; su daño compensa).

**Balance:**
- Su caminata es la más lenta, no tiene dash y es un blanco grande.
- Su recuperación larga se castiga si falla.
- La armadura solo aguanta 1 golpe (en el Demoledor y en el Super): un combo de L sí lo detiene.
- El Super se puede bloquear o saltar.

## 5. Estados extra
Ninguno: la armadura es un booleano `hasSuperArmor` durante el inicio del Demoledor y la carrera del Super (misma regla de 1 golpe en ambos).

## 6. Animaciones (además de las comunes)
- Idle respirando pesado con la bola al hombro.
- Demoledor: alzar la bola y azotarla.
- Giro con la bola en bucle (Tornado).
- Super: agachado (inicio) → carrera → giro de la bola (3 golpes) → azote al piso.
- Efecto de polvo/impacto al azotar como sprite aparte (reutilizable).

## 7. Visión final (después del prototipo)
- Agarre Quebrantahuesos (inbloqueable, giro 360°) cuando exista un sistema de agarres.
- **Plancha Sísmica** (E + adelante, recortado del prototipo): salto corto y onda de temblor al caer que hay que saltar. 110 daño · 20/6/22 f · onda en el suelo 2.5×0.4, X+1.2. Después, versión como ataque de carga.
