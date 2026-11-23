using System.Collections.Generic;
using System.Linq;
using FightingRivals.AI;
using FightingRivals.Arena;
using FightingRivals.Audio;
using FightingRivals.Characters;
using FightingRivals.Combat;
using FightingRivals.Controls;
using FightingRivals.Core;
using FightingRivals.Data;
using FightingRivals.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace FightingRivals.EditorTools
{
    // Revisa que las escenas, prefabs y datos estén completos y bien conectados.
    // En batch: escribe SCENE_VALIDATION_OK o SCENE_VALIDATION_FAILED (y sale con código 1).
    public static class GameSceneValidator
    {
        [MenuItem("Fighting Rivals/Validar escenas del prototipo")]
        public static void ValidateScenes()
        {
            var errors = new List<string>();

            ValidateBuildSettings(errors);
            ValidateData(errors);
            ValidateMainMenu(errors);
            ValidateFightScene(errors);

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    Debug.LogError("VALIDACIÓN: " + error);
                }

                Debug.LogError("SCENE_VALIDATION_FAILED");
                if (Application.isBatchMode)
                {
                    EditorApplication.Exit(1);
                }

                return;
            }

            Debug.Log("SCENE_VALIDATION_OK");
        }

        private static void ValidateBuildSettings(List<string> errors)
        {
            var scenes = EditorBuildSettings.scenes;

            if (scenes.Length != 2)
            {
                errors.Add("Build Settings debe contener exactamente dos escenas.");
                return;
            }

            if (scenes[0].path != GameSceneBuilder.MainMenuPath || !scenes[0].enabled)
            {
                errors.Add("MainMenu.unity debe ser la primera escena habilitada.");
            }

            if (scenes[1].path != GameSceneBuilder.FightScenePath || !scenes[1].enabled)
            {
                errors.Add("FightScene.unity debe ser la segunda escena habilitada.");
            }
        }

        private static void ValidateData(List<string> errors)
        {
            var controls = AssetDatabase.LoadAssetAtPath<InputActionAsset>(GameSceneBuilder.ControlsPath);
            if (controls == null)
            {
                errors.Add("Falta el archivo de controles " + GameSceneBuilder.ControlsPath);
            }
            else
            {
                foreach (var actionPath in new[] { "Fighter/Move", "Fighter/Jump", "Fighter/LightAttack", "Fighter/HeavyAttack", "Fighter/Block", "Game/Pause" })
                {
                    if (controls.FindAction(actionPath) == null)
                    {
                        errors.Add("Falta la acción de control '" + actionPath + "'.");
                    }
                }
            }

            ValidateStats(errors, GameSceneBuilder.PlayerStatsPath);
            ValidateStats(errors, GameSceneBuilder.CPUStatsPath);

            if (AssetDatabase.LoadAssetAtPath<RoundSettings>(GameSceneBuilder.RoundSettingsPath) == null)
            {
                errors.Add("Falta " + GameSceneBuilder.RoundSettingsPath);
            }

            if (AssetDatabase.LoadAssetAtPath<CPUSettings>(GameSceneBuilder.CPUSettingsPath) == null)
            {
                errors.Add("Falta " + GameSceneBuilder.CPUSettingsPath);
            }

            ValidateFighterPrefab(errors, GameSceneBuilder.PlayerPrefabPath, true);
            ValidateFighterPrefab(errors, GameSceneBuilder.CPUPrefabPath, false);
        }

        private static void ValidateStats(List<string> errors, string path)
        {
            var stats = AssetDatabase.LoadAssetAtPath<FighterStats>(path);
            if (stats == null)
            {
                errors.Add("Falta " + path);
                return;
            }

            if (stats.lightAttack == null || stats.heavyAttack == null)
            {
                errors.Add(path + " no tiene asignados los ataques ligero y pesado.");
            }

            if (stats.maxHealth <= 0)
            {
                errors.Add(path + " tiene vida máxima inválida.");
            }
        }

        private static void ValidateFighterPrefab(List<string> errors, string path, bool isPlayer)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                errors.Add("Falta el prefab " + path);
                return;
            }

            var controller = prefab.GetComponent<FighterController>();
            if (controller == null || controller.stats == null)
            {
                errors.Add(path + ": FighterController sin FighterStats.");
            }

            if (prefab.GetComponent<Health>() == null || prefab.GetComponent<FighterAttack>() == null)
            {
                errors.Add(path + ": faltan Health o FighterAttack.");
            }

            var attack = prefab.GetComponent<FighterAttack>();
            if (attack != null && attack.hitbox == null)
            {
                errors.Add(path + ": FighterAttack sin hitbox asignada.");
            }

            if (prefab.GetComponentInChildren<FighterVisual>(true) == null)
            {
                errors.Add(path + ": falta el objeto Visual con FighterVisual.");
            }

            if (isPlayer)
            {
                var input = prefab.GetComponent<PlayerInputHandler>();
                if (input == null || input.controls == null)
                {
                    errors.Add(path + ": PlayerInputHandler sin archivo de controles.");
                }

                if (prefab.GetComponent<CPUController>() != null)
                {
                    errors.Add(path + ": el jugador no debe tener CPUController.");
                }
            }
            else
            {
                var cpu = prefab.GetComponent<CPUController>();
                if (cpu == null || cpu.settings == null)
                {
                    errors.Add(path + ": CPUController sin CPUSettings.");
                }

                if (prefab.GetComponent<PlayerInputHandler>() != null)
                {
                    errors.Add(path + ": la CPU no debe tener PlayerInputHandler.");
                }
            }
        }

        private static void ValidateMainMenu(List<string> errors)
        {
            EditorSceneManager.OpenScene(GameSceneBuilder.MainMenuPath);
            ValidateCommonUI(errors, "MainMenu");

            var controller = FindSingle<MainMenuController>();
            if (controller == null)
            {
                errors.Add("MainMenu no tiene MainMenuController.");
            }
            else
            {
                RequireHiddenPanel(errors, controller.optionsPanel, "MainMenuController.optionsPanel");
                RequireHiddenPanel(errors, controller.creditsPanel, "MainMenuController.creditsPanel");
            }

            var options = FindSingle<OptionsController>();
            if (options == null || options.musicSlider == null || options.sfxSlider == null)
            {
                errors.Add("MainMenu: OptionsController sin sliders de volumen.");
            }
            else
            {
                if (options.musicSlider.onValueChanged.GetPersistentEventCount() == 0 ||
                    options.sfxSlider.onValueChanged.GetPersistentEventCount() == 0)
                {
                    errors.Add("MainMenu: los sliders de volumen no tienen eventos conectados.");
                }
            }

            RequireComponent<AudioManager>(errors, "MainMenu", "AudioManager");
            RequireWiredButton(errors, "PlayButton", 1);
            RequireWiredButton(errors, "OptionsButton", 1);
            RequireWiredButton(errors, "CreditsButton", 1);
            RequireWiredButton(errors, "BackButton", 2);
        }

        private static void ValidateFightScene(List<string> errors)
        {
            EditorSceneManager.OpenScene(GameSceneBuilder.FightScenePath);
            ValidateCommonUI(errors, "FightScene");

            RequireComponent<AudioManager>(errors, "FightScene", "AudioManager");

            var arena = FindSingle<ArenaBounds>();
            if (arena == null || arena.floor == null || arena.leftBoundary == null || arena.rightBoundary == null)
            {
                errors.Add("FightScene: ArenaBounds incompleto (suelo o límites).");
            }
            else if (arena.LeftLimit >= arena.RightLimit)
            {
                errors.Add("FightScene: el límite izquierdo debe estar a la izquierda del derecho.");
            }

            var fighters = Object.FindObjectsByType<FighterController>(FindObjectsInactive.Include);
            if (fighters.Length != 2)
            {
                errors.Add("FightScene debe tener exactamente 2 luchadores, tiene " + fighters.Length + ".");
            }

            var round = FindSingle<RoundManager>();
            if (round == null)
            {
                errors.Add("FightScene no tiene RoundManager.");
            }
            else
            {
                if (round.settings == null || round.player == null || round.cpu == null || round.hud == null || round.resultPanel == null)
                {
                    errors.Add("RoundManager tiene referencias sin asignar.");
                }
                else
                {
                    if (round.player.GetComponent<PlayerInputHandler>() == null)
                    {
                        errors.Add("RoundManager.player no es el luchador del jugador.");
                    }

                    if (round.cpu.GetComponent<CPUController>() == null)
                    {
                        errors.Add("RoundManager.cpu no es el luchador de la CPU.");
                    }

                    if (round.player.opponent != round.cpu || round.cpu.opponent != round.player)
                    {
                        errors.Add("Los luchadores no tienen asignado a su oponente.");
                    }

                    var cpuBrain = round.cpu.GetComponent<CPUController>();
                    if (cpuBrain != null && cpuBrain.target != round.player)
                    {
                        errors.Add("CPUController.target debe ser el jugador.");
                    }
                }

                if (round.hud != null && (round.hud.timerText == null || round.hud.messageText == null))
                {
                    errors.Add("CombatHUD sin textos de tiempo o mensaje.");
                }
            }

            var bars = Object.FindObjectsByType<HealthBarUI>(FindObjectsInactive.Include);
            if (bars.Length != 2 || bars.Any(bar => bar.fighter == null || bar.fill == null))
            {
                errors.Add("FightScene necesita 2 barras de vida con luchador y relleno asignados.");
            }

            var result = FindSingle<ResultPanelController>();
            if (result == null || result.resultText == null)
            {
                errors.Add("FightScene: ResultPanelController incompleto.");
            }
            else
            {
                RequireHiddenPanel(errors, result.resultPanel, "ResultPanelController.resultPanel");
            }

            var pause = FindSingle<PauseMenuController>();
            if (pause == null || pause.controls == null || pause.roundManager == null)
            {
                errors.Add("FightScene: PauseMenuController incompleto.");
            }
            else
            {
                RequireHiddenPanel(errors, pause.pausePanel, "PauseMenuController.pausePanel");
            }

            if (GameObject.Find("TemporaryResultPanel") != null)
            {
                errors.Add("FightScene todavía tiene el simulador temporal de resultado.");
            }

            RequireWiredButton(errors, "PauseButton", 1);
            RequireWiredButton(errors, "ContinueButton", 1);
            RequireWiredButton(errors, "RetryButton", 2);
            RequireWiredButton(errors, "MenuButton", 2);
        }

        private static void ValidateCommonUI(List<string> errors, string scene)
        {
            RequireComponent<GameManager>(errors, scene, "GameManager");
            RequireComponent<EventSystem>(errors, scene, "EventSystem");
            RequireComponent<InputSystemUIInputModule>(errors, scene, "InputSystemUIInputModule");

            var canvas = FindSingle<Canvas>();
            if (canvas == null)
            {
                errors.Add(scene + " no tiene Canvas.");
            }
            else
            {
                var scaler = canvas.GetComponent<CanvasScaler>();
                if (scaler == null || scaler.referenceResolution != new Vector2(1920f, 1080f))
                {
                    errors.Add(scene + " no usa CanvasScaler con resolución de referencia 1920x1080.");
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

        private static void RequireHiddenPanel(List<string> errors, GameObject panel, string label)
        {
            if (panel == null)
            {
                errors.Add(label + " no está asignado.");
            }
            else if (panel.activeSelf)
            {
                errors.Add(label + " debe iniciar oculto.");
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
