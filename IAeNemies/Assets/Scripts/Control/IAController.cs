using System;
using Combat;
using UnityEngine;
using UnityEngine.Serialization;

public class IAController : MonoBehaviour
{
    [SerializeField] private float chaseDistance;
    Fighter fighter;
    GameObject player;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
        if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            fighter.Attack(player);
        }
        else
        {
            fighter.Cancel();
        }
        
    }

    private bool InAttackRangeOfPlayer()
    {
        float distancetoPlayer = Vector3.Distance(player.transform.position,transform.position);
        return distancetoPlayer < chaseDistance;
    }
}
