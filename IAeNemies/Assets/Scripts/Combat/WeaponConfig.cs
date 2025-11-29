using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Combat
{
    public class WeaponConfig : MonoBehaviour
    {
        [SerializeField]private UnityEvent onHit;
        
        public void OnHit()
        {
            onHit?.Invoke();
        }
    }
}
