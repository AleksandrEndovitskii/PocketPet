using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(SoundManager))]
    public class GameManager : MonoBehaviour
    {
        // static instance of GameManager which allows it to be accessed by any other script
        public static GameManager Instance;

        public SoundManager SoundManager
        {
            get { return this.gameObject.GetComponent<SoundManager>(); }
        }

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

        public void Initialize()
        {
            SoundManager.Initialize();
        }
    }
}
