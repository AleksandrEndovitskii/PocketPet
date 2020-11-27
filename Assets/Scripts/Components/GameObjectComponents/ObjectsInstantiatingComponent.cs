using System.Collections.Generic;
using UnityEngine;

namespace Components.GameObjectComponents
{
    public class ObjectsInstantiatingComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private int _instancesCount;
        [SerializeField]
        private Transform _parent;

        public List<GameObject> Instances = new List<GameObject>();

        private void Awake()
        {
            for (var i = 0; i < _instancesCount; i++)
            {
                var instance = Instantiate(_prefab, _parent);

                Instances.Add(instance);
            }
        }
    }
}
