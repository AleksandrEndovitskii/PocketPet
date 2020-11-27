using System;
using Components.GameObjectComponents;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour, IInitilizable
    {
        [SerializeField]
        private ObjectsInstantiatingComponent _prefab;
        [SerializeField]
        private Transform _parent;
        [NonSerialized]
        public ObjectsInstantiatingComponent Instance;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            Instance = Instantiate(_prefab, _parent);
        }
    }
}
