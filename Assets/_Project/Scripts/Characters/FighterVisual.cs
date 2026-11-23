using System.Collections.Generic;
using FightingRivals.Data;
using UnityEngine;

namespace FightingRivals.Characters
{
    // Parte visual del luchador. Va en el objeto hijo "Visual" (el que tiene el SpriteRenderer).
    //
    // PARA EL EQUIPO DE ARTE:
    //   - Cambia el sprite del SpriteRenderer del objeto "Visual" en el prefab del luchador.
    //   - Dibuja los sprites mirando a la DERECHA; este script los voltea solo.
    //   - Si agregas un Animator con un Animator Controller, este script le envía estos parámetros
    //     (créalos con estos nombres exactos; los que no existan se ignoran):
    //       State (Int)       0 Idle, 1 Moving, 2 Jumping, 3 Attacking, 4 Blocking, 5 Hitstun, 6 KO
    //       SpeedX (Float)    velocidad horizontal absoluta
    //       Grounded (Bool)   true si está en el suelo
    //       AttackType (Int)  1 ligero, 2 pesado (se fija al empezar el ataque)
    //       Attack (Trigger)  se dispara al empezar un ataque
    //       Hit (Trigger)     se dispara al recibir un golpe
    //   - Cuando haya animaciones reales, desmarca "Use Placeholder Effects" para quitar
    //     los colores y la rotación provisionales.
    [RequireComponent(typeof(SpriteRenderer))]
    public class FighterVisual : MonoBehaviour
    {
        [Tooltip("Colores por estado, destello al recibir golpe y caída en K.O. (provisional).")]
        public bool usePlaceholderEffects = true;
        public Animator animator;

        private static readonly int StateParam = Animator.StringToHash("State");
        private static readonly int SpeedXParam = Animator.StringToHash("SpeedX");
        private static readonly int GroundedParam = Animator.StringToHash("Grounded");
        private static readonly int AttackTypeParam = Animator.StringToHash("AttackType");
        private static readonly int AttackParam = Animator.StringToHash("Attack");
        private static readonly int HitParam = Animator.StringToHash("Hit");

        private readonly HashSet<int> animatorParameters = new HashSet<int>();
        private FighterController fighter;
        private FighterAttack fighterAttack;
        private SpriteRenderer spriteRenderer;
        private Color baseColor;
        private Vector3 baseLocalPosition;
        private float flashTimer;

        private void Awake()
        {
            fighter = GetComponentInParent<FighterController>();
            fighterAttack = GetComponentInParent<FighterAttack>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            baseColor = spriteRenderer.color;
            baseLocalPosition = transform.localPosition;

            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (animator != null && animator.runtimeAnimatorController != null)
            {
                foreach (var parameter in animator.parameters)
                {
                    animatorParameters.Add(parameter.nameHash);
                }
            }
        }

        private void OnEnable()
        {
            if (fighter == null)
            {
                return;
            }

            fighter.HitReceived += OnHitReceived;
            if (fighterAttack != null)
            {
                fighterAttack.AttackStarted += OnAttackStarted;
            }
        }

        private void OnDisable()
        {
            if (fighter == null)
            {
                return;
            }

            fighter.HitReceived -= OnHitReceived;
            if (fighterAttack != null)
            {
                fighterAttack.AttackStarted -= OnAttackStarted;
            }
        }

        private void LateUpdate()
        {
            if (fighter == null)
            {
                return;
            }

            spriteRenderer.flipX = fighter.FacingDirection < 0;
            UpdateAnimator();

            if (usePlaceholderEffects)
            {
                UpdatePlaceholderEffects();
            }
        }

        private void OnAttackStarted(AttackData attack)
        {
            var isHeavy = fighter.stats != null && attack == fighter.stats.heavyAttack;
            SetAnimatorInt(AttackTypeParam, isHeavy ? 2 : 1);
            SetAnimatorTrigger(AttackParam);
        }

        private void OnHitReceived(AttackData attack)
        {
            flashTimer = 0.08f;
            SetAnimatorTrigger(HitParam);
        }

        private void UpdateAnimator()
        {
            SetAnimatorInt(StateParam, (int)fighter.State);
            SetAnimatorBool(GroundedParam, fighter.IsGrounded);

            if (animatorParameters.Contains(SpeedXParam))
            {
                animator.SetFloat(SpeedXParam, Mathf.Abs(fighter.Body.linearVelocity.x));
            }
        }

        // Efectos provisionales para poder leer el combate sin animaciones.
        private void UpdatePlaceholderEffects()
        {
            var color = baseColor;

            switch (fighter.State)
            {
                case FighterState.Attacking:
                    color = Color.Lerp(baseColor, Color.white, 0.35f);
                    break;
                case FighterState.Blocking:
                    color = Color.Lerp(baseColor, new Color(0.3f, 0.8f, 1f), 0.6f);
                    break;
                case FighterState.Hitstun:
                    color = Color.Lerp(baseColor, Color.black, 0.35f);
                    break;
                case FighterState.KO:
                    color = Color.Lerp(baseColor, Color.gray, 0.7f);
                    break;
            }

            if (flashTimer > 0f)
            {
                flashTimer -= Time.deltaTime;
                color = Color.white;
            }

            spriteRenderer.color = color;

            if (fighter.State == FighterState.KO)
            {
                // "Acostado" en el suelo.
                transform.localRotation = Quaternion.Euler(0f, 0f, 90f * -fighter.FacingDirection);
                transform.localPosition = baseLocalPosition + new Vector3(0f, -0.5f, 0f);
            }
            else
            {
                transform.localRotation = Quaternion.identity;
                transform.localPosition = baseLocalPosition;
            }
        }

        private void SetAnimatorInt(int parameter, int value)
        {
            if (animatorParameters.Contains(parameter))
            {
                animator.SetInteger(parameter, value);
            }
        }

        private void SetAnimatorBool(int parameter, bool value)
        {
            if (animatorParameters.Contains(parameter))
            {
                animator.SetBool(parameter, value);
            }
        }

        private void SetAnimatorTrigger(int parameter)
        {
            if (animatorParameters.Contains(parameter))
            {
                animator.SetTrigger(parameter);
            }
        }
    }
}
