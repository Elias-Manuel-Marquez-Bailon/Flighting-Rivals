using System;
using FightingRivals.Combat;
using FightingRivals.Data;
using UnityEngine;

namespace FightingRivals.Characters
{
    // Controla el tiempo de un ataque en tres fases:
    //   1. Inicio (startup): el luchador se prepara, todavía no golpea.
    //   2. Activo (active): la hitbox está encendida y puede conectar.
    //   3. Recuperación (recovery): el luchador no puede actuar.
    public class FighterAttack : MonoBehaviour
    {
        public enum AttackPhase
        {
            None,
            Startup,
            Active,
            Recovery
        }

        [Tooltip("Hitbox hija del luchador. Si está vacía se busca automáticamente.")]
        public AttackHitbox hitbox;

        public event Action<AttackData> AttackStarted;

        public AttackData CurrentAttack { get; private set; }
        public AttackPhase Phase { get; private set; } = AttackPhase.None;
        public bool IsAttacking => CurrentAttack != null;

        private FighterController fighter;
        private float timer;

        private void Awake()
        {
            fighter = GetComponent<FighterController>();

            if (hitbox == null)
            {
                hitbox = GetComponentInChildren<AttackHitbox>(true);
            }
        }

        public void StartAttack(AttackData attack)
        {
            CurrentAttack = attack;
            timer = 0f;
            Phase = AttackPhase.Startup;
            AttackStarted?.Invoke(attack);
        }

        public void CancelAttack()
        {
            if (hitbox != null)
            {
                hitbox.Deactivate();
            }

            CurrentAttack = null;
            Phase = AttackPhase.None;
        }

        private void Update()
        {
            if (!IsAttacking)
            {
                return;
            }

            timer += Time.deltaTime;
            var attack = CurrentAttack;

            if (Phase == AttackPhase.Startup && timer >= attack.startup)
            {
                Phase = AttackPhase.Active;
                if (hitbox != null)
                {
                    hitbox.Activate(attack, fighter);
                }
            }

            if (Phase == AttackPhase.Active && timer >= attack.startup + attack.active)
            {
                Phase = AttackPhase.Recovery;
                if (hitbox != null)
                {
                    hitbox.Deactivate();
                }
            }

            if (Phase == AttackPhase.Recovery && timer >= attack.TotalDuration)
            {
                CurrentAttack = null;
                Phase = AttackPhase.None;
                fighter.OnAttackFinished();
            }
        }
    }
}
