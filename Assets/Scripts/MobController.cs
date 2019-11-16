using UnityEngine;

public class MobController : MonoBehaviour
{
    private MobData mobData;

    void Awake()
    {
        mobData = GetComponent<Mob>().mobData;
    }

    void Follow(Transform target) { }

    void Attack() { }
}