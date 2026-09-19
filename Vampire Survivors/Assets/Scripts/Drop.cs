using System.Xml.XPath;
using UnityEngine;

public class Drop : MonoBehaviour // regelt looks en travel
{
    GameObject player;
    public DropSO so;
    XpOrb xpPickup;

    SpriteRenderer sr;

    float pickupRange;
    float speed;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = so.sprite;

        pickupRange = so.range;
        speed = so.speed;

        TryGetComponent<XpOrb>(out xpPickup);

        if(xpPickup != null)
            xpPickup.xpAmount = so.amount;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= pickupRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
        }
    }
}
