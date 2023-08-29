using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinSpawner _spawner;

    public void Init(CoinSpawner spawner)
    {
        _spawner = spawner;
    }

    private void OnDestroy()
    {
        _spawner.OnCoinDestroed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeCoin();
            Destroy(gameObject);
        }
    }
}
