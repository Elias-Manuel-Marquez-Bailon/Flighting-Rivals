using System.Collections.Generic;
using FightingRivals.Characters;
using FightingRivals.Data;
using UnityEngine;

namespace FightingRivals.Combat
{
    // Zona de golpe de un ataque. Va en un objeto hijo "Hitbox" del luchador.
    // Solo está encendida durante la fase activa del ataque y golpea a cada rival UNA sola vez por ataque.
    public class AttackHitbox : MonoBehaviour
    {
        [Tooltip("Muestra un rectángulo amarillo mientras la hitbox está activa (útil para probar).")]
        public bool showDebugHitbox = true;
        public SpriteRenderer debugRenderer;

        public bool IsActive => attack != null;

        private readonly List<FighterController> alreadyHit = new List<FighterController>();
        private readonly List<Collider2D> overlaps = new List<Collider2D>();
        private ContactFilter2D filter;
        private FighterController owner;
        private AttackData attack;

        private void Awake()
        {
            if (debugRenderer == null)
            {
                debugRenderer = GetComponent<SpriteRenderer>();
            }

            filter = ContactFilter2D.noFilter;
            SetDebugVisible(false);
        }

        public void Activate(AttackData attackData, FighterController attacker)
        {
            attack = attackData;
            owner = attacker;
            alreadyHit.Clear();

            UpdatePlacement();
            SetDebugVisible(showDebugHitbox);
            CheckHits();
        }

        public void Deactivate()
        {
            attack = null;
            SetDebugVisible(false);
        }

        private void FixedUpdate()
        {
            if (!IsActive)
            {
                return;
            }

            UpdatePlacement();
            CheckHits();
        }

        private Vector2 GetWorldCenter()
        {
            var offset = new Vector2(attack.hitboxOffset.x * owner.FacingDirection, attack.hitboxOffset.y);
            return owner.Body.position + offset;
        }

        // Coloca el objeto (y su rectángulo de prueba) delante del luchador.
        private void UpdatePlacement()
        {
            transform.localPosition = new Vector3(attack.hitboxOffset.x * owner.FacingDirection, attack.hitboxOffset.y, 0f);
            transform.localScale = new Vector3(attack.hitboxSize.x, attack.hitboxSize.y, 1f);
        }

        private void CheckHits()
        {
            Physics2D.OverlapBox(GetWorldCenter(), attack.hitboxSize, 0f, filter, overlaps);

            foreach (var hitCollider in overlaps)
            {
                if (!IsActive)
                {
                    return;
                }

                var target = hitCollider.GetComponentInParent<FighterController>();
                if (target == null || target == owner || alreadyHit.Contains(target))
                {
                    continue;
                }

                alreadyHit.Add(target);
                CombatResolver.ResolveHit(owner, target, attack);
            }
        }

        private void SetDebugVisible(bool visible)
        {
            if (debugRenderer != null)
            {
                debugRenderer.enabled = visible;
            }
        }

        private void OnDrawGizmos()
        {
            if (!IsActive || owner == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetWorldCenter(), attack.hitboxSize);
        }
    }
}
