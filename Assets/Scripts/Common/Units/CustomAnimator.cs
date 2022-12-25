using System.Collections.Generic;
using UnityEngine;
using ZenoJam.Utils;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class CustomAnimator 
    {
        private Animator _animator;

        private Dictionary<AnimationType, int> _animations = new Dictionary<AnimationType, int>();

        public CustomAnimator(Animator animator) 
        {
            _animator = animator;

            Initialize();
        }

        public void StartMovement() => SetBoolState(AnimationType.Movement, true);

        public void EndMovement() => SetBoolState(AnimationType.Movement, false);

        public void Dash() => SetTriggerState(AnimationType.Dash);

        public void StartTakingDamage() => SetBoolState(AnimationType.TakeDamage, true);

        public void EndTakingDamage() => SetBoolState(AnimationType.TakeDamage, false);

        public void Death() => SetBoolState(AnimationType.Death, true);

        public void InAir(float yVelocity, bool value) 
        {
            SetBoolState(AnimationType.Jump, value);
            SetFloatState(yVelocity);
        }

        private void SetTriggerState(AnimationType type) => _animator.SetTrigger(_animations[type]);

        private void SetBoolState(AnimationType type, bool state) => _animator.SetBool(_animations[type], state);

        private void SetFloatState(float value) => _animator.SetFloat(Constants.IN_AIR_FLOAT_VALUE, value);

        private void Initialize() 
        {
            foreach (AnimationType animationType in System.Enum.GetValues(typeof(AnimationType)))
                _animations.Add(animationType, Animator.StringToHash(animationType.ToString()));
        }
    }
}
