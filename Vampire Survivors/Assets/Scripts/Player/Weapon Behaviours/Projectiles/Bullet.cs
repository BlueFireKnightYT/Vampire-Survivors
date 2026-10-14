using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    public float bulletSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
    }
}