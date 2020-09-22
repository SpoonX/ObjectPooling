using Library.Pooling;
using UnityEngine;

namespace Game.Shower
{
    public class WaterDropDespawner : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            PoolManager.Instance.Despawn(other.gameObject);
        }
    }
}
