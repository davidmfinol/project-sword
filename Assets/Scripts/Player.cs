using UnityEngine;

[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(PlayerController))]
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