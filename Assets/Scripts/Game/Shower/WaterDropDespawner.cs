using UnityEngine;

namespace Game.Shower
{
    public class WaterDropDespawner : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Destroy(other.gameObject);
        }
    }
}
