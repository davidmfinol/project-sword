using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    private MobData mobData;
    private NavMeshAgent navMeshAgent;

    private Transform target;

    void Awake()
    {
        mobData = GetComponent<Mob>().mobData;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent.speed = mobData.moveSpeed;
        navMeshAgent.SetDestination(target.position);
    }

    void Update()
    {
        if (GetDistanceToTarget() > mobData.attackRange)
        {
            Follow(target);
        }
        else
        {
            Attack();
        }
    }

    float GetDistanceToTarget() => navMeshAgent.remainingDistance;

    void Follow(Transform target)
    {
        navMeshAgent.SetDestination(target.position);
    }

    void Attack()
    {
    }
}