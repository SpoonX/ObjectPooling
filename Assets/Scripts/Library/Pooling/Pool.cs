using System.Collections.Generic;
using UnityEngine;

namespace Library.Pooling
{
    public class Pool : MonoBehaviour
    {
        public PoolConfig config;

        private readonly Queue<GameObject> _queue = new Queue<GameObject>();

        public void Init(PoolConfig poolConfig)
        {
            config = poolConfig;

            config.prefab.SetActive(false);

            if (config.preload < 1) return;

            for (int i = 0; i < config.preload; i++)
            {
                _queue.Enqueue(InstantiatePrefab(Vector3.zero, Quaternion.identity, transform));
            }
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject instance = _queue.Count < 1 ? InstantiatePrefab(position, rotation, parent) :  _queue.Dequeue();

            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = parent;

            instance.SetActive(true);

            return instance;
        }

        public void Despawn(GameObject instance)
        {
            instance.SetActive(false);

            if (_queue.Count >= config.size)
            {
                if (config.maintainLink) PoolManager.Instance.UnlinkInstance(instance);

                Destroy(instance);

                return;
            }

            _queue.Enqueue(instance);

            instance.transform.parent = transform;
        }

        private GameObject InstantiatePrefab(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject instance = Instantiate(config.prefab, position, rotation, parent);

            if (config.maintainLink) PoolManager.Instance.LinkInstance(instance, this);

            return instance;
        }
    }
}
