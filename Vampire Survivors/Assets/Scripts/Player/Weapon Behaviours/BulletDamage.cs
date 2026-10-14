using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [HideInInspector] public float damage;
    public bool canPierce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            DealDamage(damage, target);
            print(damage);
        }

        if (!canPierce)
            gameObject.SetActive(false);
    }

    public void DealDamage(float damage, IDamagable target)
    {
        target.TakeDamage(damage);
    }
}
