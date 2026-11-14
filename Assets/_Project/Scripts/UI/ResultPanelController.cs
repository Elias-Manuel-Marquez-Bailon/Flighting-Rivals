using FightingRivals.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FightingRivals.UI
{
    public class ResultPanelController : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject resultPanel;
        public GameObject debugPanel;
        public Text resultText;

        public void ShowVictory()
        {
            ShowResult("GANASTE");
        }

        public void ShowDefeat()
        {
            ShowResult("PERDISTE");
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
            if (resultText != null)
            {
                resultText.text = message;
            }

            if (debugPanel != null)
            {
                debugPanel.SetActive(false);
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
