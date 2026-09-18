using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damage);
    void Die();
}

public interface IEnemyBehaviour
{
    void Initialize(EnemySO enemySO);
    void Move();
}