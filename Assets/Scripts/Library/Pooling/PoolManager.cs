using System;
using System.Collections.Generic;
using UnityEngine;

namespace Library.Pooling
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance { get; private set; }

        public PoolConfig[] poolConfigs;

        private readonly Dictionary<GameObject, Pool> _pools = new Dictionary<GameObject, Pool>();

        private readonly Dictionary<GameObject, Pool> _instancePools = new Dictionary<GameObject, Pool>();

        private void Awake()
        {
            Instance = this;

            foreach (PoolConfig config in poolConfigs)
            {
                CreatePool(config);
            }
        }

        private void CreatePool(PoolConfig config)
        {
            Pool pool = new GameObject($"Pool ({ config.prefab.name })").AddComponent<Pool>();

            pool.Init(config);

            _pools.Add(config.prefab, pool);
        }

        public Pool GetPool(GameObject prefab)
        {
            bool hasPool = _pools.TryGetValue(prefab, out Pool pool);

            if (!hasPool) throw new Exception($"No pool found for provided prefab '{prefab.name}'!");

            return pool;
        }

        public void LinkInstance(GameObject instance, Pool pool)
        {
            _instancePools.Add(instance, pool);
        }

        public void UnlinkInstance(GameObject instance)
        {
            _instancePools.Remove(instance);
        }

        public void Despawn(GameObject instance)
        {
            if (!_instancePools.TryGetValue(instance, out Pool pool))
            {
                throw new Exception(
                    $"Unable to find pool for '{instance.name}'! "+
                    "Did you forget to enable 'maintainLink' in the config?"
                );
            }

            pool.Despawn(instance);
        }
    }
}
