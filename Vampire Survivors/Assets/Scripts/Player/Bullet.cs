using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IAttacker
{
    public float damage;
    Rigidbody2D rb;

    public float bulletSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5);
    }
    private void Update()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            DealDamage(damage, target);
        }

        Destroy(this.gameObject);
    }

    public void DealDamage(float damage, IDamagable target)
    {
        target.TakeDamage(damage);
    }
}
