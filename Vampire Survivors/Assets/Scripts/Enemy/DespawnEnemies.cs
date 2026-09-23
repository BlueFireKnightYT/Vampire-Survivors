using System.Threading.Tasks;
using UnityEngine;

public class DespawnEnemies : MonoBehaviour
{
    Collider2D coll;
    private async void OnTriggerExit2D(Collider2D collision)
    {
        coll = collision;
        if (collision.CompareTag("Enemy"))
        {
            if (coll.gameObject.activeSelf)
                await RespawnEnemy(coll);
            else
                return;
        }

    }

    async Task RespawnEnemy(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(15, 30);

        Vector3 spawnPos = transform.position + (Vector3)(randomDirection * randomDistance);

        collision.gameObject.transform.position = spawnPos;
        await Task.Yield();
        collision.gameObject.SetActive(true);
    }
}
