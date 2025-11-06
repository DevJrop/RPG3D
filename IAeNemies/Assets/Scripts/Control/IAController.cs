using UnityEngine;
using UnityEngine.Serialization;

public class IAController : MonoBehaviour
{
    [SerializeField] private float chaseDistance;
    void Update()
    {
        if (DistanceToPlayer() < chaseDistance)
        {
            Debug.Log("Ahi te vi perro" );
        }
        
    }

    private float DistanceToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return Vector3.Distance(player.transform.position,transform.position);
    }
}
