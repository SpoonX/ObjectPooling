using System;
using UnityEngine;

namespace Library.Pooling
{
    [Serializable]
    public class PoolConfig
    {
        // Used to identify the config in the inspector.
        public string name;

        // Keep a reference between instances and their pool?
        public bool maintainLink;

        // The prefab to make clones of.
        public GameObject prefab;

        // The max size of the pool.
        public int size;

        // How many clones to spawn on Awake
        public int preload;
    }
}
