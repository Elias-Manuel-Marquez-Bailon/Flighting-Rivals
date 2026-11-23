using FightingRivals.Characters;
using FightingRivals.Data;

namespace FightingRivals.Combat
{
    // ÚNICO lugar donde se decide qué pasa cuando un golpe conecta:
    // ¿lo bloqueó? ¿cuánto daño recibe? Ningún otro script aplica daño.
    public static class CombatResolver
    {
        public static void ResolveHit(FighterController attacker, FighterController defender, AttackData attack)
        {
            if (attacker == null || defender == null || attack == null)
            {
                return;
            }

            if (defender.State == FighterState.KO)
            {
                return;
            }

            var attackerX = attacker.Body.position.x;
            var pushDirection = defender.Body.position.x >= attackerX ? 1 : -1;

            // Bloqueo frontal: anula todo el daño (sin chip damage).
            if (defender.IsBlockingAgainst(attackerX))
            {
                defender.ReceiveBlock(attack, pushDirection);
                return;
            }

            defender.ReceiveHit(attack, pushDirection);
            defender.Health.TakeDamage(attack.damage);
        }
    }
}
