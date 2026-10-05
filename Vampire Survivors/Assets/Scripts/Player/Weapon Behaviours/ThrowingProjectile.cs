using UnityEngine;

public class ThrowingProjectile : MonoBehaviour, IAttacker
{
    [HideInInspector] public float damage;

    public void DealDamage(float damage, IDamagable target)
    {
        target.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get IDamagable
        //Deal damage
        if (collision.CompareTag("Enemy"))
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            DealDamage(damage, target);
        }
    }
}
