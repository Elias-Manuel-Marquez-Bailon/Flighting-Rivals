using FightingRivals.Characters;
using FightingRivals.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace FightingRivals.UI
{
    // Barra de vida de UN luchador. Hay una para el jugador y otra para la CPU.
    // Escucha los cambios de vida del luchador y ajusta el ancho del relleno.
    public class HealthBarUI : MonoBehaviour
    {
        public FighterController fighter;
        [Tooltip("Rectángulo de relleno (hijo de la barra).")]
        public RectTransform fill;
        public Text nameText;
        [Tooltip("Actívalo en la barra derecha para que se vacíe hacia la derecha.")]
        public bool fillFromRight;

        private Health health;

        private void Start()
        {
            if (fighter == null)
            {
                Debug.LogError("HealthBarUI: falta asignar el luchador.", this);
                return;
            }

            health = fighter.Health;
            health.HealthChanged += Refresh;
            Refresh(health.CurrentHealth, health.MaxHealth);

            if (nameText != null && fighter.stats != null)
            {
                nameText.text = fighter.stats.displayName;
            }
        }

        private void OnDestroy()
        {
            if (health != null)
            {
                health.HealthChanged -= Refresh;
            }
        }

        private void Refresh(int current, int max)
        {
            if (fill == null)
            {
                return;
            }

            var amount = max > 0 ? Mathf.Clamp01((float)current / max) : 0f;

            if (fillFromRight)
            {
                fill.anchorMin = new Vector2(1f - amount, 0f);
                fill.anchorMax = Vector2.one;
            }
            else
            {
                fill.anchorMin = Vector2.zero;
                fill.anchorMax = new Vector2(amount, 1f);
            }

            fill.offsetMin = Vector2.zero;
            fill.offsetMax = Vector2.zero;
        }
    }
}
