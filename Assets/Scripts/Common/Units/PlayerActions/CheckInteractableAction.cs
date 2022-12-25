using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    public class CheckInteractableAction 
    {
        public void Check(Collider2D collider)
        {
            var colliders = Physics2D.OverlapCircleAll(collider.transform.position, collider.bounds.size.x / 2);

            foreach (var concreteCollider in colliders)
                if (concreteCollider.TryGetComponent(out IInteractable interactable))
                    interactable.Interact();
        }
    }
}
