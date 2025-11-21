using UnityEngine;

namespace Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
   
        [SerializeField] GameObject targetDestroy = null;
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetDestroy != null)
                {
                    Destroy(targetDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
