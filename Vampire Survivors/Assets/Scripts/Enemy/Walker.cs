using UnityEngine;

public class Walker : MonoBehaviour, IEnemyBehaviour
{
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Initialize(EnemySO enemySO)
    {
        //not used
    }

    public void Move(float moveSpeed, Rigidbody2D rigidbody2D)
    {
        Vector2 targetDirection = (player.transform.position - transform.position).normalized;
        Vector2 targetVelocity = targetDirection * moveSpeed;

        rigidbody2D.linearVelocity = Vector2.MoveTowards(rigidbody2D.linearVelocity, targetVelocity, moveSpeed * Time.fixedDeltaTime * 5f);
    }
}
