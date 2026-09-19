using UnityEngine;

public class Pickup : MonoBehaviour // regelt het oppakken van de drops
{
    IPickupable pickupInterface;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Drop"))
        {
            pickupInterface = collision.gameObject.GetComponent<IPickupable>();
            pickupInterface.pickupObject();
            collision.gameObject.SetActive(false);
        }
    }
}
