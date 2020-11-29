using System;
using System.Collections;
using System.Linq;
using Components.GameObjectComponents;
using Components.InteractionComponents;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour, IInitilizable
    {
        [SerializeField]
        private ObjectsInstantiatingComponent _ballsContainerPrefab;
        [SerializeField]
        private Transform _ballsContainerParent;
        [SerializeField]
        private int _startStepsCount;
        [SerializeField]
        private int _finishStepsCount;
        [SerializeField]
        private float _delayBeforeStartPlayNextUnPlayedSequenceSecondsCount;
        [SerializeField]
        private string _sceneName;
        [SerializeField]
        private AudioClip _errorAudioClip;

        public ObjectsInstantiatingComponent BallsContainerInstance;
        private MiniGameModel _miniGameModel;
        private Coroutine _playNextUnPlayedSequenceAfterDelayCoroutine;

        private void Awake()
        {
            Initialize();

            Subscribe();

            Debug.Log($"Mini game is started");

            PlayNextUnPlayedSequence();
        }

        public void Initialize()
        {
            BallsContainerInstance = Instantiate(_ballsContainerPrefab, _ballsContainerParent);

            var interactableBlueprints = BallsContainerInstance.Instances.Select(x =>
                x.GetComponent<IInteractable>()).ToList();
            _miniGameModel = new MiniGameModel(_startStepsCount,_finishStepsCount, interactableBlueprints);
        }

        private void Subscribe()
        {
            GameManager.Instance.AutoPlayManager.IsAutoplayOnChanged += OnIsAutoplayOnChanged;
            GameManager.Instance.InteractionManager.Interacted += OnInteracted;

            _miniGameModel.IsInteractedChanged += OnIsInteractedChanged;
        }

        private void OnIsAutoplayOnChanged(bool isAutoplayOn)
        {
            if (isAutoplayOn)
            {
                return;
            }

            PlayNextUnPlayedSequence();
        }

        private void OnInteracted(IInteractable interactable)
        {
            // interacted via autoplay - do not need to mark it as interacted
            if (GameManager.Instance.AutoPlayManager.IsAutoplayOn)
            {
                Debug.Log("Autoplay has interacted with interactable");

                return;
            }

            // all interactable is interacted - game is over
            if (_miniGameModel.FirstUnInteractedInteractable == null)
            {
                return;
            }

            if (interactable != _miniGameModel.FirstUnInteractedInteractable.Interactable)
            {
                Debug.Log("Player has interacted with wrong interactable - restarting last un-played sequence");

                GameManager.Instance.SoundManager.PlaySound(_errorAudioClip);

                _miniGameModel.FirstUnInteractedSequence.IsInteracted = false;

                return;
            }

            Debug.Log("Player has interacted with interactable");

            _miniGameModel.FirstUnInteractedInteractable.IsInteracted = true;
        }

        private void OnIsInteractedChanged(bool isInteracted)
        {
            if (!isInteracted)
            {
                return;
            }

            Debug.Log($"Mini game is ended");

            GameManager.Instance.SceneLoadingManager.LoadScene(_sceneName);
        }

        private void PlayNextUnPlayedSequence()
        {
            if (_playNextUnPlayedSequenceAfterDelayCoroutine != null)
            {
                StopCoroutine(_playNextUnPlayedSequenceAfterDelayCoroutine);
                _playNextUnPlayedSequenceAfterDelayCoroutine = null;
            }

            _playNextUnPlayedSequenceAfterDelayCoroutine = StartCoroutine(PerformActionAfterTimeCoroutine(
                _delayBeforeStartPlayNextUnPlayedSequenceSecondsCount, () => { StartAutoPlayOfNextUnPlayedSequence(); }));
        }

        private void StartAutoPlayOfNextUnPlayedSequence()
        {
            var unPlayedSequence = _miniGameModel.InteractableSequenceModels.FirstOrDefault(x =>
                x.IsInteracted == false);
            // all interactable sequences was played - game over
            if (unPlayedSequence == null)
            {
                return;
            }

            GameManager.Instance.AutoPlayManager.StartAutoPlay(unPlayedSequence);
        }

        private IEnumerator PerformActionAfterTimeCoroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);

            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}
