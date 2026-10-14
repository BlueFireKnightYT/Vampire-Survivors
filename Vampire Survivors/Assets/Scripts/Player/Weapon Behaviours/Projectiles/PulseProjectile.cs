using UnityEngine;

public class PulseProjectile : MonoBehaviour
{
    bool isScaling = false;
    BulletDamage bulletDamage;
    GameObject player;

    private void Start()
    {
        bulletDamage = GetComponent<BulletDamage>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        isScaling = true;
    }
    private void Update()
    {
        if (!isScaling)
            return;

        transform.position = player.transform.position;
        transform.localScale += new Vector3(5, 5, 5) * Time.deltaTime;

        //lowers damage, but never less than 0
        if (bulletDamage.damage > 0.1f)
            //does damage - 10 per second
            bulletDamage.damage -= 10f * Time.deltaTime;
        else
            bulletDamage.damage = 0.1f;
    }
    private void OnDisable()
    {
        isScaling = false;
        transform.localScale = new Vector3(1, 1, 1);
    }
}
