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
    [SerializeField] private float waypointTolerance = 1f;
    [SerializeField] private float wayPointDwellTime = 2f;
    Fighter fighter;
    GameObject player;
    Health health;
    [SerializeField] PatrolPath patrolPath;
    Mover mover;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;
    Vector3 guardPosition;

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
            AttackBehaviour();
        }
        else if(timeSinceLastSawPlayer < suspiciusTime)
        {
            SuspiciusBehavior();
        }
        else
        {
            PatrolBehaviour();
        }
        
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;
        
        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }

        if (timeSinceArrivedAtWaypoint > wayPointDwellTime)
        {
            mover.StartMoveAction(nextPosition);
        }
        
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWayPoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void SuspiciusBehavior()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
        timeSinceLastSawPlayer = 0f;
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
