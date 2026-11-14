using UnityEngine;
using UnityEngine.SceneManagement;

namespace FightingRivals.Core
{
    public class GameManager : MonoBehaviour
    {
        public const string MainMenuSceneName = "MainMenu";
        public const string FightSceneName = "FightScene";

        public void LoadMainMenu()
        {
            LoadScene(MainMenuSceneName);
        }

        public void LoadFightScene()
        {
            LoadScene(FightSceneName);
        }

        public void RestartFight()
        {
            LoadScene(FightSceneName);
        }

        private static void LoadScene(string sceneName)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }
    }
}
