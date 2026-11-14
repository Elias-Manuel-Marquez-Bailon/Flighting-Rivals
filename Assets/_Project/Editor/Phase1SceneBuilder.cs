using System.IO;
using FightingRivals.Core;
using FightingRivals.UI;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace FightingRivals.EditorTools
{
    public static class Phase1SceneBuilder
    {
        private const string MainMenuPath = "Assets/_Project/Scenes/MainMenu.unity";
        private const string FightScenePath = "Assets/_Project/Scenes/FightScene.unity";

        private static readonly Color BackgroundColor = new Color(0.055f, 0.06f, 0.085f, 1f);
        private static readonly Color PanelColor = new Color(0.09f, 0.10f, 0.14f, 0.96f);
        private static readonly Color AccentColor = new Color(0.86f, 0.22f, 0.26f, 1f);
        private static readonly Color TextColor = new Color(0.96f, 0.96f, 0.98f, 1f);
        private static readonly Color MutedColor = new Color(0.66f, 0.69f, 0.76f, 1f);
        private static readonly Color ButtonColor = new Color(0.16f, 0.18f, 0.24f, 1f);

        private static readonly string[] Folders =
        {
            "Assets/_Project",
            "Assets/_Project/Art",
            "Assets/_Project/Art/UI",
            "Assets/_Project/Audio",
            "Assets/_Project/Audio/Music",
            "Assets/_Project/Audio/SFX",
            "Assets/_Project/Data",
            "Assets/_Project/Editor",
            "Assets/_Project/Prefabs",
            "Assets/_Project/Prefabs/UI",
            "Assets/_Project/Scenes",
            "Assets/_Project/Scripts",
            "Assets/_Project/Scripts/Core",
            "Assets/_Project/Scripts/UI",
            "Assets/_Project/Settings"
        };

        [MenuItem("Fighting Rivals/Fase 1/Construir esqueleto navegable")]
        public static void BuildPhase1()
        {
            EnsureFolders();
            BuildMainMenu();
            BuildFightScene();

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(MainMenuPath, true),
                new EditorBuildSettingsScene(FightScenePath, true)
            };

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorSceneManager.OpenScene(MainMenuPath);

            Debug.Log("Fase 1 completada: MainMenu.unity y FightScene.unity creadas y añadidas a Build Settings.");
        }

        private static void BuildMainMenu()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            CreateCamera();
            var canvas = CreateCanvas();
            CreateEventSystem();

            var background = CreateImage("Background", canvas.transform, BackgroundColor);
            Stretch(background.rectTransform);

            var accentBar = CreateImage("AccentBar", canvas.transform, AccentColor);
            Place(accentBar.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -110f), new Vector2(1920f, 8f));

            var title = CreateText("Title", canvas.transform, "FIGHTING RIVALS", 92, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(title.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -170f), new Vector2(1500f, 120f));

            var subtitle = CreateText("Subtitle", canvas.transform, "Prototipo 2D - esqueleto navegable", 30, MutedColor, TextAnchor.MiddleCenter);
            Place(subtitle.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -292f), new Vector2(1000f, 50f));

            var menuContainer = CreateImage("MenuContainer", canvas.transform, PanelColor);
            Place(menuContainer.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -30f), new Vector2(600f, 420f));

            var playButton = CreateButton("PlayButton", menuContainer.transform, "JUGAR", 34);
            Place(playButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 110f), new Vector2(500f, 84f));

            var optionsButton = CreateButton("OptionsButton", menuContainer.transform, "OPCIONES", 34);
            Place(optionsButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), new Vector2(500f, 84f));

            var creditsButton = CreateButton("CreditsButton", menuContainer.transform, "CRÉDITOS", 34);
            Place(creditsButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -110f), new Vector2(500f, 84f));

            var footer = CreateText("Footer", canvas.transform, "Proyecto académico - Fase 1", 20, MutedColor, TextAnchor.MiddleCenter);
            Place(footer.rectTransform, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 36f), new Vector2(1000f, 40f));

            var systems = new GameObject("GameSystems");
            var gameManager = systems.AddComponent<GameManager>();
            var menuController = systems.AddComponent<MainMenuController>();
            menuController.gameManager = gameManager;

            UnityEventTools.AddPersistentListener(playButton.onClick, menuController.Play);
            UnityEventTools.AddPersistentListener(optionsButton.onClick, menuController.OpenOptions);
            UnityEventTools.AddPersistentListener(creditsButton.onClick, menuController.OpenCredits);

            var optionsPanel = CreateOverlay("OptionsPanel", canvas.transform);
            var optionsTitle = CreateText("Title", optionsPanel.transform, "OPCIONES", 72, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(optionsTitle.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -180f), new Vector2(1200f, 100f));

            var optionsBody = CreateText("Body", optionsPanel.transform,
                "Volumen de música y efectos: se implementará en la Fase 6.\n\nPor ahora este panel solo valida la navegación del menú.",
                30, MutedColor, TextAnchor.MiddleCenter);
            Place(optionsBody.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 20f), new Vector2(1200f, 220f));

            var optionsBack = CreateButton("BackButton", optionsPanel.transform, "VOLVER", 30);
            Place(optionsBack.GetComponent<RectTransform>(), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 140f), new Vector2(340f, 80f));
            UnityEventTools.AddPersistentListener(optionsBack.onClick, menuController.ClosePanels);

            var creditsPanel = CreateOverlay("CreditsPanel", canvas.transform);
            var creditsTitle = CreateText("Title", creditsPanel.transform, "CRÉDITOS", 72, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(creditsTitle.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -180f), new Vector2(1200f, 100f));

            var creditsBody = CreateText("Body", creditsPanel.transform,
                "Equipo: [pendiente]\nArte y audio: pendientes de registrar\n\nLos recursos externos se documentan en Docs/ASSET_SOURCES.md",
                28, MutedColor, TextAnchor.MiddleCenter);
            Place(creditsBody.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 20f), new Vector2(1200f, 260f));

            var creditsBack = CreateButton("BackButton", creditsPanel.transform, "VOLVER", 30);
            Place(creditsBack.GetComponent<RectTransform>(), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 140f), new Vector2(340f, 80f));
            UnityEventTools.AddPersistentListener(creditsBack.onClick, menuController.ClosePanels);

            menuController.optionsPanel = optionsPanel;
            menuController.creditsPanel = creditsPanel;

            optionsPanel.SetActive(false);
            creditsPanel.SetActive(false);

            EditorSceneManager.SaveScene(scene, MainMenuPath);
        }

        private static void BuildFightScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            CreateCamera();
            var canvas = CreateCanvas();
            CreateEventSystem();

            var background = CreateImage("Background", canvas.transform, BackgroundColor);
            Stretch(background.rectTransform);

            var accentBar = CreateImage("AccentBar", canvas.transform, AccentColor);
            Place(accentBar.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -110f), new Vector2(1920f, 8f));

            var title = CreateText("Title", canvas.transform, "PELEA", 84, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(title.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -160f), new Vector2(1200f, 110f));

            var subtitle = CreateText("Subtitle", canvas.transform, "Esqueleto de Fase 1 - sin combate todavía", 30, MutedColor, TextAnchor.MiddleCenter);
            Place(subtitle.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -280f), new Vector2(1200f, 50f));

            var placeholder = CreateText("ArenaPlaceholder", canvas.transform, "Aquí irá la arena del salón de clases.", 36, MutedColor, TextAnchor.MiddleCenter);
            Place(placeholder.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -40f), new Vector2(1200f, 80f));

            var menuButton = CreateButton("MenuButton", canvas.transform, "MENÚ", 24);
            Place(menuButton.GetComponent<RectTransform>(), new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-32f, -32f), new Vector2(220f, 64f));

            var debugPanel = CreateImage("TemporaryResultPanel", canvas.transform, PanelColor).gameObject;
            Place(debugPanel.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(32f, -32f), new Vector2(540f, 240f));

            var debugTitle = CreateText("Title", debugPanel.transform, "SIMULADOR TEMPORAL", 24, AccentColor, TextAnchor.MiddleLeft, FontStyle.Bold);
            Place(debugTitle.rectTransform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(24f, -18f), new Vector2(492f, 34f));

            var debugNote = CreateText("Note", debugPanel.transform, "Eliminar cuando exista combate real.", 17, MutedColor, TextAnchor.MiddleLeft);
            Place(debugNote.rectTransform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(24f, -54f), new Vector2(492f, 28f));

            var winButton = CreateButton("VictoryButton", debugPanel.transform, "GANASTE", 24);
            Place(winButton.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(24f, -104f), new Vector2(236f, 72f));

            var loseButton = CreateButton("DefeatButton", debugPanel.transform, "PERDISTE", 24);
            Place(loseButton.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(280f, -104f), new Vector2(236f, 72f));

            var resultPanel = CreateOverlay("ResultPanel", canvas.transform);
            var resultText = CreateText("ResultText", resultPanel.transform, "GANASTE", 112, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(resultText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 120f), new Vector2(1600f, 180f));

            var resultNote = CreateText("Note", resultPanel.transform, "Resultado simulado (temporal)", 26, MutedColor, TextAnchor.MiddleCenter);
            Place(resultNote.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 20f), new Vector2(1000f, 50f));

            var retryButton = CreateButton("RetryButton", resultPanel.transform, "REINTENTAR", 28);
            Place(retryButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(-200f, -90f), new Vector2(360f, 84f));

            var resultMenuButton = CreateButton("MenuButton", resultPanel.transform, "MENÚ PRINCIPAL", 28);
            Place(resultMenuButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(200f, -90f), new Vector2(360f, 84f));

            var systems = new GameObject("GameSystems");
            var gameManager = systems.AddComponent<GameManager>();
            var resultController = systems.AddComponent<ResultPanelController>();
            resultController.gameManager = gameManager;
            resultController.resultPanel = resultPanel;
            resultController.debugPanel = debugPanel;
            resultController.resultText = resultText;

            UnityEventTools.AddPersistentListener(winButton.onClick, resultController.ShowVictory);
            UnityEventTools.AddPersistentListener(loseButton.onClick, resultController.ShowDefeat);
            UnityEventTools.AddPersistentListener(retryButton.onClick, resultController.Retry);
            UnityEventTools.AddPersistentListener(resultMenuButton.onClick, resultController.ReturnToMenu);
            UnityEventTools.AddPersistentListener(menuButton.onClick, gameManager.LoadMainMenu);

            resultPanel.SetActive(false);

            EditorSceneManager.SaveScene(scene, FightScenePath);
        }

        private static void CreateCamera()
        {
            var cameraObject = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
            cameraObject.tag = "MainCamera";

            var camera = cameraObject.GetComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 5.4f;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = BackgroundColor;
            cameraObject.transform.position = new Vector3(0f, 0f, -10f);
        }

        private static Canvas CreateCanvas()
        {
            var canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));

            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            return canvas;
        }

        private static void CreateEventSystem()
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
        }

        private static Image CreateImage(string name, Transform parent, Color color)
        {
            var rect = CreateRect(name, parent);
            var image = rect.gameObject.AddComponent<Image>();
            image.color = color;
            return image;
        }

        private static Text CreateText(string name, Transform parent, string content, int fontSize, Color color, TextAnchor alignment, FontStyle style = FontStyle.Normal)
        {
            var rect = CreateRect(name, parent);
            var text = rect.gameObject.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.text = content;
            text.fontSize = fontSize;
            text.color = color;
            text.alignment = alignment;
            text.fontStyle = style;
            text.raycastTarget = false;
            return text;
        }

        private static Button CreateButton(string name, Transform parent, string label, int fontSize)
        {
            var rect = CreateRect(name, parent);
            var image = rect.gameObject.AddComponent<Image>();
            image.color = ButtonColor;

            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = image;

            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(0.86f, 0.89f, 1f, 1f);
            colors.pressedColor = new Color(0.68f, 0.72f, 0.88f, 1f);
            colors.selectedColor = Color.white;
            button.colors = colors;

            var text = CreateText("Label", rect, label, fontSize, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Stretch(text.rectTransform, 12f, 4f, 12f, 4f);

            return button;
        }

        private static GameObject CreateOverlay(string name, Transform parent)
        {
            var image = CreateImage(name, parent, new Color(0.03f, 0.03f, 0.05f, 0.95f));
            Stretch(image.rectTransform);
            return image.gameObject;
        }

        private static RectTransform CreateRect(string name, Transform parent)
        {
            var gameObject = new GameObject(name, typeof(RectTransform));
            var rect = gameObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            return rect;
        }

        private static void Stretch(RectTransform rect, float left = 0f, float bottom = 0f, float right = 0f, float top = 0f)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(left, bottom);
            rect.offsetMax = new Vector2(-right, -top);
        }

        private static void Place(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot, Vector2 position, Vector2 size)
        {
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.pivot = pivot;
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
        }

        private static void EnsureFolders()
        {
            foreach (var folder in Folders)
            {
                EnsureFolder(folder);
            }
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
            {
                return;
            }

            var parent = Path.GetDirectoryName(path)?.Replace('\\', '/');
            var folderName = Path.GetFileName(path);

            if (!string.IsNullOrEmpty(parent))
            {
                EnsureFolder(parent);
            }

            AssetDatabase.CreateFolder(parent, folderName);
        }
    }
}
