using System.IO;
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
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace FightingRivals.EditorTools
{
    // Construye por código las escenas MainMenu y FightScene, los prefabs de los luchadores
    // y los archivos de datos (vida, daño, tiempo, CPU).
    //
    // IMPORTANTE:
    //   - Las ESCENAS se sobrescriben siempre. No lo ejecutes si alguien editó las escenas a mano.
    //   - Los PREFABS y los DATOS solo se crean si no existen, así no se pierden los sprites
    //     ni los valores ajustados por el equipo. Para recrear los prefabs usa
    //     "Regenerar prefabs de luchadores".
    public static class GameSceneBuilder
    {
        public const string MainMenuPath = "Assets/_Project/Scenes/MainMenu.unity";
        public const string FightScenePath = "Assets/_Project/Scenes/FightScene.unity";
        public const string ControlsPath = "Assets/_Project/Settings/FightingRivalsControls.inputactions";
        public const string SquareSpritePath = "Assets/_Project/Art/Placeholders/Square.png";

        public const string LightAttackPath = "Assets/_Project/Data/Combat/LightAttack.asset";
        public const string HeavyAttackPath = "Assets/_Project/Data/Combat/HeavyAttack.asset";
        public const string PlayerStatsPath = "Assets/_Project/Data/Characters/PlayerStats.asset";
        public const string CPUStatsPath = "Assets/_Project/Data/Characters/CPUStats.asset";
        public const string RoundSettingsPath = "Assets/_Project/Data/RoundSettings.asset";
        public const string CPUSettingsPath = "Assets/_Project/Data/CPUSettings.asset";

        public const string PlayerPrefabPath = "Assets/_Project/Prefabs/Characters/PlayerFighter.prefab";
        public const string CPUPrefabPath = "Assets/_Project/Prefabs/Characters/CPUFighter.prefab";

        private static readonly Color BackgroundColor = new Color(0.055f, 0.06f, 0.085f, 1f);
        private static readonly Color PanelColor = new Color(0.09f, 0.10f, 0.14f, 0.96f);
        private static readonly Color AccentColor = new Color(0.86f, 0.22f, 0.26f, 1f);
        private static readonly Color TextColor = new Color(0.96f, 0.96f, 0.98f, 1f);
        private static readonly Color MutedColor = new Color(0.66f, 0.69f, 0.76f, 1f);
        private static readonly Color ButtonColor = new Color(0.16f, 0.18f, 0.24f, 1f);
        private static readonly Color PlayerColor = new Color(0.25f, 0.55f, 1f, 1f);
        private static readonly Color CPUColor = new Color(0.92f, 0.3f, 0.25f, 1f);
        private static readonly Color HealthColor = new Color(0.98f, 0.78f, 0.18f, 1f);

        private static readonly string[] Folders =
        {
            "Assets/_Project/Art/Placeholders",
            "Assets/_Project/Art/UI",
            "Assets/_Project/Audio/Music",
            "Assets/_Project/Audio/SFX",
            "Assets/_Project/Data/Characters",
            "Assets/_Project/Data/Combat",
            "Assets/_Project/Prefabs/Characters",
            "Assets/_Project/Prefabs/UI",
            "Assets/_Project/Scenes",
            "Assets/_Project/Settings"
        };

        private class GameData
        {
            public Sprite square;
            public InputActionAsset controls;
            public FighterStats playerStats;
            public FighterStats cpuStats;
            public RoundSettings roundSettings;
            public CPUSettings cpuSettings;
            public GameObject playerPrefab;
            public GameObject cpuPrefab;
        }

        [MenuItem("Fighting Rivals/Construir escenas del prototipo")]
        public static void BuildAll()
        {
            EnsureFolders();

            var data = PrepareData();
            if (data == null)
            {
                return;
            }

            BuildMainMenu();
            BuildFightScene(data);

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(MainMenuPath, true),
                new EditorBuildSettingsScene(FightScenePath, true)
            };

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorSceneManager.OpenScene(MainMenuPath);

            Debug.Log("Escenas del prototipo construidas: MainMenu.unity y FightScene.unity.");
        }

        [MenuItem("Fighting Rivals/Regenerar prefabs de luchadores (sobrescribe)")]
        public static void RegenerateFighterPrefabs()
        {
            if (!Application.isBatchMode &&
                !EditorUtility.DisplayDialog("Regenerar prefabs",
                    "Se borrarán PlayerFighter.prefab y CPUFighter.prefab (incluidos sprites o cambios hechos a mano) y se reconstruirán las escenas. ¿Continuar?",
                    "Sí, regenerar", "Cancelar"))
            {
                return;
            }

            AssetDatabase.DeleteAsset(PlayerPrefabPath);
            AssetDatabase.DeleteAsset(CPUPrefabPath);
            BuildAll();
        }

        // ---------------- Datos, sprite provisional y prefabs ----------------

        private static GameData PrepareData()
        {
            var data = new GameData();

            data.controls = AssetDatabase.LoadAssetAtPath<InputActionAsset>(ControlsPath);
            if (data.controls == null)
            {
                Debug.LogError("No se encontró el archivo de controles en " + ControlsPath);
                return null;
            }

            data.square = EnsurePlaceholderSprite();

            var lightAttack = EnsureAsset<AttackData>(LightAttackPath, attack =>
            {
                attack.attackName = "Ataque ligero";
                attack.damage = 50;
                attack.startup = 0.08f;
                attack.active = 0.1f;
                attack.recovery = 0.2f;
                attack.hitboxOffset = new Vector2(0.85f, 0.25f);
                attack.hitboxSize = new Vector2(0.9f, 0.5f);
                attack.hitstun = 0.3f;
                attack.blockstun = 0.15f;
                attack.knockback = 3f;
            });

            var heavyAttack = EnsureAsset<AttackData>(HeavyAttackPath, attack =>
            {
                attack.attackName = "Ataque pesado";
                attack.damage = 120;
                attack.startup = 0.22f;
                attack.active = 0.12f;
                attack.recovery = 0.4f;
                attack.hitboxOffset = new Vector2(1f, 0.2f);
                attack.hitboxSize = new Vector2(1.2f, 0.6f);
                attack.hitstun = 0.5f;
                attack.blockstun = 0.25f;
                attack.knockback = 6f;
            });

            data.playerStats = EnsureAsset<FighterStats>(PlayerStatsPath, stats =>
            {
                stats.displayName = "JUGADOR";
                stats.lightAttack = lightAttack;
                stats.heavyAttack = heavyAttack;
            });

            data.cpuStats = EnsureAsset<FighterStats>(CPUStatsPath, stats =>
            {
                stats.displayName = "CPU";
                stats.moveSpeed = 3.6f;
                stats.lightAttack = lightAttack;
                stats.heavyAttack = heavyAttack;
            });

            data.roundSettings = EnsureAsset<RoundSettings>(RoundSettingsPath, null);
            data.cpuSettings = EnsureAsset<CPUSettings>(CPUSettingsPath, null);

            data.playerPrefab = EnsureFighterPrefab(PlayerPrefabPath, "PlayerFighter", data.playerStats, PlayerColor, data, true);
            data.cpuPrefab = EnsureFighterPrefab(CPUPrefabPath, "CPUFighter", data.cpuStats, CPUColor, data, false);

            AssetDatabase.SaveAssets();
            return data;
        }

        private static T EnsureAsset<T>(string path, System.Action<T> setup) where T : ScriptableObject
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null)
            {
                return asset;
            }

            asset = ScriptableObject.CreateInstance<T>();
            setup?.Invoke(asset);
            AssetDatabase.CreateAsset(asset, path);
            return asset;
        }

        // Cuadrado blanco de 1x1 unidades; se colorea para hacer luchadores y escenario provisionales.
        private static Sprite EnsurePlaceholderSprite()
        {
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(SquareSpritePath);
            if (sprite != null)
            {
                return sprite;
            }

            var texture = new Texture2D(16, 16, TextureFormat.RGBA32, false);
            var pixels = new Color32[16 * 16];
            for (var i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Color32(255, 255, 255, 255);
            }

            texture.SetPixels32(pixels);
            texture.Apply();
            File.WriteAllBytes(SquareSpritePath, texture.EncodeToPNG());
            Object.DestroyImmediate(texture);

            AssetDatabase.ImportAsset(SquareSpritePath);
            var importer = (TextureImporter)AssetImporter.GetAtPath(SquareSpritePath);
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.spritePixelsPerUnit = 16f;
            importer.filterMode = FilterMode.Point;
            importer.mipmapEnabled = false;
            importer.SaveAndReimport();

            return AssetDatabase.LoadAssetAtPath<Sprite>(SquareSpritePath);
        }

        private static GameObject EnsureFighterPrefab(string path, string name, FighterStats stats, Color color, GameData data, bool isPlayer)
        {
            var existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (existing != null)
            {
                return existing;
            }

            var root = new GameObject(name);

            var body = root.AddComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Dynamic;
            body.gravityScale = stats.gravityScale;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;

            var bodyCollider = root.AddComponent<BoxCollider2D>();
            bodyCollider.size = new Vector2(1f, 2f);

            root.AddComponent<Health>();
            var attack = root.AddComponent<FighterAttack>();
            var controller = root.AddComponent<FighterController>();
            controller.stats = stats;
            root.AddComponent<FighterAudio>();

            // Visual: aquí el equipo de arte pondrá el sprite real (y el Animator).
            var visual = new GameObject("Visual");
            visual.transform.SetParent(root.transform, false);
            visual.transform.localScale = new Vector3(1f, 2f, 1f);
            var renderer = visual.AddComponent<SpriteRenderer>();
            renderer.sprite = data.square;
            renderer.color = color;
            renderer.sortingOrder = 0;
            visual.AddComponent<FighterVisual>();

            // Hitbox: zona de golpe; el rectángulo amarillo solo se ve mientras el ataque está activo.
            var hitboxObject = new GameObject("Hitbox");
            hitboxObject.transform.SetParent(root.transform, false);
            var hitboxRenderer = hitboxObject.AddComponent<SpriteRenderer>();
            hitboxRenderer.sprite = data.square;
            hitboxRenderer.color = new Color(1f, 0.85f, 0.1f, 0.55f);
            hitboxRenderer.sortingOrder = 5;
            hitboxRenderer.enabled = false;
            var hitbox = hitboxObject.AddComponent<AttackHitbox>();
            hitbox.debugRenderer = hitboxRenderer;
            attack.hitbox = hitbox;

            if (isPlayer)
            {
                var input = root.AddComponent<PlayerInputHandler>();
                input.controls = data.controls;
            }
            else
            {
                var cpu = root.AddComponent<CPUController>();
                cpu.settings = data.cpuSettings;
            }

            var prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
            Object.DestroyImmediate(root);
            return prefab;
        }

        // ---------------- Menú principal ----------------

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

            var subtitle = CreateText("Subtitle", canvas.transform, "Prototipo 2D - 1 contra 1", 30, MutedColor, TextAnchor.MiddleCenter);
            Place(subtitle.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -292f), new Vector2(1000f, 50f));

            var menuContainer = CreateImage("MenuContainer", canvas.transform, PanelColor);
            Place(menuContainer.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -30f), new Vector2(600f, 420f));

            var playButton = CreateButton("PlayButton", menuContainer.transform, "JUGAR", 34);
            Place(playButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 110f), new Vector2(500f, 84f));

            var optionsButton = CreateButton("OptionsButton", menuContainer.transform, "OPCIONES", 34);
            Place(optionsButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), new Vector2(500f, 84f));

            var creditsButton = CreateButton("CreditsButton", menuContainer.transform, "CRÉDITOS", 34);
            Place(creditsButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -110f), new Vector2(500f, 84f));

            var footer = CreateText("Footer", canvas.transform, "Proyecto académico - Prototipo", 20, MutedColor, TextAnchor.MiddleCenter);
            Place(footer.rectTransform, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 36f), new Vector2(1000f, 40f));

            var systems = new GameObject("GameSystems");
            var gameManager = systems.AddComponent<GameManager>();
            var menuController = systems.AddComponent<MainMenuController>();
            var optionsController = systems.AddComponent<OptionsController>();
            systems.AddComponent<AudioManager>();
            menuController.gameManager = gameManager;

            UnityEventTools.AddPersistentListener(playButton.onClick, menuController.Play);
            UnityEventTools.AddPersistentListener(optionsButton.onClick, menuController.OpenOptions);
            UnityEventTools.AddPersistentListener(creditsButton.onClick, menuController.OpenCredits);

            // Panel de opciones con volumen de música y efectos.
            var optionsPanel = CreateOverlay("OptionsPanel", canvas.transform);
            var optionsTitle = CreateText("Title", optionsPanel.transform, "OPCIONES", 72, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(optionsTitle.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -180f), new Vector2(1200f, 100f));

            var musicLabel = CreateText("MusicLabel", optionsPanel.transform, "Música", 34, TextColor, TextAnchor.MiddleRight);
            Place(musicLabel.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(1f, 0.5f), new Vector2(-40f, 80f), new Vector2(400f, 60f));
            var musicSlider = CreateSlider("MusicSlider", optionsPanel.transform);
            Place(musicSlider.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 80f), new Vector2(520f, 40f));

            var sfxLabel = CreateText("SfxLabel", optionsPanel.transform, "Efectos", 34, TextColor, TextAnchor.MiddleRight);
            Place(sfxLabel.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(1f, 0.5f), new Vector2(-40f, -20f), new Vector2(400f, 60f));
            var sfxSlider = CreateSlider("SfxSlider", optionsPanel.transform);
            Place(sfxSlider.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, -20f), new Vector2(520f, 40f));

            optionsController.musicSlider = musicSlider;
            optionsController.sfxSlider = sfxSlider;
            UnityEventTools.AddPersistentListener(musicSlider.onValueChanged, optionsController.SetMusicVolume);
            UnityEventTools.AddPersistentListener(sfxSlider.onValueChanged, optionsController.SetSfxVolume);

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

        // ---------------- Escena de pelea ----------------

        private static void BuildFightScene(GameData data)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            CreateCamera();
            var canvas = CreateCanvas();
            CreateEventSystem();

            // Arena provisional: salón de clases hecho con rectángulos de color.
            var arenaObject = new GameObject("Arena");
            var arena = arenaObject.AddComponent<ArenaBounds>();

            CreateSprite("Background", arenaObject.transform, data.square, new Color(0.17f, 0.18f, 0.24f), new Vector2(0f, 0f), new Vector2(22f, 12f), -20);
            CreateSprite("Chalkboard", arenaObject.transform, data.square, new Color(0.12f, 0.3f, 0.22f), new Vector2(0f, 1.3f), new Vector2(9f, 3.6f), -15);
            CreateSprite("LeftWindow", arenaObject.transform, data.square, new Color(0.35f, 0.5f, 0.65f), new Vector2(-7.2f, 1.6f), new Vector2(2.4f, 3f), -15);
            CreateSprite("RightWindow", arenaObject.transform, data.square, new Color(0.35f, 0.5f, 0.65f), new Vector2(7.2f, 1.6f), new Vector2(2.4f, 3f), -15);

            var floor = CreateSprite("Floor", arenaObject.transform, data.square, new Color(0.38f, 0.28f, 0.2f), new Vector2(0f, -4.5f), new Vector2(22f, 2f), -10);
            arena.floor = floor.AddComponent<BoxCollider2D>();

            var leftBoundary = new GameObject("LeftBoundary");
            leftBoundary.transform.SetParent(arenaObject.transform, false);
            leftBoundary.transform.position = new Vector3(-8.5f, 0f, 0f);
            arena.leftBoundary = leftBoundary.transform;

            var rightBoundary = new GameObject("RightBoundary");
            rightBoundary.transform.SetParent(arenaObject.transform, false);
            rightBoundary.transform.position = new Vector3(8.5f, 0f, 0f);
            arena.rightBoundary = rightBoundary.transform;

            // Luchadores (instancias de los prefabs).
            var fightersRoot = new GameObject("Fighters");
            var player = InstantiateFighter(data.playerPrefab, fightersRoot.transform, new Vector2(-3f, -2.4f));
            var cpu = InstantiateFighter(data.cpuPrefab, fightersRoot.transform, new Vector2(3f, -2.4f));
            player.opponent = cpu;
            player.arena = arena;
            cpu.opponent = player;
            cpu.arena = arena;
            var cpuBrain = cpu.GetComponent<CPUController>();
            cpuBrain.target = player;

            // Los cambios a instancias de prefab hechos por código deben registrarse para que se guarden.
            PrefabUtility.RecordPrefabInstancePropertyModifications(player);
            PrefabUtility.RecordPrefabInstancePropertyModifications(cpu);
            PrefabUtility.RecordPrefabInstancePropertyModifications(cpuBrain);

            // HUD
            var playerBar = CreateHealthBar("PlayerHealthBar", canvas.transform, player, false);
            Place(playerBar.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(40f, -40f), new Vector2(760f, 44f));

            var cpuBar = CreateHealthBar("CPUHealthBar", canvas.transform, cpu, true);
            Place(cpuBar.GetComponent<RectTransform>(), new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-40f, -40f), new Vector2(760f, 44f));

            var timerText = CreateText("TimerText", canvas.transform, "60", 72, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(timerText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -20f), new Vector2(200f, 100f));

            var pauseButton = CreateButton("PauseButton", canvas.transform, "PAUSA", 24);
            Place(pauseButton.GetComponent<RectTransform>(), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -130f), new Vector2(180f, 56f));

            var messageText = CreateText("MessageText", canvas.transform, "¡PELEA!", 140, AccentColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(messageText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 120f), new Vector2(1600f, 200f));
            var outline = messageText.gameObject.AddComponent<Outline>();
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(4f, -4f);

            var hint = CreateText("ControlsHint", canvas.transform,
                "A/D o flechas: mover   ·   Espacio: saltar   ·   J: ataque ligero   ·   K: ataque pesado   ·   L: bloquear   ·   Esc: pausa",
                22, MutedColor, TextAnchor.MiddleCenter);
            Place(hint.rectTransform, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 24f), new Vector2(1800f, 40f));

            // Panel de pausa
            var pausePanel = CreateOverlay("PausePanel", canvas.transform);
            var pauseTitle = CreateText("Title", pausePanel.transform, "PAUSA", 96, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(pauseTitle.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 220f), new Vector2(1200f, 140f));

            var continueButton = CreateButton("ContinueButton", pausePanel.transform, "CONTINUAR", 30);
            Place(continueButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 60f), new Vector2(420f, 84f));
            var pauseRetryButton = CreateButton("RetryButton", pausePanel.transform, "REINTENTAR", 30);
            Place(pauseRetryButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -50f), new Vector2(420f, 84f));
            var pauseMenuButton = CreateButton("MenuButton", pausePanel.transform, "MENÚ PRINCIPAL", 30);
            Place(pauseMenuButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -160f), new Vector2(420f, 84f));

            // Panel de resultado
            var resultPanel = CreateOverlay("ResultPanel", canvas.transform);
            var resultText = CreateText("ResultText", resultPanel.transform, "GANASTE", 112, TextColor, TextAnchor.MiddleCenter, FontStyle.Bold);
            Place(resultText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 120f), new Vector2(1600f, 180f));

            var retryButton = CreateButton("RetryButton", resultPanel.transform, "REINTENTAR", 28);
            Place(retryButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(-200f, -60f), new Vector2(360f, 84f));
            var resultMenuButton = CreateButton("MenuButton", resultPanel.transform, "MENÚ PRINCIPAL", 28);
            Place(resultMenuButton.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(200f, -60f), new Vector2(360f, 84f));

            // Sistemas
            var systems = new GameObject("GameSystems");
            var gameManager = systems.AddComponent<GameManager>();
            var roundManager = systems.AddComponent<RoundManager>();
            var resultController = systems.AddComponent<ResultPanelController>();
            var pauseController = systems.AddComponent<PauseMenuController>();
            var hud = canvas.gameObject.AddComponent<CombatHUD>();
            systems.AddComponent<AudioManager>();

            hud.timerText = timerText;
            hud.messageText = messageText;

            resultController.gameManager = gameManager;
            resultController.resultPanel = resultPanel;
            resultController.resultText = resultText;

            roundManager.settings = data.roundSettings;
            roundManager.player = player;
            roundManager.cpu = cpu;
            roundManager.hud = hud;
            roundManager.resultPanel = resultController;

            pauseController.gameManager = gameManager;
            pauseController.roundManager = roundManager;
            pauseController.pausePanel = pausePanel;
            pauseController.controls = data.controls;

            UnityEventTools.AddPersistentListener(pauseButton.onClick, pauseController.TogglePause);
            UnityEventTools.AddPersistentListener(continueButton.onClick, pauseController.Resume);
            UnityEventTools.AddPersistentListener(pauseRetryButton.onClick, pauseController.Retry);
            UnityEventTools.AddPersistentListener(pauseMenuButton.onClick, pauseController.ReturnToMenu);
            UnityEventTools.AddPersistentListener(retryButton.onClick, resultController.Retry);
            UnityEventTools.AddPersistentListener(resultMenuButton.onClick, resultController.ReturnToMenu);

            pausePanel.SetActive(false);
            resultPanel.SetActive(false);

            EditorSceneManager.SaveScene(scene, FightScenePath);
        }

        private static FighterController InstantiateFighter(GameObject prefab, Transform parent, Vector2 position)
        {
            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
            instance.transform.position = position;
            return instance.GetComponent<FighterController>();
        }

        private static GameObject CreateHealthBar(string name, Transform parent, FighterController fighter, bool fillFromRight)
        {
            var frame = CreateImage(name, parent, new Color(0.04f, 0.04f, 0.06f, 0.95f));

            var fillArea = CreateRect("FillArea", frame.transform);
            Stretch(fillArea, 4f, 4f, 4f, 4f);

            var fill = CreateImage("Fill", fillArea, HealthColor);
            Stretch(fill.rectTransform);

            var nameText = CreateText("Name", frame.transform, "LUCHADOR", 26, TextColor, fillFromRight ? TextAnchor.UpperRight : TextAnchor.UpperLeft, FontStyle.Bold);
            var anchor = fillFromRight ? new Vector2(1f, 0f) : new Vector2(0f, 0f);
            Place(nameText.rectTransform, anchor, anchor, new Vector2(anchor.x, 1f), new Vector2(0f, -6f), new Vector2(500f, 40f));

            var bar = frame.gameObject.AddComponent<HealthBarUI>();
            bar.fighter = fighter;
            bar.fill = fill.rectTransform;
            bar.nameText = nameText;
            bar.fillFromRight = fillFromRight;

            return frame.gameObject;
        }

        // ---------------- Ayudantes ----------------

        private static GameObject CreateSprite(string name, Transform parent, Sprite sprite, Color color, Vector2 position, Vector2 size, int sortingOrder)
        {
            var spriteObject = new GameObject(name);
            spriteObject.transform.SetParent(parent, false);
            spriteObject.transform.position = position;
            spriteObject.transform.localScale = new Vector3(size.x, size.y, 1f);

            var renderer = spriteObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.color = color;
            renderer.sortingOrder = sortingOrder;

            return spriteObject;
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

        private static Slider CreateSlider(string name, Transform parent)
        {
            var resources = new DefaultControls.Resources
            {
                standard = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd"),
                background = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                knob = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd")
            };

            var sliderObject = DefaultControls.CreateSlider(resources);
            sliderObject.name = name;
            sliderObject.transform.SetParent(parent, false);

            var slider = sliderObject.GetComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = 0.7f;
            return slider;
        }

        private static GameObject CreateOverlay(string name, Transform parent)
        {
            var image = CreateImage(name, parent, new Color(0.03f, 0.03f, 0.05f, 0.92f));
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
