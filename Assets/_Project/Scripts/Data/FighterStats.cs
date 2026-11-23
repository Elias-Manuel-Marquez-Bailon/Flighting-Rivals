using UnityEngine;

namespace FightingRivals.Data
{
    // Estadísticas de un luchador. Cada personaje tendrá su propio archivo
    // en Assets/_Project/Data/Characters/.
    [CreateAssetMenu(fileName = "NewFighterStats", menuName = "Fighting Rivals/Estadísticas de luchador")]
    public class FighterStats : ScriptableObject
    {
        [Header("Identidad")]
        public string displayName = "Luchador";

        [Header("Vida")]
        public int maxHealth = 1000;

        [Header("Movimiento")]
        public float moveSpeed = 4f;
        [Tooltip("Multiplicador de velocidad al caminar hacia atrás.")]
        public float backwardSpeedMultiplier = 0.8f;
        public float jumpVelocity = 11f;
        public float gravityScale = 3f;

        [Header("Ataques")]
        public AttackData lightAttack;
        public AttackData heavyAttack;
    }
}
