using UnityEngine;

public class Walker : MonoBehaviour, IEnemyBehaviour
{
    GameObject player;

    public int enemyType;
    Vector2 targetPosition = new Vector2();

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
        if(enemyType == 0)
        {
            targetPosition = Vector2.zero;
            Vector2 targetDirection = (player.transform.position - transform.position).normalized;
            Vector2 targetVelocity = targetDirection * moveSpeed;

            rigidbody2D.linearVelocity = Vector2.MoveTowards(rigidbody2D.linearVelocity, targetVelocity, moveSpeed * Time.fixedDeltaTime * 5f);
        }
        else if (enemyType == 1)
        {
            if(targetPosition == Vector2.zero)
            {
                targetPosition = (player.transform.position - transform.position).normalized;
            }

            Vector2 targetVelocity = targetPosition * moveSpeed;
            rigidbody2D.linearVelocity = Vector2.MoveTowards(rigidbody2D.linearVelocity, targetVelocity, moveSpeed * Time.fixedDeltaTime * 5f);
        }
    }
}
