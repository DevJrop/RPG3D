using System.Collections;
using System.Collections.Generic;
using Control;
using Core;
using Recourses;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
        
    }
}
