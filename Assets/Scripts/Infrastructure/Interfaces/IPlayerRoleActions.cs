using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace ZenoJam.Infrastructure.Interfaces
{
    public interface IPlayerRoleActions : IDisposable
    {
        public void Enable();

        public void Disable();

        public void StartMovement(CallbackContext context);

        public void EndMovement(CallbackContext context);

        public void Space(CallbackContext context);

        public void LMB(CallbackContext context);

        public void RMB(CallbackContext context);

        public void ApplyDamage(int damage, Transform from, out bool isDead);

        public Vector2 MovementDirection { get; }

        public bool CanMove { get; }
    }
}
