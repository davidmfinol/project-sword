using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    private MobData mobData;
    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        mobData = GetComponent<Mob>().mobData;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.speed = mobData.moveSpeed;
    }

    void Follow(Transform target) { }

    void Attack() { }
}