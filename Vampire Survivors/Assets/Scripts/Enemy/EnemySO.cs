using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO", order = 1)]
public class EnemySO : ScriptableObject
{
    public string objectName;
    public Sprite sprite;

    public float health;
    public float damage;
    public float speed;

    public int xp;
    public int gold;
    public float dropPercentage;
}
