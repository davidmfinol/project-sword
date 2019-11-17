using UnityEngine;

public class Sword : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Mob"))
        {
            other.gameObject.GetComponent<HealthModule>().DecrementHealth(10f);
        }
    }
}