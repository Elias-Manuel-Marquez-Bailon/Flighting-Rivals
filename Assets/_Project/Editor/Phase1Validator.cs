using System.Collections.Generic;
using System.Linq;
using FightingRivals.Core;
using FightingRivals.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace FightingRivals.EditorTools
{
    public static class Phase1Validator
    {
        private const string MainMenuPath = "Assets/_Project/Scenes/MainMenu.unity";
        private const string FightScenePath = "Assets/_Project/Scenes/FightScene.unity";

        [MenuItem("Fighting Rivals/Fase 1/Validar esqueleto navegable")]
        public static void ValidatePhase1()
        {
            var errors = new List<string>();

            ValidateBuildSettings(errors);
            ValidateMainMenu(errors);
            ValidateFightScene(errors);

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    Debug.LogError("FASE 1: " + error);
                }

                Debug.LogError("PHASE1_VALIDATION_FAILED");
                EditorApplication.Exit(1);
                return;
            }

            Debug.Log("PHASE1_VALIDATION_OK");
        }

        private static void ValidateBuildSettings(List<string> errors)
        {
            var scenes = EditorBuildSettings.scenes;

            if (scenes.Length != 2)
            {
                errors.Add("Build Settings debe contener exactamente dos escenas.");
                return;
            }

            if (scenes[0].path != MainMenuPath || !scenes[0].enabled)
            {
                errors.Add("MainMenu.unity debe ser la primera escena habilitada.");
            }

            if (scenes[1].path != FightScenePath || !scenes[1].enabled)
            {
                errors.Add("FightScene.unity debe ser la segunda escena habilitada.");
            }
        }

        private static void ValidateMainMenu(List<string> errors)
        {
            EditorSceneManager.OpenScene(MainMenuPath);
            ValidateCommonUI(errors, "MainMenu");

            RequireComponent<GameManager>(errors, "MainMenu", "GameManager");
            RequireComponent<EventSystem>(errors, "MainMenu", "EventSystem");
            RequireComponent<InputSystemUIInputModule>(errors, "MainMenu", "InputSystemUIInputModule");

            var controller = FindSingle<MainMenuController>();
            if (controller == null)
            {
                errors.Add("MainMenu no tiene MainMenuController.");
            }
            else
            {
                if (controller.optionsPanel == null)
                {
                    errors.Add("MainMenuController.optionsPanel no está asignado.");
                }
                else if (controller.optionsPanel.activeSelf)
                {
                    errors.Add("OptionsPanel debe iniciar oculto.");
                }

                if (controller.creditsPanel == null)
                {
                    errors.Add("MainMenuController.creditsPanel no está asignado.");
                }
                else if (controller.creditsPanel.activeSelf)
                {
                    errors.Add("CreditsPanel debe iniciar oculto.");
                }
            }

            RequireWiredButton(errors, "PlayButton", 1);
            RequireWiredButton(errors, "OptionsButton", 1);
            RequireWiredButton(errors, "CreditsButton", 1);
            RequireWiredButton(errors, "BackButton", 2);
        }

        private static void ValidateFightScene(List<string> errors)
        {
            EditorSceneManager.OpenScene(FightScenePath);
            ValidateCommonUI(errors, "FightScene");

            RequireComponent<GameManager>(errors, "FightScene", "GameManager");
            RequireComponent<EventSystem>(errors, "FightScene", "EventSystem");
            RequireComponent<InputSystemUIInputModule>(errors, "FightScene", "InputSystemUIInputModule");

            var controller = FindSingle<ResultPanelController>();
            if (controller == null)
            {
                errors.Add("FightScene no tiene ResultPanelController.");
            }
            else
            {
                if (controller.resultPanel == null)
                {
                    errors.Add("ResultPanelController.resultPanel no está asignado.");
                }
                else if (controller.resultPanel.activeSelf)
                {
                    errors.Add("ResultPanel debe iniciar oculto.");
                }

                if (controller.debugPanel == null)
                {
                    errors.Add("ResultPanelController.debugPanel no está asignado.");
                }
                else if (!controller.debugPanel.activeSelf)
                {
                    errors.Add("El panel de resultado temporal debe iniciar visible.");
                }

                if (controller.resultText == null)
                {
                    errors.Add("ResultPanelController.resultText no está asignado.");
                }
            }

            RequireWiredButton(errors, "VictoryButton", 1);
            RequireWiredButton(errors, "DefeatButton", 1);
            RequireWiredButton(errors, "RetryButton", 1);
            RequireWiredButton(errors, "MenuButton", 2);
        }

        private static void ValidateCommonUI(List<string> errors, string scene)
        {
            var canvas = FindSingle<Canvas>();
            if (canvas == null)
            {
                errors.Add(scene + " no tiene Canvas.");
            }
            else
            {
                var scaler = canvas.GetComponent<CanvasScaler>();
                if (scaler == null)
                {
                    errors.Add(scene + " no tiene CanvasScaler.");
                }
                else if (scaler.referenceResolution != new Vector2(1920f, 1080f))
                {
                    errors.Add(scene + " no usa la resolución de referencia 1920x1080.");
                }
            }

            var camera = FindSingle<Camera>();
            if (camera == null)
            {
                errors.Add(scene + " no tiene cámara.");
            }
            else if (!camera.orthographic)
            {
                errors.Add(scene + " debe usar una cámara ortográfica.");
            }

            var texts = Object.FindObjectsByType<Text>(FindObjectsInactive.Include);
            if (texts.Length == 0)
            {
                errors.Add(scene + " no tiene textos de UI.");
            }
            else if (texts.Any(text => text.font == null))
            {
                errors.Add(scene + " tiene textos sin fuente asignada.");
            }
        }

        private static void RequireComponent<T>(List<string> errors, string scene, string label) where T : Component
        {
            if (FindSingle<T>() == null)
            {
                errors.Add(scene + " no tiene " + label + ".");
            }
        }

        private static void RequireWiredButton(List<string> errors, string buttonName, int minimumCount)
        {
            var wiredCount = Object.FindObjectsByType<Button>(FindObjectsInactive.Include)
                .Count(button => button.name == buttonName && button.onClick.GetPersistentEventCount() > 0);

            if (wiredCount < minimumCount)
            {
                errors.Add("Se esperaban al menos " + minimumCount + " botones '" + buttonName + "' con eventos, se encontraron " + wiredCount + ".");
            }
        }

        private static T FindSingle<T>() where T : Component
        {
            return Object.FindObjectsByType<T>(FindObjectsInactive.Include).FirstOrDefault();
        }
    }
}
