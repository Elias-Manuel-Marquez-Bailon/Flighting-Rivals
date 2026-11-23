using FightingRivals.Characters;
using FightingRivals.Data;
using UnityEngine;

namespace FightingRivals.AI
{
    // "Cerebro" del rival controlado por la computadora. Va SOLO en el luchador CPU.
    // Hace lo mismo que PlayerInputHandler (dar órdenes a FighterController), pero decidiendo solo:
    //   - Si el jugador está lejos, se acerca.
    //   - Si está cerca, ataca (ligero o pesado) con pausas entre ataques.
    //   - A veces bloquea cuando el jugador ataca.
    //   - A veces retrocede si está demasiado pegado, sin salirse de la arena.
    // Los valores se ajustan en Assets/_Project/Data/CPUSettings.
    [RequireComponent(typeof(FighterController))]
    public class CPUController : MonoBehaviour
    {
        private enum Intent
        {
            Wait,
            Approach,
            Retreat
        }

        private const float WallMargin = 1.2f;

        public CPUSettings settings;
        [Tooltip("Luchador al que ataca. Si está vacío se usa el oponente del FighterController.")]
        public FighterController target;

        private FighterController fighter;
        private FighterAttack targetAttack;
        private Intent intent = Intent.Wait;
        private float decisionTimer;
        private float attackCooldown;
        private float blockTimer;

        private void Awake()
        {
            fighter = GetComponent<FighterController>();
        }

        private void Start()
        {
            if (target == null)
            {
                target = fighter.opponent;
            }

            if (target != null)
            {
                targetAttack = target.GetComponent<FighterAttack>();
                targetAttack.AttackStarted += OnTargetAttackStarted;
            }

            if (settings == null)
            {
                Debug.LogError("CPUController: falta asignar CPUSettings.", this);
                enabled = false;
                return;
            }

            attackCooldown = Random.Range(settings.minAttackCooldown, settings.maxAttackCooldown);
        }

        private void OnDestroy()
        {
            if (targetAttack != null)
            {
                targetAttack.AttackStarted -= OnTargetAttackStarted;
            }
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            if (!fighter.ControlEnabled || fighter.State == FighterState.KO)
            {
                fighter.SetMoveInput(0f);
                fighter.SetBlockHeld(false);
                return;
            }

            attackCooldown -= Time.deltaTime;
            decisionTimer -= Time.deltaTime;

            // Mientras bloquea, no hace nada más.
            if (blockTimer > 0f)
            {
                blockTimer -= Time.deltaTime;
                fighter.SetMoveInput(0f);
                fighter.SetBlockHeld(true);
                return;
            }

            fighter.SetBlockHeld(false);

            if (decisionTimer <= 0f)
            {
                Decide();
                decisionTimer = Random.Range(settings.minDecisionTime, settings.maxDecisionTime);
            }

            Act();
        }

        // Reacción al ataque del jugador: tirar "un dado" para ver si bloquea.
        private void OnTargetAttackStarted(AttackData attack)
        {
            if (settings == null || !fighter.ControlEnabled || !fighter.IsGrounded)
            {
                return;
            }

            if (fighter.State == FighterState.Attacking || fighter.State == FighterState.Hitstun)
            {
                return;
            }

            var closeEnough = DistanceToTarget() <= settings.attackRange + 0.6f;
            if (closeEnough && Random.value < settings.blockChance)
            {
                blockTimer = settings.blockHoldTime;
            }
        }

        private void Decide()
        {
            var distance = DistanceToTarget();

            if (distance > settings.attackRange)
            {
                intent = Intent.Approach;
                return;
            }

            if (distance < settings.tooCloseDistance && !IsWallBehind() && Random.value < settings.retreatChance)
            {
                intent = Intent.Retreat;
                return;
            }

            if (attackCooldown <= 0f)
            {
                if (Random.value < settings.heavyAttackChance)
                {
                    fighter.HeavyAttack();
                }
                else
                {
                    fighter.LightAttack();
                }

                attackCooldown = Random.Range(settings.minAttackCooldown, settings.maxAttackCooldown);
            }

            intent = Intent.Wait;
        }

        private void Act()
        {
            var distance = DistanceToTarget();
            var towardTarget = DirectionToTarget();

            switch (intent)
            {
                case Intent.Approach:
                    // Se detiene un poco antes del alcance máximo para no "vibrar" entre acercarse y parar.
                    if (distance <= settings.attackRange * 0.85f)
                    {
                        intent = Intent.Wait;
                        decisionTimer = 0f;
                        fighter.SetMoveInput(0f);
                    }
                    else
                    {
                        fighter.SetMoveInput(towardTarget);
                    }
                    break;

                case Intent.Retreat:
                    if (distance >= settings.attackRange || IsWallBehind())
                    {
                        intent = Intent.Wait;
                        fighter.SetMoveInput(0f);
                    }
                    else
                    {
                        fighter.SetMoveInput(-towardTarget);
                    }
                    break;

                default:
                    fighter.SetMoveInput(0f);
                    break;
            }
        }

        private float DistanceToTarget()
        {
            return Mathf.Abs(target.transform.position.x - transform.position.x);
        }

        private float DirectionToTarget()
        {
            return target.transform.position.x >= transform.position.x ? 1f : -1f;
        }

        private bool IsWallBehind()
        {
            if (fighter.arena == null)
            {
                return false;
            }

            var x = transform.position.x;
            return DirectionToTarget() > 0f
                ? x - fighter.arena.LeftLimit < WallMargin
                : fighter.arena.RightLimit - x < WallMargin;
        }
    }
}
