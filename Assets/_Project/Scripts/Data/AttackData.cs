using UnityEngine;

namespace FightingRivals.Data
{
    // Datos de UN ataque (ligero, pesado...). Se crea como archivo en Assets/_Project/Data/Combat/
    // y se puede ajustar desde el Inspector sin tocar código.
    [CreateAssetMenu(fileName = "NewAttack", menuName = "Fighting Rivals/Datos de ataque")]
    public class AttackData : ScriptableObject
    {
        [Header("Identidad")]
        public string attackName = "Ataque";

        [Header("Daño")]
        public int damage = 50;

        [Header("Tiempos (segundos)")]
        [Tooltip("Tiempo antes de que el golpe pueda conectar.")]
        public float startup = 0.08f;
        [Tooltip("Tiempo durante el cual la hitbox está activa.")]
        public float active = 0.1f;
        [Tooltip("Tiempo después del golpe en que el luchador no puede actuar.")]
        public float recovery = 0.2f;

        [Header("Hitbox (relativa al luchador mirando a la derecha)")]
        public Vector2 hitboxOffset = new Vector2(0.9f, 0.2f);
        public Vector2 hitboxSize = new Vector2(0.8f, 0.5f);

        [Header("Reacción del rival")]
        [Tooltip("Segundos que el rival queda aturdido al recibir el golpe.")]
        public float hitstun = 0.3f;
        [Tooltip("Segundos que el rival queda inmóvil si bloquea el golpe.")]
        public float blockstun = 0.15f;
        [Tooltip("Velocidad con la que el golpe empuja al rival.")]
        public float knockback = 3f;

        [Header("Sonidos (opcionales)")]
        [Tooltip("Suena al lanzar el ataque.")]
        public AudioClip swingSound;
        [Tooltip("Suena cuando el ataque conecta.")]
        public AudioClip hitSound;

        public float TotalDuration => startup + active + recovery;
    }
}
