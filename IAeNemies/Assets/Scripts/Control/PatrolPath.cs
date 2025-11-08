using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolPath : MonoBehaviour
{
    const float wayPointRadius = 0.3f;
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var j = GetNextIndex(i);
            Gizmos.DrawSphere(GetWayPoint(i), wayPointRadius); 
            Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
        }
    }

    public int GetNextIndex(int i)
    {
        if (i + 1 == transform.childCount)
        {
            return 0;
        }
        return i + 1;
    }

    public Vector3 GetWayPoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
