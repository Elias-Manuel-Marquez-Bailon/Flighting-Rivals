using UnityEngine;

namespace FightingRivals.Data
{
    // Ajustes de la inteligencia de la CPU (dificultad normal).
    [CreateAssetMenu(fileName = "CPUSettings", menuName = "Fighting Rivals/Ajustes de CPU")]
    public class CPUSettings : ScriptableObject
    {
        [Header("Distancias")]
        [Tooltip("Distancia a la que la CPU intenta atacar.")]
        public float attackRange = 1.5f;
        [Tooltip("Si el jugador está más cerca que esto, la CPU puede retroceder.")]
        public float tooCloseDistance = 1.05f;

        [Header("Ritmo de decisiones")]
        [Tooltip("Segundos mínimos entre decisiones.")]
        public float minDecisionTime = 0.2f;
        [Tooltip("Segundos máximos entre decisiones.")]
        public float maxDecisionTime = 0.45f;
        [Tooltip("Pausa mínima entre ataques de la CPU.")]
        public float minAttackCooldown = 0.5f;
        [Tooltip("Pausa máxima entre ataques de la CPU.")]
        public float maxAttackCooldown = 1.2f;

        [Header("Probabilidades (0 a 1)")]
        [Range(0f, 1f)] public float heavyAttackChance = 0.3f;
        [Range(0f, 1f)] public float blockChance = 0.4f;
        [Range(0f, 1f)] public float retreatChance = 0.25f;

        [Header("Bloqueo")]
        public float blockHoldTime = 0.5f;
    }
}
