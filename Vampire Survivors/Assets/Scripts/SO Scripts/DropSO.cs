using UnityEngine;

[CreateAssetMenu(fileName = "PickupSO", menuName = "Scriptable Objects/PickupSO")]
public class DropSO : ScriptableObject
{
    public float range;
    public Sprite sprite;
    public float speed;
    public float amount;
}
