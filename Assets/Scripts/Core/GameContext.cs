using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using ZenoJam.Common;

namespace ZenoJam.Core
{
    public class GameContext : MonoBehaviour, IDisposable
    {
        [SerializeField] private SceneTransition _sceneTransition;
        [SerializeField] private AudioMixer _mixer;

        private SceneSwitcher _sceneSwitcher;

        private Dictionary<Type, object> _registeredTypes;

        public void Construct() 
        {
            _registeredTypes = new Dictionary<Type, object>();
            _sceneSwitcher = new SceneSwitcher(_sceneTransition);

            RegisterInstance(_sceneTransition);
            RegisterInstance(_sceneSwitcher);
            RegisterInstance(_mixer);
            RegisterInstance(GetInput());

            _sceneSwitcher.SceneChanged += RegisterSceneContext;
        }

        public void Dispose()
        {
            _sceneSwitcher.SceneChanged -= RegisterSceneContext;
        }

        private void RegisterSceneContext(SceneContext context)
        {
            if (_registeredTypes.ContainsKey(typeof(SceneContext)))
                _registeredTypes.Remove(typeof(SceneContext));

            context.Construct();
            RegisterInstance(context);
        }

        public T Resolve<T>() => (T) _registeredTypes[typeof(T)];

        private void RegisterInstance<T>(T instance) => _registeredTypes.Add(typeof(T), instance);

        private Input GetInput() => new Input();
    }
}