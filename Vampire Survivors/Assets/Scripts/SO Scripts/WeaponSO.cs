using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public int maxLevel;
    public float damage;
    public float interval;
    public GameObject projectile;
}
