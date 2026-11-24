# Ficha: «El Sinodal» (Jefe final)

> Versión prototipo · Estado: borrador para revisión · 23/11/2026 · Reglas comunes en `README.md`
> Referencia visual: `Referencias/Boss/Boss_Referencia.md` · Ficha anterior (antes de la imagen): `Docs/Ideas_Sprites_y_Jefe.md` §4
> Basado en un compañero de clase: **ya dio su permiso** para usar su cara (23/11/2026).

## 1. Perfil
- **Arquetipo:** Jefe / peso pesado con proyectil. **Solo lo controla la CPU** en el último combate.
- **Concepto (1 línea):** el sinodal que decide quién se titula; pelea con un martillo hecho de un **gabinete de computadora** que suelta descargas y lanza hojas de examen.
- **Apariencia:** cabello negro corto, robusto; camisa y pantalón negros, zapatos negros, **dos bandas cruzadas en el pecho hechas de hojas de examen**. Arma: martillo gigante con cabeza de **gabinete de PC/servidor** (luces LED rojas y verdes) y mango metálico. Descargas eléctricas **verdes y rojas**.
- **Paleta (hex, máx. 16):** [PENDIENTE]

## 2. Estadísticas (`FighterData`)
| Campo | Valor |
| --- | --- |
| HP | 1500 |
| Velocidad de caminar (u/s) | 2.8 |
| Fuerza de salto | 11 |
| Gravity scale | 3 |

## 3. Cajas
- **Hurtbox (cuerpo):** 1.0 × 2.0 u.
- **¿El arma tiene hurtbox?:** No.
- **Lienzo:** 160×128 px (por el martillo).

## 4. Ataques (`AttackData`)
> Excepción de jefe (decidido el 23/11/2026): conserva **2 especiales**.

| Ataque | Input | Daño | Inicio | Activo | Recup. | Hitbox (ancho×alto, desplazamiento X) | Efecto |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Codazo de Autoridad | L | 60 | 6 | 3 | 10 | 0.7×0.5, X+0.6 | Rápido para interrumpir |
| Gabinetazo | F | 140 | 16 | 4 | 20 | 1.2×0.8, X+0.9 | Barrido horizontal con el martillo y chispas. Super armor frames 1–12 (aguanta 1 golpe) |
| **Lluvia de Exámenes** | E | 40 c/u | 12 | — | 16 | 0.4×0.4 proyectil | Lanza 3 hojas en abanico. Máx. 1 ráfaga en pantalla |
| **Sello de Reprobado** | Especial 2 (solo CPU) | 120 | 18 | — | 24 | Proyectil a ras de suelo 1.2×0.6, 6 u/s | Azota el martillo contra el piso y sale una onda eléctrica que **avanza por el suelo**. Hay que saltarla o bloquearla. Castigable. Máx. 1 onda en pantalla |
| **Examen Extraordinario** (Super) | S | 350 | 10 | 6 | 35 | Pantalla completa | Se carga de electricidad roja y verde, pantalla oscura 1 s (aviso) y lluvia de hojas; si conecta, martillazo final de 100 con "REPROBADO". **Se puede bloquear** (sin daño por bloqueo) |

**Combo:** L → L → F.

**Balance:**
- Más vida y daño que nadie, pero lento y castigable. HP 1500 es la excepción de jefe a la regla de ±100 (se puede subir después del playtest).
- El super se puede bloquear: la CPU lo usa en cuanto llena la barra, así que debe ser evitable para que sea justo.
- La super armor del Gabinetazo usa el mismo código que la armadura de Panchito.
- La CPU usa el super en cuanto llena la barra.
- **Fase 2 (opcional):** bajo 40 % de vida, la electricidad se vuelve roja y camina un 15 % más rápido (estado `Enraged`). La pose de rabia de la imagen sirve para el cambio de fase.

## 5. Estados extra
- `Enraged` (opcional, fase 2).

## 6. Animaciones (además de las comunes)
- Idle con el martillo al hombro.
- Barrido (F), azote al piso + onda eléctrica (E + adelante), lanzar hojas (E) + **sprite de la hoja** aparte (2 frames en bucle).
- Pose de rabia con electricidad (super / cambio de fase).
- Victoria señalando al rival (riéndose).

## 7. Visión final (después del prototipo)
- Intro "Su calificación es…" antes de la pelea.
- Diálogo con cada personaje antes de la pelea.
- Tercera fase en una sala de titulación.
- Desbloquearlo como jugable (con menos vida y daño) como premio al ganarle.
