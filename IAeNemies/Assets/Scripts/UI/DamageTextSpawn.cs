using UnityEngine;

namespace UI
{
    public class DamageTextSpawn : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;
        
        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate(damageTextPrefab, transform);
            instance.SetValue(damageAmount);
        }
    }
}
