using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneAsyncButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private string _sceneName;

        private void Awake()
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        }
        private void OnDestroy()
        {
            this.gameObject.GetComponent<Button>().onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            StartCoroutine(LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(_sceneName);

            Debug.Log($"Scene({_sceneName}) async loading is started");

            // Wait until the asynchronous scene fully loads
            while (!asyncOperation.isDone)
            {
                yield return null;

                Debug.Log($"Scene({_sceneName}) async loading is finished");
            }
        }
    }
}