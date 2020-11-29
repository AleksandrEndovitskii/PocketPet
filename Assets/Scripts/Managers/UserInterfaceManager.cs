using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class UserInterfaceManager : SerializedMonoBehaviour, IInitilizable
    {
        [SerializeField]
        private Canvas _canvasPrefab;
        [SerializeField]
        private EventSystem _eventSystemPrefab;
        [SerializeField]
        private Dictionary<string, RectTransform> _sceneNameUserInterfacePrefabs = new Dictionary<string, RectTransform>();

        private Canvas _canvasInstance;
        private EventSystem _eventSystemInstance;
        private RectTransform _userInterfaceInstance;

        public void Initialize()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
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

            _canvasInstance = Instantiate(_canvasPrefab);
            _eventSystemInstance = Instantiate(_eventSystemPrefab);
            _userInterfaceInstance = Instantiate(keyValuePair.Value, _canvasInstance.transform);
        }
    }
}
