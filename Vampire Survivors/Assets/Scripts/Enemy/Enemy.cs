using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour, IDamagable, IAttacker //Dit is waar je de interface referenced
{
    //dit is het Basis Script
    //Deze maakt gebruik van interfaces
    //Hier worden functies voor IEDERE enemy gemaakt

    public EnemySO enemySO;
    SpriteRenderer sr;
    Rigidbody2D rb;
    IEnemyBehaviour enemyBehaviour;

    IDamagable playerDamagable;

    float health;
    float damage;

    void Awake()
    {
        if(TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemySO);
        }
        else
        {
            Debug.LogWarning("No Scriptable Object found on " + gameObject.name);
        }

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        health = enemySO.health;
        damage = enemySO.damage;
    }

   void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(1);
        }
    }

    void FixedUpdate()
    {
        enemyBehaviour.Move(enemySO.speed, rb);

        if(rb.linearVelocityX < 0) sr.flipX = true;
        else if (rb.linearVelocityX > 0) sr.flipX = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<IDamagable>(out playerDamagable))
                DealDamage(damage, playerDamagable);
        }
    }

    public void TakeDamage(float damage) //Deze 2 zijn gereferenced uit IDamagable
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die() 
    {
        //Drop item
        Destroy(gameObject);
    }

    public void DealDamage(float damage, IDamagable target)
    {
        target?.TakeDamage(damage);
    }
}
