using UnityEngine;

namespace ZenoJam.Common
{
    public class BasePlayerData 
    {
        public readonly CustomAnimator Animator;
        public readonly Collider2D Collider;
        public readonly Rigidbody2D Rigidbody;
        public readonly UnitConfig Config;

        public BasePlayerData(CustomAnimator animator, Collider2D collider, Rigidbody2D rigidbody, UnitConfig config) 
        {
            Animator = animator;
            Collider = collider;
            Rigidbody = rigidbody;
            Config = config;
        }
    }
}
