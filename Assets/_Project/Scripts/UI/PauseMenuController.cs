using FightingRivals.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace FightingRivals.UI
{
    // Menú de pausa de FightScene: Continuar, Reintentar y Menú principal.
    // Se abre con Escape (acción "Game/Pause") o con el botón PAUSA del HUD.
    public class PauseMenuController : MonoBehaviour
    {
        public GameManager gameManager;
        public RoundManager roundManager;
        public GameObject pausePanel;
        public InputActionAsset controls;

        public bool IsPaused { get; private set; }

        private InputActionAsset runtimeControls;
        private InputAction pauseAction;

        private void Awake()
        {
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }

            if (controls != null)
            {
                // Copia propia de los controles (igual que PlayerInputHandler).
                runtimeControls = Instantiate(controls);
                pauseAction = runtimeControls.FindAction("Game/Pause");
            }
        }

        private void OnEnable()
        {
            pauseAction?.Enable();
        }

        private void OnDisable()
        {
            pauseAction?.Disable();
        }

        private void OnDestroy()
        {
            // Si se sale de la escena estando en pausa, el tiempo vuelve a la normalidad.
            if (IsPaused)
            {
                Time.timeScale = 1f;
            }

            if (runtimeControls != null)
            {
                Destroy(runtimeControls);
            }
        }

        private void Update()
        {
            if (pauseAction != null && pauseAction.WasPressedThisFrame())
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Pause()
        {
            // No se puede pausar cuando el round ya terminó (se está mostrando el resultado).
            if (IsPaused || (roundManager != null && roundManager.IsRoundOver))
            {
                return;
            }

            IsPaused = true;
            Time.timeScale = 0f;

            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }
        }

        public void Resume()
        {
            if (!IsPaused)
            {
                return;
            }

            IsPaused = false;
            Time.timeScale = 1f;

            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }

            // Quita la selección del botón para que Espacio/Enter no lo vuelva a pulsar.
            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        public void Retry()
        {
            GetGameManager().RestartFight();
        }

        public void ReturnToMenu()
        {
            GetGameManager().LoadMainMenu();
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
