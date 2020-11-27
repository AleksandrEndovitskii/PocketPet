using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class CursorLockingManager: MonoBehaviour, IInitilizable
    {
        public Action<CursorLockMode> SelectedCursorLockModeChanged = delegate {  };

        [SerializeField]
        private List<SceneCursorLockMode> _sceneCursorLockModes = new List<SceneCursorLockMode>();
        [SerializeField]
        private CursorLockMode _defaultCursorLockMode = CursorLockMode.None;

        [Serializable]
        public class SceneCursorLockMode
        {
            public Scene scene;
            public CursorLockMode cursorLockMode;
        }

        public CursorLockMode SelectedCursorLockMode
        {
            get
            {
                return Cursor.lockState;
            }
            set
            {
                if (Cursor.lockState == value)
                {
                    return;
                }

                Debug.Log(
                    $"Selected cursor lock mode changed from {Cursor.lockState} to {value}");

                Cursor.lockState = value;

                SelectedCursorLockModeChanged.Invoke(Cursor.lockState);
            }
        }

        public void Initialize()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        }

        private void SceneManagerOnActiveSceneChanged(Scene previousScene, Scene currentScene)
        {
            if (_sceneCursorLockModes.All(x=>x.scene != currentScene))
            {
                Debug.LogWarning($"Cursor lock mode for scene({currentScene}) not specified - " +
                                 $"resetting it to default state({_defaultCursorLockMode})");

                SelectedCursorLockMode = _defaultCursorLockMode;

                return;
            }

            var cursorLockMode = _sceneCursorLockModes.First(x=>x.scene == currentScene);

            SelectedCursorLockMode = cursorLockMode.cursorLockMode;
        }
    }
}