using UnityEngine;

namespace Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
   
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
