# Inventario de recursos externos

Este archivo registra todo recurso externo (sprites, animaciones, música,
efectos, fuentes, iconos) que se incorpore al proyecto. Sin una entrada
aquí, un recurso no debe considerarse aprobado.

## Reglas

-   No usar material extraído de juegos comerciales.
-   Verificar que la licencia permita uso académico y distribución en la
    entrega.
-   Registrar la URL exacta y la fecha de descarga.
-   Guardar los recursos del juego dentro de `Assets/_Project/Art/` o
    `Assets/_Project/Audio/`; los paquetes externos, en
    `Assets/_Project/ThirdParty/`.
-   Anotar cualquier modificación hecha al recurso original.

## Recursos registrados

| Recurso | Autor | URL | Licencia | Atribución | Fecha | Uso | Cambios |
| --- | --- | --- | --- | --- | --- | --- | --- |
| _Pendiente_ | | | | | | | |

## Plantilla de ficha

``` text
Nombre:
Autor:
URL de origen:
Licencia:
¿Requiere atribución?:
Fecha de descarga:
Uso en el juego:
Cambios realizados:
```

## Recursos integrados de Unity

La UI de la Fase 1 usa componentes de `com.unity.ugui`, la fuente
integrada `LegacyRuntime.ttf` y los sprites por defecto de Unity UI. No
son recursos externos ni requieren atribución adicional.

## Recursos de plantilla de Unity

Los archivos de `Assets/Welcome/`, `Assets/Settings/` y
`Assets/Scenes/SampleScene.unity` provienen de la plantilla 2D (URP) de
Unity. No forman parte del contenido del juego; `SampleScene` ya está
fuera de Build Settings y el resto se sustituirá durante el desarrollo.

## Sprites de personajes (PixelLab)

``` text
Nombre: Sprites de Leo (12 animaciones + proyectil «Onda Acústica»)
Autor: Generados por el equipo con PixelLab (plan de pago)
URL de origen: https://pixellab.ai
Licencia: Términos de servicio de PixelLab (https://pixellab.ai/termsofservice); uso propio con suscripción activa
¿Requiere atribución?: No
Fecha de descarga: 27/11/2026
Uso en el juego: Assets/_Project/Art/Characters/Leo/
Cambios realizados: Saturación +15 % (PIL); importados como Sprite, filtro Point, sin compresión
```

``` text
Nombre: Sprites de Alex (12 animaciones)
Autor: Generados por el equipo: poses clave con Gemini (convertidas a pixel art con script) y animaciones con PixelLab (plan de pago)
URL de origen: https://pixellab.ai · https://gemini.google.com
Licencia: Términos de servicio de PixelLab y de Google Gemini; uso propio
¿Requiere atribución?: No
Fecha de descarga: 27/11/2026
Uso en el juego: Assets/_Project/Art/Characters/Alex/
Cambios realizados: Selección de frames, efectos no deseados eliminados, «Recibir golpe» armado con pose de Gemini, saturación +15 %; importados como Sprite, filtro Point, sin compresión
```

``` text
Nombre: Sprites de Elias (14 animaciones)
Autor: Generados por el equipo: poses clave con Gemini (convertidas a pixel art con script) y animaciones con PixelLab (plan de pago)
URL de origen: https://pixellab.ai · https://gemini.google.com
Licencia: Términos de servicio de PixelLab y de Google Gemini; uso propio
¿Requiere atribución?: No
Fecha de descarga: 27/11/2026
Uso en el juego: Assets/_Project/Art/Characters/Elias/
Cambios realizados: Selección de frames, efectos no deseados eliminados con script, «Recibir golpe» armado con pose de Gemini, blanco del ojo retocado a mano en «Caminar», saturación +15 %; importados como Sprite, filtro Point, sin compresión
```
