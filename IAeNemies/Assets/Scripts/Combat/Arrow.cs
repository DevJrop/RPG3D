using System;
using Core;
using UnityEngine;

namespace Combat
{
    public class Arrow : MonoBehaviour
    {
        Health target = null;
        float damage = 0f;
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isHoming = true;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead()) 
            {
               transform.LookAt(GetAimLocation());
            }
        
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
            
        }
    }
}
