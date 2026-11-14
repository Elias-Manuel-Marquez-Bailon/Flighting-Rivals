using FightingRivals.Core;
using UnityEngine;

namespace FightingRivals.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject optionsPanel;
        public GameObject creditsPanel;

        private void Awake()
        {
            ClosePanels();
        }

        public void Play()
        {
            GetGameManager().LoadFightScene();
        }

        public void OpenOptions()
        {
            SetPanel(optionsPanel, true);
            SetPanel(creditsPanel, false);
        }

        public void OpenCredits()
        {
            SetPanel(creditsPanel, true);
            SetPanel(optionsPanel, false);
        }

        public void ClosePanels()
        {
            SetPanel(optionsPanel, false);
            SetPanel(creditsPanel, false);
        }

        private GameManager GetGameManager()
        {
            if (gameManager == null)
            {
                gameManager = FindAnyObjectByType<GameManager>();
            }

            return gameManager;
        }

        private static void SetPanel(GameObject panel, bool visible)
        {
            if (panel != null)
            {
                panel.SetActive(visible);
            }
        }
    }
}
