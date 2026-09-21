using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    public float waveDelay;
    public float spawnDelay;
    public List<WaveEnemyData> enemies = new List<WaveEnemyData>();
}
