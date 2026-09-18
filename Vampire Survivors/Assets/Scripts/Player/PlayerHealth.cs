using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public float maxHp;
    float hp;
    Slider hpBarSlider;

    public float immunityTime;
    float remainingTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        hp = maxHp;
        hpBarSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
    }

    public void TakeDamage(float Damage) // IDamagable
    {
        if (remainingTime < 0)
        {
            hp -= Damage;
            hpBarSlider.value = hp;

            if (hp <= 0)
            {
                Die();
            }
            else remainingTime = immunityTime;
        }
    }

    public void Die() //IDamagable
    {
        Destroy(this.gameObject);
    }
}
