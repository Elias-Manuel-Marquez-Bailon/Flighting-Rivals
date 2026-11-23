using UnityEngine;
using UnityEngine.UI;

namespace FightingRivals.UI
{
    // Textos del HUD de combate: temporizador y mensaje central ("¡PELEA!", "K.O.", "¡TIEMPO!").
    // Las barras de vida tienen su propio script: HealthBarUI.
    // Solo MUESTRA información; quien decide es RoundManager.
    public class CombatHUD : MonoBehaviour
    {
        public Text timerText;
        public Text messageText;

        private void Awake()
        {
            HideMessage();
        }

        public void SetTimer(float secondsLeft)
        {
            if (timerText != null)
            {
                timerText.text = Mathf.CeilToInt(Mathf.Max(0f, secondsLeft)).ToString();
            }
        }

        public void ShowMessage(string message)
        {
            if (messageText == null)
            {
                return;
            }

            messageText.text = message;
            messageText.gameObject.SetActive(true);
        }

        public void HideMessage()
        {
            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }
    }
}
