# Ficha: Alejandro «Ajex» (El Puño Carmesí)

> Versión prototipo · Estado: borrador para revisión · 23/11/2026 · Reglas comunes en `README.md`
> Referencia visual: `Referencias/Ajex/Ajex_Referencia.md`

## 1. Perfil
- **Arquetipo:** Defensivo / contraataque. Espera al rival y lo castiga cuando ataca. Es el arquetipo que le faltaba al elenco.
- **Concepto (1 línea):** artista marcial disciplinado que canaliza fuego rojo en puños y patadas; su fuerza es leer al rival y devolverle el golpe.
- **Apariencia:** cabello castaño oscuro, complexión delgada y atlética; traje de kung fu negro con ribetes rojos y botones chinos, faja roja, pantalón negro holgado, zapatos negros de kung fu. Fuego **rojo/carmesí** en manos y pies al atacar.
- **Paleta (hex, máx. 16):** [PENDIENTE]

## 2. Estadísticas (`FighterData`)
| Campo | Valor |
| --- | --- |
| HP | 1000 |
| Velocidad de caminar (u/s) | 3.5 |
| Fuerza de salto | 12 |
| Gravity scale | 3 |

## 3. Cajas
- **Hurtbox (cuerpo):** 0.8 × 1.8 u.
- **¿El arma tiene hurtbox?:** No tiene arma. El fuego no tiene hurtbox.

## 4. Ataques (`AttackData`)
| Ataque | Input | Daño | Inicio | Activo | Recup. | Hitbox (ancho×alto, desplazamiento X) | Efecto |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Palma de Fuego | L | 50 | 5 | 3 | 8 | 0.7×0.4, X+0.6 | Golpe de palma rápido con chispa roja |
| Patada Carmesí | F | 110 | 12 | 4 | 18 | 1.2×0.7, X+0.8 | Patada alta con estela de fuego; alcance medio-largo |
| **Guardia del Dragón** | E | 0 → 130 | 2 | 14 (ventana) | 22 si falla | Contra: 1.0×0.8, X+0.7 | Se pone en postura. Si lo golpean durante la ventana, **no recibe daño** y responde con una palma de fuego de 130 que derriba. Si nadie lo golpea, queda expuesto 22 f. No funciona contra proyectiles ni contra supers. Enfriamiento 3 s |
| **Dragón Carmesí** (Super) | S | 300 | 12 | 6 | 30 | 1.0×1.0, X+0.8 (avanza 2 u) | Se lanza hacia adelante con los puños en llamas. Si conecta: ráfaga de 5 puñetazos de fuego (50 c/u) y una patada final de 50 que derriba. Si falla, queda expuesto |

**Combo:** L → L → F.

**Balance:**
- Su especial **no ataca por sí solo**: si el rival no lo golpea, Ajex pierde el turno y se le castiga. Es un juego de adivinanzas.
- La contra no sirve contra proyectiles (Leo) ni supers, así que no es invencible.
- Su super es cuerpo a cuerpo (avanza 2 u): no tiene ataques a distancia, así que no se pisa con Leo (zona).

## 5. Estados extra
- `Counter` (ventana activa → éxito: contraataque / fallo: recuperación).

## 6. Animaciones (además de las comunes)
- Palma con anillo de fuego (L) y patada alta con estela (F).
- Postura de guardia del dragón (E) + palma de contraataque.
- Super: embestida con puños en llamas + ráfaga de puñetazos + patada final (efecto de fuego encima del sprite).

## 7. Visión final (después del prototipo)
- Patada voladora ascendente con fuego (anti-aéreo).
- Postura de meditación que recupera un poco de barra de super.
