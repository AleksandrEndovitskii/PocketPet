using Components.InteractionComponents;
using Managers;
using UnityEngine;

namespace Components.SceneComponents
{
    public class LoadSceneAsyncInteractableComponent : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private string _sceneName;

        public void Interact()
        {
            GameManager.Instance.SceneLoadingManager.LoadScene(_sceneName);
        }
    }
}
