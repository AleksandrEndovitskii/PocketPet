using UnityEngine;

namespace Components.GameObjectComponents
{
    public class DontDestroyOnLoadGameObjectComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene
        }
    }
}
