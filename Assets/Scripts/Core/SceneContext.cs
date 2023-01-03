using System;
using System.Collections.Generic;
using UnityEngine;
using ZenoJam.Common;

namespace ZenoJam.Core
{
    public class SceneContext : MonoBehaviour 
    {
        [SerializeField] private UIContext _uiContext;
        [SerializeField] private SceneCamera _camera;
        [SerializeField] private SceneChangeTrigger _sceneChangeTrigger;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private TriggersContainer _triggersContainer;

        private Dictionary<Type, object> _registeredTypes;

        public IReadOnlyDictionary<Type, object> RegisteredTypes => _registeredTypes;

        public void Construct()
        {
            _registeredTypes = new Dictionary<Type, object>();

            RegisterInstance(GetPlayer());
            RegisterInstance(_triggersContainer);
            RegisterInstance(_uiContext);
            RegisterInstance(_camera);
            RegisterInstance(_sceneChangeTrigger);

            SpawnEnemies();
        }

        public T Resolve<T>() => (T)_registeredTypes[typeof(T)];

        private void RegisterInstance<T>(T instance) => _registeredTypes.Add(typeof(T), instance);

        private Player GetPlayer() 
        {
            _playerSpawner.Initialize();

            return _playerSpawner.Create();
        }

        private void SpawnEnemies() 
        {
            _enemySpawner.Initialize();

            _enemySpawner.SpawnAll();
        }
    }
}