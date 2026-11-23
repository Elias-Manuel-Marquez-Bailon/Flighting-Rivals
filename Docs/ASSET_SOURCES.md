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

## Recursos integrados y generados

La UI usa componentes de `com.unity.ugui`, la fuente integrada
`LegacyRuntime.ttf` y los sprites por defecto de Unity UI (incluidos los
sliders de Opciones). No son recursos externos ni requieren atribución.

`Assets/_Project/Art/Placeholders/Square.png` es un cuadrado blanco de
16×16 generado por `GameSceneBuilder`. Se colorea para los luchadores y el
escenario provisionales; no es un recurso externo y se reemplazará por el
arte definitivo.

## Recursos de plantilla de Unity

Los archivos de `Assets/Welcome/`, `Assets/Settings/` y
`Assets/Scenes/SampleScene.unity` provienen de la plantilla 2D (URP) de
Unity. No forman parte del contenido del juego; `SampleScene` ya está
fuera de Build Settings y el resto se sustituirá durante el desarrollo.
