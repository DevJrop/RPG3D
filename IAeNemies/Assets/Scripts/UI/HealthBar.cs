using System;
using Recourses;
using UnityEngine;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;

       
        void Update()
        {
            if (Mathf.Approximately(healthComponent.GetFraction(),0) || Mathf.Approximately(healthComponent.GetFraction(),1))
            {
                rootCanvas.enabled = false;
                return;
            }
            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        }
    }
}
