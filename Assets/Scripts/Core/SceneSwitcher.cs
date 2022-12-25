using UnityEngine;
using UnityEngine.SceneManagement;
using ZenoJam.Utils;
using Cysharp.Threading.Tasks;
using System;
using ZenoJam.Common;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class SceneSwitcher 
    {
        public event Action SceneChangeInitiated;
        public event Action<SceneContext> SceneChanged;

        private SceneTransition _transition;

        private SceneType _currentScene;
        private SceneType _nextScene;

        public SceneSwitcher(SceneTransition transition) 
        {
            _transition = transition;
        }

        public void ReloadCurrentScene()
        {
            ReloadSceneAsync().Forget();
        }

        public void ChangeScene(SceneType sceneType) 
        {
            ChangeSceneAsync(sceneType).Forget();
        }

        public async UniTask ChangeSceneAsync(SceneType sceneType) 
        {
            SceneChangeInitiated?.Invoke();

            _nextScene = sceneType;

            AsyncOperation loadScene = SceneManager.LoadSceneAsync((int) sceneType, LoadSceneMode.Additive);

            ToggleTransition(_transition, true);

            await LoadSceneAsync(loadScene);

            ToggleTransition(_transition, false);

            if ((int) _currentScene != Constants.START_SCENE_INDEX)
                loadScene.completed += async (operation) => await UnloadSceneAsync((int)_currentScene);

            loadScene.completed -= OnSceneLoadCompleted;
            loadScene.completed -= async (operation) => await UnloadSceneAsync((int)_currentScene);

            _currentScene = sceneType;
        }

        private async UniTask ReloadSceneAsync() 
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3f));

            ToggleTransition(_transition, true);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            SceneChangeInitiated?.Invoke();

            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync((int)_currentScene);

            await UniTask.WaitUntil(() => unloadScene.isDone);

            AsyncOperation loadScene = SceneManager.LoadSceneAsync((int)_currentScene, LoadSceneMode.Additive);

            await LoadSceneAsync(loadScene);

            ToggleTransition(_transition, false);

            loadScene.completed -= OnSceneLoadCompleted;
        }

        private async UniTask LoadSceneAsync(AsyncOperation loadScene) 
        {

            loadScene.allowSceneActivation = false;

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            loadScene.completed += OnSceneLoadCompleted;

            UniTask secondDelayTask = UniTask.Delay(TimeSpan.FromSeconds(2f));
            UniTask loadSceneTask = UniTask.WaitUntil(() => loadScene.progress == 0.9f);

            await UniTask.WhenAll(secondDelayTask, loadSceneTask);

            loadScene.allowSceneActivation = true;

            await UniTask.WaitUntil(() => loadScene.isDone);
        }

        private void ToggleTransition(SceneTransition transition, bool value) 
        {
            switch (value)
            {
                case true:
                    transition.gameObject.SetActive(true);
                    transition.Activate();
                    break;

                case false:
                    transition.Deactivate();
                    break;
            }
        }

        private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;

            SceneChanged?.Invoke(GetContext(SceneManager.GetSceneByBuildIndex((int) _nextScene)));
        }

        private void OnSceneLoadCompleted(AsyncOperation operation)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex((int)_nextScene);

            SceneManager.SetActiveScene(scene);
        }

        private async UniTask UnloadSceneAsync(int buildIndex) 
        {
            await SceneManager.UnloadSceneAsync(buildIndex);
        }

        private SceneContext GetContext(Scene scene) 
        {
            var allObjects = scene.GetRootGameObjects();

            foreach (var obj in allObjects) 
            {
                if (obj.TryGetComponent(out SceneContext context)) 
                    return context;
            }

            throw new MissingComponentException($"{scene.name} does'nt contains a Scene Context");
        }
    }
}