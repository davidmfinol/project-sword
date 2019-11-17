using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    private MobData mobData;
    private Animator animator;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    private Transform target;

    void Awake()
    {
        mobData = GetComponent<Mob>().mobData;
        animator = GetComponent<Animator>();
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
        Follow(target);
        if (GetDistanceToTarget() <= mobData.attackRange)
        {
            Attack();
            animator.SetBool("attack", true);
        }
        else
        {
            animator.SetBool("attack", false);
        }
    }

    float GetDistanceToTarget() => navMeshAgent.remainingDistance;

    void Follow(Transform target)
    {
        navMeshAgent.SetDestination(target.position);
        animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
    }

    void Attack()
    {
        animator.SetBool("attack", true);
    }
}