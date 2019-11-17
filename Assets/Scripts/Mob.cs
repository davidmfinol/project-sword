using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(MobController))]
public class Mob : MonoBehaviour
{
    public MobData mobData;
    private Rigidbody rb;
    private HealthModule healthModule;
    private NavMeshAgent navMeshAgent;

    public bool canSplit;
    public int splitCount;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthModule = GetComponent<HealthModule>();
        navMeshAgent = GetComponent<MobController>().navMeshAgent;
    }

    void OnEnable()
    {
        healthModule.onDeath += Die;
    }

    void OnDisable()
    {
        healthModule.onDeath -= Die;
    }

    void Split()
    {
        splitCount += 1;
        if (splitCount < 3)
        {
            gameObject.transform.localScale *= 0.75f;
            if (GetComponent<MobController>().navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.isStopped = true;
                navMeshAgent.enabled = false;
            }

            rb.AddForce(Vector3.up * 25f, ForceMode.Impulse);
            GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity);
            clone.GetComponent<Mob>().canSplit = true;
            clone.GetComponent<Mob>().splitCount += splitCount + 1;
            clone.GetComponent<Rigidbody>().AddForce(-clone.transform.forward * 10f, ForceMode.Impulse);
            clone.GetComponent<Rigidbody>().velocity = Vector3.zero;
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(GameObject.FindObjectOfType<Player>().transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Die()
    {
        if (canSplit)
        {
            Split();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(GameObject.FindObjectOfType<Player>().transform.position);
        }
    }
}
