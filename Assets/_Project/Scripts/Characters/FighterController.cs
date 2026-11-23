using System;
using FightingRivals.Arena;
using FightingRivals.Combat;
using FightingRivals.Data;
using UnityEngine;

namespace FightingRivals.Characters
{
    // Cerebro "físico" del luchador: moverse, saltar, girar, bloquear y reaccionar a golpes.
    // Lo usan tanto el jugador como la CPU. Quien da las órdenes es otro script:
    //   - PlayerInputHandler (teclado / control) en el jugador.
    //   - CPUController en el rival.
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Health))]
    [RequireComponent(typeof(FighterAttack))]
    public class FighterController : MonoBehaviour
    {
        private const float GroundFriction = 20f;

        [Header("Datos")]
        public FighterStats stats;

        [Header("Referencias de escena (si están vacías se buscan solas)")]
        public FighterController opponent;
        public ArenaBounds arena;

        // Eventos que otros scripts escuchan (sonido, visual, etc.).
        public event Action Jumped;
        public event Action<AttackData> HitReceived;
        public event Action<AttackData> HitBlocked;
        public event Action KnockedOut;

        public FighterState State { get; private set; } = FighterState.Idle;
        public int FacingDirection { get; private set; } = 1; // 1 = derecha, -1 = izquierda.
        public bool ControlEnabled { get; private set; } = true;
        public bool IsGrounded { get; private set; }
        public Health Health { get; private set; }
        public FighterAttack Attack { get; private set; }
        public Rigidbody2D Body => body;

        private Rigidbody2D body;
        private BoxCollider2D bodyCollider;
        private float moveInput;
        private bool blockHeld;
        private float stunTimer;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            bodyCollider = GetComponent<BoxCollider2D>();
            Health = GetComponent<Health>();
            Attack = GetComponent<FighterAttack>();

            body.freezeRotation = true;
            // Sin fricción para que el luchador no se quede "pegado" al suelo o a otros objetos.
            bodyCollider.sharedMaterial = new PhysicsMaterial2D("SinFriccion") { friction = 0f, bounciness = 0f };

            if (stats != null)
            {
                body.gravityScale = stats.gravityScale;
                Health.Initialize(stats.maxHealth);
            }
            else
            {
                Debug.LogError(name + ": falta asignar FighterStats en el Inspector.", this);
                Health.Initialize(1000);
            }

            Health.Died += EnterKO;
        }

        private void Start()
        {
            if (arena == null)
            {
                arena = FindAnyObjectByType<ArenaBounds>();
            }

            if (opponent == null)
            {
                FindOpponent();
            }

            if (opponent != null)
            {
                // Los luchadores no chocan físicamente entre sí; KeepApartFromOpponent los separa.
                // Así nadie se queda parado encima de la cabeza del otro.
                Physics2D.IgnoreCollision(bodyCollider, opponent.bodyCollider);
            }

            FaceOpponent();
        }

        private void OnDestroy()
        {
            if (Health != null)
            {
                Health.Died -= EnterKO;
            }
        }

        // ---------- Órdenes (las llaman PlayerInputHandler o CPUController) ----------

        public void SetMoveInput(float direction)
        {
            moveInput = Mathf.Clamp(direction, -1f, 1f);
        }

        public void SetBlockHeld(bool held)
        {
            blockHeld = held;
        }

        public void Jump()
        {
            if (!CanAct() || !IsGrounded || State == FighterState.Blocking || stats == null)
            {
                return;
            }

            var velocity = body.linearVelocity;
            velocity.x = moveInput * GetMoveSpeed();
            velocity.y = stats.jumpVelocity;
            body.linearVelocity = velocity;

            IsGrounded = false;
            State = FighterState.Jumping;
            Jumped?.Invoke();
        }

        public void LightAttack()
        {
            TryAttack(stats != null ? stats.lightAttack : null);
        }

        public void HeavyAttack()
        {
            TryAttack(stats != null ? stats.heavyAttack : null);
        }

        // RoundManager lo usa para congelar a los luchadores al inicio y al final del round.
        public void SetControlEnabled(bool enabled)
        {
            ControlEnabled = enabled;

            if (!enabled)
            {
                moveInput = 0f;
                blockHeld = false;
            }
        }

        public bool CanAct()
        {
            if (!ControlEnabled)
            {
                return false;
            }

            switch (State)
            {
                case FighterState.Idle:
                case FighterState.Moving:
                case FighterState.Jumping:
                    return true;
                case FighterState.Blocking:
                    return stunTimer <= 0f;
                default:
                    return false;
            }
        }

        // ---------- Recibir golpes (lo llama CombatResolver) ----------

        // ¿Está bloqueando hacia el lado de donde viene el atacante? Los golpes por la espalda no se bloquean.
        public bool IsBlockingAgainst(float attackerX)
        {
            if (State != FighterState.Blocking || !IsGrounded)
            {
                return false;
            }

            var side = attackerX - body.position.x;
            return Mathf.Approximately(side, 0f) || (int)Mathf.Sign(side) == FacingDirection;
        }

        public void ReceiveHit(AttackData attack, int pushDirection)
        {
            if (State == FighterState.KO)
            {
                return;
            }

            Attack.CancelAttack();
            stunTimer = attack.hitstun;
            State = FighterState.Hitstun;

            var velocity = body.linearVelocity;
            velocity.x = pushDirection * attack.knockback;
            body.linearVelocity = velocity;

            HitReceived?.Invoke(attack);
        }

        public void ReceiveBlock(AttackData attack, int pushDirection)
        {
            stunTimer = attack.blockstun;

            var velocity = body.linearVelocity;
            velocity.x = pushDirection * attack.knockback * 0.5f;
            body.linearVelocity = velocity;

            HitBlocked?.Invoke(attack);
        }

        // FighterAttack avisa cuando el ataque terminó su recuperación.
        public void OnAttackFinished()
        {
            if (State == FighterState.Attacking)
            {
                UpdateNeutralState();
            }
        }

        // ---------- Ciclo de Unity ----------

        private void Update()
        {
            if (State == FighterState.KO)
            {
                return;
            }

            if (stunTimer > 0f)
            {
                stunTimer -= Time.deltaTime;

                if (stunTimer <= 0f && State == FighterState.Hitstun)
                {
                    stunTimer = 0f;
                    UpdateNeutralState();
                }
            }

            if (State != FighterState.Hitstun && State != FighterState.Attacking)
            {
                UpdateNeutralState();
            }

            if (CanTurn())
            {
                FaceOpponent();
            }
        }

        private void FixedUpdate()
        {
            IsGrounded = CheckGrounded();

            var velocity = body.linearVelocity;
            var slowingDown = Mathf.MoveTowards(velocity.x, 0f, GroundFriction * Time.fixedDeltaTime);

            switch (State)
            {
                case FighterState.Idle:
                    velocity.x = 0f;
                    break;
                case FighterState.Moving:
                    velocity.x = moveInput * GetMoveSpeed();
                    break;
                case FighterState.Jumping:
                    // En el aire se conserva la velocidad del salto.
                    break;
                case FighterState.Attacking:
                    if (IsGrounded)
                    {
                        velocity.x = 0f;
                    }
                    break;
                case FighterState.Blocking:
                    // Durante el bloqueo solo se desliza por el empujón del golpe bloqueado.
                    velocity.x = stunTimer > 0f ? slowingDown : 0f;
                    break;
                case FighterState.Hitstun:
                case FighterState.KO:
                    velocity.x = slowingDown;
                    break;
            }

            body.linearVelocity = velocity;

            KeepApartFromOpponent();
            ClampInsideArena();
        }

        // ---------- Lógica interna ----------

        private void TryAttack(AttackData attack)
        {
            if (attack == null || !CanAct() || State == FighterState.Blocking)
            {
                return;
            }

            Attack.StartAttack(attack);
            State = FighterState.Attacking;
        }

        // Elige Idle / Moving / Blocking / Jumping según lo que se esté pulsando.
        private void UpdateNeutralState()
        {
            if (!IsGrounded)
            {
                State = FighterState.Jumping;
                return;
            }

            if (State == FighterState.Blocking && stunTimer > 0f)
            {
                return; // Sigue bloqueando hasta que pase el "blockstun".
            }

            if (blockHeld && ControlEnabled)
            {
                State = FighterState.Blocking;
            }
            else if (Mathf.Abs(moveInput) > 0.01f && ControlEnabled)
            {
                State = FighterState.Moving;
            }
            else
            {
                State = FighterState.Idle;
            }
        }

        private void EnterKO()
        {
            Attack.CancelAttack();
            moveInput = 0f;
            blockHeld = false;
            stunTimer = 0f;
            State = FighterState.KO;
            KnockedOut?.Invoke();
        }

        private float GetMoveSpeed()
        {
            if (stats == null)
            {
                return 0f;
            }

            var walkingBackward = moveInput != 0f && (int)Mathf.Sign(moveInput) != FacingDirection;
            return walkingBackward ? stats.moveSpeed * stats.backwardSpeedMultiplier : stats.moveSpeed;
        }

        private bool CheckGrounded()
        {
            if (arena == null || arena.floor == null)
            {
                return false;
            }

            return body.linearVelocity.y <= 0.01f && bodyCollider.IsTouching(arena.floor);
        }

        private bool CanTurn()
        {
            return IsGrounded &&
                   (State == FighterState.Idle || State == FighterState.Moving || State == FighterState.Blocking);
        }

        private void FaceOpponent()
        {
            if (opponent == null)
            {
                return;
            }

            var dx = opponent.transform.position.x - transform.position.x;
            if (Mathf.Abs(dx) > 0.05f)
            {
                FacingDirection = dx > 0f ? 1 : -1;
            }
        }

        // Evita que los dos luchadores queden uno dentro del otro.
        private void KeepApartFromOpponent()
        {
            if (opponent == null || opponent.body == null)
            {
                return;
            }

            var myPosition = body.position;
            var otherPosition = opponent.body.position;
            var minDistanceX = (bodyCollider.size.x + opponent.bodyCollider.size.x) * 0.5f;
            var minDistanceY = (bodyCollider.size.y + opponent.bodyCollider.size.y) * 0.5f;
            var dx = myPosition.x - otherPosition.x;

            if (Mathf.Abs(dx) >= minDistanceX || Mathf.Abs(myPosition.y - otherPosition.y) >= minDistanceY)
            {
                return;
            }

            float direction;
            if (!Mathf.Approximately(dx, 0f))
            {
                direction = Mathf.Sign(dx);
            }
            else
            {
                // Exactamente en el mismo punto: cada uno retrocede hacia su espalda.
                // Si miran al mismo lado, se desempata por nombre para que se separen.
                direction = opponent.FacingDirection != FacingDirection
                    ? -FacingDirection
                    : (string.CompareOrdinal(name, opponent.name) < 0 ? -1f : 1f);
            }

            // Cada luchador se mueve la mitad de lo necesario; entre los dos se separan por completo.
            var push = (minDistanceX - Mathf.Abs(dx)) * 0.5f;
            body.position = new Vector2(myPosition.x + direction * push, myPosition.y);
        }

        private void ClampInsideArena()
        {
            if (arena == null)
            {
                return;
            }

            var halfWidth = bodyCollider.size.x * 0.5f;
            var position = body.position;
            var clampedX = Mathf.Clamp(position.x, arena.LeftLimit + halfWidth, arena.RightLimit - halfWidth);

            if (!Mathf.Approximately(clampedX, position.x))
            {
                body.position = new Vector2(clampedX, position.y);

                var velocity = body.linearVelocity;
                velocity.x = 0f;
                body.linearVelocity = velocity;
            }
        }

        private void FindOpponent()
        {
            foreach (var fighter in FindObjectsByType<FighterController>(FindObjectsInactive.Exclude))
            {
                if (fighter != this)
                {
                    opponent = fighter;
                    return;
                }
            }
        }
    }
}
