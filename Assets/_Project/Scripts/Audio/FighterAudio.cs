using FightingRivals.Characters;
using FightingRivals.Data;
using UnityEngine;

namespace FightingRivals.Audio
{
    // Sonidos de UN luchador. Va en el objeto raíz del prefab del luchador.
    // Escucha lo que hace el luchador (saltar, atacar, recibir golpe...) y pide a AudioManager que suene.
    //
    // PARA EL EQUIPO DE AUDIO:
    //   - Los sonidos del ataque (al lanzarlo y al conectar) están en cada archivo de ataque
    //     (Assets/_Project/Data/Combat/LightAttack y HeavyAttack): campos "Swing Sound" y "Hit Sound".
    //   - Aquí van los sonidos propios del personaje.
    [RequireComponent(typeof(FighterController))]
    public class FighterAudio : MonoBehaviour
    {
        public AudioClip jumpClip;
        public AudioClip hurtClip;
        public AudioClip blockClip;
        public AudioClip knockoutClip;

        private FighterController fighter;

        private void Awake()
        {
            fighter = GetComponent<FighterController>();
        }

        private void OnEnable()
        {
            fighter.Jumped += OnJumped;
            fighter.HitReceived += OnHitReceived;
            fighter.HitBlocked += OnHitBlocked;
            fighter.KnockedOut += OnKnockedOut;
            fighter.GetComponent<FighterAttack>().AttackStarted += OnAttackStarted;
        }

        private void OnDisable()
        {
            fighter.Jumped -= OnJumped;
            fighter.HitReceived -= OnHitReceived;
            fighter.HitBlocked -= OnHitBlocked;
            fighter.KnockedOut -= OnKnockedOut;
            fighter.GetComponent<FighterAttack>().AttackStarted -= OnAttackStarted;
        }

        private void OnJumped()
        {
            Play(jumpClip);
        }

        private void OnAttackStarted(AttackData attack)
        {
            Play(attack.swingSound);
        }

        private void OnHitReceived(AttackData attack)
        {
            Play(attack.hitSound);
            Play(hurtClip);
        }

        private void OnHitBlocked(AttackData attack)
        {
            Play(blockClip);
        }

        private void OnKnockedOut()
        {
            Play(knockoutClip);
        }

        private static void Play(AudioClip clip)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySfx(clip);
            }
        }
    }
}
