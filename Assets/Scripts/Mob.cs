using UnityEngine;

[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(MobController))]
public class Mob : MonoBehaviour
{
    public MobData mobData;
    private Rigidbody rb;
    private HealthModule healthModule;

    public bool canSplit;
    public int splitCount;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthModule = GetComponent<HealthModule>();
    }

    void Start()
    {
        splitCount = 0;
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

        if (splitCount <= 2)
        {
            gameObject.transform.localScale *= 0.5f;
            rb.AddForceAtPosition(Vector3.up * 1000f, transform.position, ForceMode.Impulse);
            GetComponent<MobController>().navMeshAgent.velocity = rb.velocity;
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
}
