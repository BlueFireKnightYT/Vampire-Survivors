using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour, IDamagable, IAttacker, IDroppable //Dit is waar je de interface referenced
{
    //dit is het Basis Script
    //Deze maakt gebruik van interfaces
    //Hier worden functies voor IEDERE enemy gemaakt

    public EnemySO enemySO;
    SpriteRenderer sr;
    Animator animator;
    Rigidbody2D rb;
    IEnemyBehaviour enemyBehaviour;
    GameObject player;
    PlayerXpSystem xpSystem;

    IDamagable playerDamagable;

    float maxHealth;
    float health;
    float damage;

    float dropRate;

    void Awake()
    {
        if (TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemySO);
        }
        else
        {
            Debug.LogWarning("No Scriptable Object found on " + gameObject.name);
        }

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        xpSystem = player.GetComponent<PlayerXpSystem>();


        maxHealth = enemySO.health;
        health = maxHealth;
        damage = enemySO.damage;
        animator.runtimeAnimatorController = enemySO.animator;
        dropRate = enemySO.dropPercentage;
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

        if(rb.linearVelocityX < 0) 
            sr.flipX = true;
        else if (rb.linearVelocityX > 0) 
            sr.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        int chance = Random.Range(1, 101);

        if (chance <= dropRate)
        {
            GameObject xpOrb = pooledXpOrb();
            
            Drop(xpOrb);
        }

        health = maxHealth;
        this.gameObject.SetActive(false);
    }

    GameObject pooledXpOrb()
    {
        GameObject foundEnemy = null;
        foreach (GameObject xpOrb in xpSystem.xpOrbPool)
        {
            if (!xpOrb.activeSelf)
            {
                foundEnemy = xpOrb;
                break;
            }
        }
        return foundEnemy;
    }


    public void DealDamage(float damage, IDamagable target)
    {
        target?.TakeDamage(damage);
    }

    public void DecideDrop()
    {
        //Unused in the enemy
    }

    public void Drop(GameObject drop)
    {
        drop.transform.position = transform.position;
        drop.SetActive(true);
        GameObject droppedObject = drop;
    }
}
