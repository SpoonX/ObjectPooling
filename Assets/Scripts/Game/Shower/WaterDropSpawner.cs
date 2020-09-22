using System.Collections;
using Library.Pooling;
using UnityEngine;

namespace Game.Shower
{
    public class WaterDropSpawner : MonoBehaviour
    {
        public GameObject prefab;

        public int dropsPerSecond = 2;

        public Transform container;

        private Pool _pool;

        void Start()
        {
            _pool = PoolManager.Instance.GetPool(prefab);

            StartCoroutine(SpawnDrops());
        }

        private IEnumerator SpawnDrops()
        {
            WaitForSeconds interval = new WaitForSeconds(1f / dropsPerSecond);

            while (true)
            {
                _pool.Spawn(transform.position, Quaternion.identity, container);

                yield return interval;
            }
        }
    }
}
