using UnityEngine;

[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(MobController))]
public class Mob : MonoBehaviour
{
    public MobData mobData;
    private HealthModule healthModule;

    void Awake()
    {
        healthModule = GetComponent<HealthModule>();
    }

    void OnEnable()
    {
        healthModule.onDeath += Die;
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
    }
}
