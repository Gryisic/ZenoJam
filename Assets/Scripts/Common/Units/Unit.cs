using System;
using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Unit : MonoBehaviour, IMovable, IDisposable
    {
        [SerializeField] protected UnitConfig config;

        protected SpriteRenderer sprite;
        protected Rigidbody2D m_rigidbody;
        protected Collider2D m_collider;
        protected CustomAnimator animator;

        private void Awake()
        {
            animator = new CustomAnimator(GetComponent<Animator>());

            sprite = GetComponent<SpriteRenderer>();
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<Collider2D>();

            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        protected abstract void Initialize();

        public abstract void Dispose();

        public abstract void Move(Vector2 destination);
    }
}
