using System;
using UnityEngine;

namespace FightingRivals.Combat
{
    // Vida de un luchador. Solo guarda el número y avisa cuando cambia o llega a cero.
    // El daño NO se aplica desde aquí directamente: lo hace CombatResolver.
    public class Health : MonoBehaviour
    {
        public event Action<int, int> HealthChanged; // (vida actual, vida máxima)
        public event Action Died;

        public int MaxHealth { get; private set; } = 1000;
        public int CurrentHealth { get; private set; } = 1000;
        public bool IsDead => CurrentHealth <= 0;

        public void Initialize(int maxHealth)
        {
            MaxHealth = Mathf.Max(1, maxHealth);
            CurrentHealth = MaxHealth;
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (IsDead || amount <= 0)
            {
                return;
            }

            // La vida nunca baja de cero.
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);

            if (IsDead)
            {
                Died?.Invoke();
            }
        }
    }
}
