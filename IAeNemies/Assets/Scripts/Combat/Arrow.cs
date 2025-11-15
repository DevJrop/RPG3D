using Core;
using UnityEngine;

namespace Combat
{
    public class Arrow : MonoBehaviour
    {
        Health target = null;
        [SerializeField] private float speed = 1;

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
        
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target)
        {
            this.target = target;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
            if (targetCapsuleCollider == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsuleCollider.height / 2;
        }
    }
}
