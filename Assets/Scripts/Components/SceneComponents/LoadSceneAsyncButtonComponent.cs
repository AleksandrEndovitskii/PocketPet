using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.SceneComponents
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
            GameManager.Instance.SceneLoadingManager.LoadScene(_sceneName);
        }
    }
}