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

        private List<GameObject> _instances = new List<GameObject>();

        private void Awake()
        {
            for (var i = 0; i < _instancesCount; i++)
            {
                var instance = Instantiate(_prefab, _parent);

                _instances.Add(instance);
            }
        }
    }
}
