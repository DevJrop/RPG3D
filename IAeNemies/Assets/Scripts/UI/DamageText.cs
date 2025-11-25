using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] Text damageText;
        private void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetValue(float amount)
        {
            damageText.text = String.Format("{0:0}", amount);
        }
    }
}
