using FightingRivals.Characters;
using FightingRivals.Data;
using FightingRivals.UI;
using UnityEngine;

namespace FightingRivals.Core
{
    // Controla el round: cuenta regresiva, detecta K.O. o fin del tiempo
    // y muestra el resultado. Va en el objeto GameSystems de FightScene.
    //
    // El round pasa por estas fases:
    //   Intro    -> se muestra "¡PELEA!" y nadie se puede mover.
    //   Fighting -> corre el tiempo y se pelea.
    //   Ending   -> se muestra "K.O." o "¡TIEMPO!" unos segundos.
    //   Finished -> se muestra la pantalla de resultado.
    public class RoundManager : MonoBehaviour
    {
        private enum RoundPhase
        {
            Intro,
            Fighting,
            Ending,
            Finished
        }

        private enum RoundResult
        {
            PlayerWins,
            CpuWins,
            Draw
        }

        public RoundSettings settings;
        public FighterController player;
        public FighterController cpu;
        public CombatHUD hud;
        public ResultPanelController resultPanel;

        public bool IsFighting => phase == RoundPhase.Fighting;
        public bool IsRoundOver => phase == RoundPhase.Ending || phase == RoundPhase.Finished;
        public float TimeLeft { get; private set; }

        private RoundPhase phase = RoundPhase.Intro;
        private RoundResult result;
        private float phaseTimer;
        private bool knockoutPending;

        private void Start()
        {
            if (settings == null || player == null || cpu == null)
            {
                Debug.LogError("RoundManager: faltan referencias (settings, player o cpu).", this);
                enabled = false;
                return;
            }

            player.Health.Died += OnFighterDied;
            cpu.Health.Died += OnFighterDied;

            TimeLeft = settings.roundTime;
            UpdateTimerDisplay();
            StartIntro();
        }

        private void OnDestroy()
        {
            if (player != null && player.Health != null)
            {
                player.Health.Died -= OnFighterDied;
            }

            if (cpu != null && cpu.Health != null)
            {
                cpu.Health.Died -= OnFighterDied;
            }
        }

        private void Update()
        {
            switch (phase)
            {
                case RoundPhase.Intro:
                    phaseTimer -= Time.deltaTime;
                    if (phaseTimer <= 0f)
                    {
                        StartFighting();
                    }
                    break;

                case RoundPhase.Fighting:
                    TimeLeft = Mathf.Max(0f, TimeLeft - Time.deltaTime);
                    UpdateTimerDisplay();
                    if (TimeLeft <= 0f)
                    {
                        EndRound(true);
                    }
                    break;

                case RoundPhase.Ending:
                    phaseTimer -= Time.deltaTime;
                    if (phaseTimer <= 0f)
                    {
                        ShowResult();
                    }
                    break;
            }
        }

        // Se revisa en LateUpdate para detectar un doble K.O. ocurrido en el mismo frame.
        private void LateUpdate()
        {
            if (knockoutPending && phase == RoundPhase.Fighting)
            {
                EndRound(false);
            }
        }

        private void OnFighterDied()
        {
            knockoutPending = true;
        }

        private void StartIntro()
        {
            phase = RoundPhase.Intro;
            phaseTimer = settings.introDuration;
            SetFightersControl(false);
            ShowMessage("¡PELEA!");
        }

        private void StartFighting()
        {
            phase = RoundPhase.Fighting;
            HideMessage();
            SetFightersControl(true);
        }

        private void EndRound(bool timeUp)
        {
            phase = RoundPhase.Ending;
            phaseTimer = settings.resultDelay;
            SetFightersControl(false);

            result = GetResult();

            if (timeUp)
            {
                ShowMessage("¡TIEMPO!");
            }
            else
            {
                ShowMessage(result == RoundResult.Draw ? "DOBLE K.O." : "K.O.");
            }
        }

        // Gana quien tenga más vida. Con la misma vida es empate.
        private RoundResult GetResult()
        {
            var playerHealth = player.Health.CurrentHealth;
            var cpuHealth = cpu.Health.CurrentHealth;

            if (playerHealth > cpuHealth)
            {
                return RoundResult.PlayerWins;
            }

            if (cpuHealth > playerHealth)
            {
                return RoundResult.CpuWins;
            }

            return RoundResult.Draw;
        }

        private void ShowResult()
        {
            phase = RoundPhase.Finished;
            HideMessage();

            if (resultPanel == null)
            {
                return;
            }

            switch (result)
            {
                case RoundResult.PlayerWins:
                    resultPanel.ShowVictory();
                    break;
                case RoundResult.CpuWins:
                    resultPanel.ShowDefeat();
                    break;
                default:
                    resultPanel.ShowDraw();
                    break;
            }
        }

        private void SetFightersControl(bool enabled)
        {
            player.SetControlEnabled(enabled);
            cpu.SetControlEnabled(enabled);
        }

        private void UpdateTimerDisplay()
        {
            if (hud != null)
            {
                hud.SetTimer(TimeLeft);
            }
        }

        private void ShowMessage(string message)
        {
            if (hud != null)
            {
                hud.ShowMessage(message);
            }
        }

        private void HideMessage()
        {
            if (hud != null)
            {
                hud.HideMessage();
            }
        }
    }
}
