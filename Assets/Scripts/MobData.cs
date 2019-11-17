using UnityEngine;

[CreateAssetMenu(fileName = "MobData", menuName = "Data/MobData")]
public class MobData : ScriptableObject
{
    public string mobName;
    public float moveSpeed;
    public float attackRange;
}