using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    public float waveDelay;
    public float spawnDelay;
    public int waveType;
    // 0 = normal
    // 1 = charging
    public List<WaveEnemyData> enemies = new List<WaveEnemyData>();
}
