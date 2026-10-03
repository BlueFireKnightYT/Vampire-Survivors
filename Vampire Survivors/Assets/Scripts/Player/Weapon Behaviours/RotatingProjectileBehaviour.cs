using System.Collections;
using UnityEngine;

public class RotatingProjectileBehaviour : MonoBehaviour, IAttacker
{
    private Rigidbody2D playerRb;
    private float radius;
    private float orbitSpeed;
    private float currentAngle;

    public float damage;

    public void Setup(Rigidbody2D player, float initialAngleDegrees, float orbitRadius, float speed)
    {
        playerRb = player;
        currentAngle = initialAngleDegrees * Mathf.Deg2Rad;
        radius = orbitRadius;
        orbitSpeed = speed;

        UpdatePosition();
    }

    private void FixedUpdate()
    {
        if (playerRb == null) return;

        currentAngle += orbitSpeed * Time.fixedDeltaTime;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 offset = new Vector3(Mathf.Cos(currentAngle) * radius, Mathf.Sin(currentAngle) * radius, 0f);

        transform.position = (Vector3)playerRb.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            DealDamage(damage, target);
        }
    }

    public void DealDamage(float damage, IDamagable target)
    {
        target.TakeDamage(damage);
    }
}
