using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoadingManager : MonoBehaviour
    {
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            Debug.Log($"Scene({sceneName}) async loading is started");

            // Wait until the asynchronous scene fully loads
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            Debug.Log($"Scene({sceneName}) async loading is finished");
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }
}
