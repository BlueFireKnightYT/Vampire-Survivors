using UnityEngine;

public class XpOrb : MonoBehaviour, IPickupable // regelt functie van xp orb
{
    PlayerXpSystem xpSystem;
    public float xpAmount;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        xpSystem = player.GetComponent<PlayerXpSystem>();
    }

    void IPickupable.pickupObject()
    {
        xpSystem.GetXp(xpAmount);
    }
}
