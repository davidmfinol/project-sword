using UnityEngine;

[RequireComponent(typeof(HealthModule))]
public class Player : MonoBehaviour
{
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

    }
}