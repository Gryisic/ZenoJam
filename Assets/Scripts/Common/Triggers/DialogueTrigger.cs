using System;
using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DialogueTrigger : Trigger, IDialogueTrigger, IInteractable
    {
        public event Action<DialogueProvider> DialogueInitiated;

        [SerializeField] private DialogueProvider _provider;

        private void Awake()
        {
            Initialize();
        }

        public void Interact()
        {
            if (_provider.ContainsListenableMonologues(MonologueType.Additional))
            {
                _provider.SetTransform(transform);

                DialogueInitiated?.Invoke(_provider);
            }
        }

        protected override void Execute() 
        {
            _provider.SetListened();
            _provider.SetTransform(transform);

            DialogueInitiated?.Invoke(_provider);
        }

        private void Initialize()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_provider.ContainsListenableMonologues(MonologueType.Main)
                && collision.gameObject.TryGetComponent(out Player player))
            {
                Execute();
            }
        }
    }
}