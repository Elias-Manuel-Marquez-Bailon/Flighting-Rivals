using FightingRivals.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightingRivals.Controls
{
    // Lee los controles del jugador y le dice a FighterController qué hacer.
    // Va SOLO en el luchador del jugador.
    //
    // Las teclas se configuran en Assets/_Project/Settings/FightingRivalsControls.inputactions
    // (mapa "Fighter"). Teclado, control y, más adelante, botones táctiles alimentan las mismas acciones.
    [RequireComponent(typeof(FighterController))]
    public class PlayerInputHandler : MonoBehaviour
    {
        public InputActionAsset controls;

        private FighterController fighter;
        private InputActionAsset runtimeControls;
        private InputActionMap fighterMap;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction lightAttackAction;
        private InputAction heavyAttackAction;
        private InputAction blockAction;

        private void Awake()
        {
            fighter = GetComponent<FighterController>();

            if (controls == null)
            {
                Debug.LogError("PlayerInputHandler: falta asignar el archivo de controles (FightingRivalsControls).", this);
                enabled = false;
                return;
            }

            // Cada luchador usa su propia copia de los controles; así, al reiniciar la pelea,
            // el luchador anterior no desactiva por error los controles del nuevo.
            runtimeControls = Instantiate(controls);
            fighterMap = runtimeControls.FindActionMap("Fighter", true);
            moveAction = fighterMap.FindAction("Move", true);
            jumpAction = fighterMap.FindAction("Jump", true);
            lightAttackAction = fighterMap.FindAction("LightAttack", true);
            heavyAttackAction = fighterMap.FindAction("HeavyAttack", true);
            blockAction = fighterMap.FindAction("Block", true);
        }

        private void OnEnable()
        {
            fighterMap?.Enable();
        }

        private void OnDisable()
        {
            fighterMap?.Disable();
        }

        private void OnDestroy()
        {
            if (runtimeControls != null)
            {
                Destroy(runtimeControls);
            }
        }

        private void Update()
        {
            // En pausa no se envían órdenes.
            if (Time.timeScale == 0f)
            {
                return;
            }

            fighter.SetMoveInput(moveAction.ReadValue<float>());
            fighter.SetBlockHeld(blockAction.IsPressed());

            if (jumpAction.WasPressedThisFrame())
            {
                fighter.Jump();
            }

            if (lightAttackAction.WasPressedThisFrame())
            {
                fighter.LightAttack();
            }

            if (heavyAttackAction.WasPressedThisFrame())
            {
                fighter.HeavyAttack();
            }
        }
    }
}
