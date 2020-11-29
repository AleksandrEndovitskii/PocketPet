using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class UserInterfaceManager : SerializedMonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private Canvas _canvasPrefab;
        [SerializeField]
        private EventSystem _eventSystemPrefab;
        [SerializeField]
        private Dictionary<string, RectTransform> _sceneNameUserInterfacePrefabs = new Dictionary<string, RectTransform>();

        [NonSerialized]
        public Canvas CanvasInstance;
        [NonSerialized]
        public EventSystem EventSystemInstance;
        [NonSerialized]
        public RectTransform UserInterfaceInstance;

        public void Initialize()
        {
            Subscribe();
        }
        public void UnInitialize()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        }
        private void UnSubscribe()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
        }

        private void SceneManagerOnActiveSceneChanged(Scene previousScene, Scene currentScene)
        {
            InstantiateUserInterfaceForCurrentScene(currentScene);
        }

        private void InstantiateUserInterfaceForCurrentScene(Scene currentScene)
        {
            var keyValuePair = _sceneNameUserInterfacePrefabs.FirstOrDefault(x =>
                x.Key == currentScene.name);
            if (keyValuePair.Key == null)
            {
                return;
            }

            CanvasInstance = Instantiate(_canvasPrefab);
            EventSystemInstance = Instantiate(_eventSystemPrefab);
            UserInterfaceInstance = Instantiate(keyValuePair.Value, CanvasInstance.transform);
        }
    }
}
