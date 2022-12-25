using UnityEngine;
using UnityEngine.InputSystem;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    public class NeutralRoleActions : IPlayerRoleActions
    {
        private Collider2D _collider;

        private CheckInteractableAction _checkInteractableAction;

        public Vector2 MovementDirection { get; private set; }

        public bool CanMove { get; private set; }

        public NeutralRoleActions(Collider2D collider) 
        {
            _collider = collider;

            _checkInteractableAction = new CheckInteractableAction();
        }

        public void Enable() { }

        public void Disable() { }

        public void Dispose() { }

        public void LMB(InputAction.CallbackContext context) => _checkInteractableAction.Check(_collider);

        public void RMB(InputAction.CallbackContext context) => _checkInteractableAction.Check(_collider);

        public void Space(InputAction.CallbackContext context) => _checkInteractableAction.Check(_collider);

        public void StartMovement(InputAction.CallbackContext context)
        {
            MovementDirection = context.ReadValue<Vector2>();
            MovementDirection = new Vector2(MovementDirection.x, 0);
        }

        public void EndMovement(InputAction.CallbackContext context) 
        {
            MovementDirection = Vector2.zero;
        }

        public void ApplyDamage(int damage, Transform from, out bool isDead) { isDead = false; }
    }
}
