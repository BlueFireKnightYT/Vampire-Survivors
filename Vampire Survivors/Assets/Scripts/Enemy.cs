using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour, IDamagable //Dit is waar je de interface referenced
{
    //dit is het Basis Script
    //Deze maakt gebruik van interfaces
    //Hier worden functies voor IEDERE enemy gemaakt

    public EnemySO enemySO;
    private IEnemyBehaviour enemyBehaviour;

    private float health;

    private void Awake()
    {
        if(TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemySO);
        }
        else
        {
            Debug.LogWarning("No Scriptable Object found on " + gameObject.name);
        }

        health = enemySO.health;
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {
        enemyBehaviour.Move();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die() //Deze 2 zijn gereferenced uit IDamagable
    {
        //Drop item
        Destroy(gameObject);
    }
}
