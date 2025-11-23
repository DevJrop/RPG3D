using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Combat;
using Core;
using Movement;
using Recourses;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        [SerializeField] private float maxNavMeshProjectionDistance = 1f;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] private CursorMapping[] cursorMappings = null;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            if (InteractWithComponent()) return;
            if(InteractWithMovement())return;
            
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();

            foreach (RaycastHit hit in hits)
            {
                MonoBehaviour[] behaviours = hit.transform.GetComponents<MonoBehaviour>();
                
                foreach (MonoBehaviour behaviour in behaviours)
                {
                    if (behaviour is IRaycastable raycastable)
                    {
                        if (raycastable.HandleRaycast(this))
                        {
                            SetCursor(raycastable.GetCursorType());
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        RaycastHit[] RaycastAllSorted()
        {
            
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            } 
            return false;
        }
        
        private CursorMapping GetCursorMappings(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMappings(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private bool InteractWithMovement()
        {
            
            Vector3 target;
            bool hasHit = RaycastNavmesh(out target);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);  
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavmesh(out Vector3 target)
        {
            target = new Vector3();
            
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas );
            if (!hasCastToNavMesh) return false;
            target = navMeshHit.position;
            return true;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
             
        }
    }
}