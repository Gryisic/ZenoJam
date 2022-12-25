using UnityEngine;

namespace ZenoJam.Utils
{
    public static class Vector2Extension 
    {
        public static Vector2 DamageImpact(this Vector2 vector, Rigidbody2D rigidbody, Transform from) 
        {
            Vector2 difference = rigidbody.position - (Vector2)from.position;
            Vector2 force = new Vector2(Mathf.Abs(difference.x), Mathf.Abs(difference.y));
            Vector2 direction = difference.normalized;
            Vector2 impactDirection = force * direction;

            return new Vector2(impactDirection.x + Constants.MINIMAL_TAKING_DAMAGE_IMPACT_FORCE * direction.x,
                                              impactDirection.y + Constants.MINIMAL_TAKING_DAMAGE_IMPACT_FORCE);
        }
    }
}