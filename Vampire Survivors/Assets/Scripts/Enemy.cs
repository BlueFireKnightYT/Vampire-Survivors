using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour, IDamagable //Dit is waar je de interface referenced
{
    //dit is het Basis Script
    //Deze maakt gebruik van interfaces
    //Hier worden functies voor IEDERE enemy gemaakt

    public EnemySO enemyData;
    private IEnemyBehaviour enemyBehaviour;

    private float health;

    private void Start()
    {
        health = enemyData.health;
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(1);
        }
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
