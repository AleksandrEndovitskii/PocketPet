using System;
using UnityEngine;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(SettingsManager))]
    [RequireComponent(typeof(SceneLoadingManager))]
    [RequireComponent(typeof(CursorLockingManager))]
    [RequireComponent(typeof(SoundManager))]
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(SelectionManager))]
    [RequireComponent(typeof(HighlightManager))]
    [RequireComponent(typeof(InteractionManager))]
    [RequireComponent(typeof(AutoPlayManager))]
    [RequireComponent(typeof(UserInterfaceManager))]
    public class GameManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        // static instance of GameManager which allows it to be accessed by any other script
        public static GameManager Instance;

        public SettingsManager SettingsManager => this.gameObject.GetComponent<SettingsManager>();
        public SceneLoadingManager SceneLoadingManager => this.gameObject.GetComponent<SceneLoadingManager>();
        public CursorLockingManager CursorLockingManager => this.gameObject.GetComponent<CursorLockingManager>();
        public SoundManager SoundManager => this.gameObject.GetComponent<SoundManager>();
        public InputManager InputManager => this.gameObject.GetComponent<InputManager>();
        public SelectionManager SelectionManager => this.gameObject.GetComponent<SelectionManager>();
        public HighlightManager HighlightManager => this.gameObject.GetComponent<HighlightManager>();
        public InteractionManager InteractionManager => this.gameObject.GetComponent<InteractionManager>();
        public AutoPlayManager AutoPlayManager => this.gameObject.GetComponent<AutoPlayManager>();
        public UserInterfaceManager UserInterfaceManager => this.gameObject.GetComponent<UserInterfaceManager>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
                    Destroy(gameObject);
                }
            }

            Initialize();
        }
        private void OnDestroy()
        {
            UnInitialize();
        }

        public void Initialize()
        {
            SettingsManager.Initialize();
            CursorLockingManager.Initialize();
            SoundManager.Initialize();
            HighlightManager.Initialize();
            InteractionManager.Initialize();
            UserInterfaceManager.Initialize();
        }
        public void UnInitialize()
        {
            UserInterfaceManager.UnInitialize();
            InteractionManager.Uninitialize();
            HighlightManager.UnInitialize();
            SoundManager.UnInitialize();
            CursorLockingManager.UnInitialize();
            SettingsManager.UnInitialize();
        }
    }
}
