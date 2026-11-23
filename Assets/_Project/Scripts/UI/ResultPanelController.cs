using FightingRivals.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FightingRivals.UI
{
    // Pantalla de resultado (Ganaste / Perdiste / Empate) con Reintentar y Menú principal.
    // RoundManager decide el resultado y llama a ShowVictory, ShowDefeat o ShowDraw.
    public class ResultPanelController : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject resultPanel;
        public Text resultText;

        private bool resultShown;

        private void Awake()
        {
            if (resultPanel != null)
            {
                resultPanel.SetActive(false);
            }
        }

        public void ShowVictory()
        {
            ShowResult("GANASTE");
        }

        public void ShowDefeat()
        {
            ShowResult("PERDISTE");
        }

        public void ShowDraw()
        {
            ShowResult("EMPATE");
        }

        public void Retry()
        {
            GetGameManager().RestartFight();
        }

        public void ReturnToMenu()
        {
            GetGameManager().LoadMainMenu();
        }

        private void ShowResult(string message)
        {
            // El resultado solo se muestra una vez por pelea.
            if (resultShown)
            {
                return;
            }

            resultShown = true;

            if (resultText != null)
            {
                resultText.text = message;
            }

            if (resultPanel != null)
            {
                resultPanel.SetActive(true);
            }
        }

        private GameManager GetGameManager()
        {
            if (gameManager == null)
            {
                gameManager = FindAnyObjectByType<GameManager>();
            }

            return gameManager;
        }
    }
}
