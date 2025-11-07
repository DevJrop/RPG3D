using System;
using Combat;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

public class IAController : MonoBehaviour
{
    [SerializeField] private float chaseDistance;
    Fighter fighter;
    GameObject player;
    Health health;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (health.IsDead()) return;
        
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
