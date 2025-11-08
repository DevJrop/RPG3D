using System;
using Combat;
using Core;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

public class IAController : MonoBehaviour
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float suspiciusTime;
    Fighter fighter;
    GameObject player;
    Health health;
    Vector3 guardPosition;
    Mover mover;
    float timeSinceLastSawPlayer = Mathf.Infinity;

    private void Start()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player");
        guardPosition = transform.position;
    }

    void Update()
    {
        if (health.IsDead()) return;
        
        if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            timeSinceLastSawPlayer = 0f;
            AttackBehaviour();
        }
        else if(timeSinceLastSawPlayer < suspiciusTime)
        {
            SuspiciusBehavior();
        }
        else
        {
            GuardBehaviour();
        }
        
        timeSinceLastSawPlayer += Time.deltaTime;
    }
    private void GuardBehaviour()
    {
        mover.StartMoveAction(guardPosition);
    }

    private void SuspiciusBehavior()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
        fighter.Attack(player);
    }

    private bool InAttackRangeOfPlayer()
    {
        float distancetoPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distancetoPlayer < chaseDistance;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        
    }
}
