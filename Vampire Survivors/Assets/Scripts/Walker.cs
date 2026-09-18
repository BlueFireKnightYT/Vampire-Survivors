using UnityEngine;

public class Walker : MonoBehaviour, IEnemyBehaviour
{
    private EnemySO enemySO;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Initialize(EnemySO enemySO)
    {
        this.enemySO = enemySO;
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .1f);
    }
}
