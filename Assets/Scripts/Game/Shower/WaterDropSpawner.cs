using System.Collections;
using UnityEngine;

namespace Game.Shower
{
    public class WaterDropSpawner : MonoBehaviour
    {
        public GameObject prefab;

        public int dropsPerSecond = 2;

        public Transform container;

        void Start()
        {
            StartCoroutine(SpawnDrops());
        }

        private IEnumerator SpawnDrops()
        {
            WaitForSeconds interval = new WaitForSeconds(1f / dropsPerSecond);

            while (true)
            {
                Instantiate(prefab, transform.position, Quaternion.identity, container);

                yield return interval;
            }
        }
    }
}
