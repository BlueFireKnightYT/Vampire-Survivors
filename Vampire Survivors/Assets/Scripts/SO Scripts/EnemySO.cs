using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO", order = 1)]
public class EnemySO : ScriptableObject
{
    public string objectName;
    public RuntimeAnimatorController animator;

    public float health;
    public float damage;
    public float speed;

    public float dropRate;
}
