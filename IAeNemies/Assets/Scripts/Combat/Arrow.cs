using Core;
using UnityEngine;

namespace Combat
{
    public class Arrow : MonoBehaviour
    {
        Health target = null;
        float damage = 0f;
        [SerializeField] private float speed = 1;
        void Update()
        {
            if (target == null) return;
        
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        public Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
            if (targetCapsuleCollider == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsuleCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
            
        }
    }
}
