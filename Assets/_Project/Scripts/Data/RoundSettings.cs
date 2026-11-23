using UnityEngine;

namespace FightingRivals.Data
{
    // Reglas del round: tiempo y pausas entre mensajes.
    [CreateAssetMenu(fileName = "RoundSettings", menuName = "Fighting Rivals/Reglas del round")]
    public class RoundSettings : ScriptableObject
    {
        [Tooltip("Duración del round en segundos.")]
        public float roundTime = 60f;

        [Tooltip("Segundos que se muestra '¡PELEA!' antes de poder moverse.")]
        public float introDuration = 1.2f;

        [Tooltip("Segundos entre el K.O. (o fin del tiempo) y la pantalla de resultado.")]
        public float resultDelay = 1.5f;
    }
}
