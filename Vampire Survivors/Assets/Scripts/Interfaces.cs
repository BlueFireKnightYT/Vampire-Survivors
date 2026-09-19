using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damage);
    void Die();
}

public interface IEnemyBehaviour
{
    void Initialize(EnemySO enemySO);
    void Move(float moveSpeed, Rigidbody2D rigidbody2D);
}

public interface IAttacker
{
    void DealDamage(float damage, IDamagable target);
}

public interface IDroppable
{
    void DecideDrop();
    void Drop(GameObject drop);
}

public interface IWeapon
{
    void AutoShoot();
}

public interface IPickupable
{
    void pickupObject();
}