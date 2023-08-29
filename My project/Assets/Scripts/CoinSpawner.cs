using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private float _spawnTime;

    private WaitForSeconds wait;

    public void OnCoinDestroed(Coin coin)
    {
        coin.ObjectDestroyed -= OnCoinDestroed;

        if (gameObject.activeSelf)
            StartCoroutine(WaitForSpawn());
    }

    private void Start()
    {
        wait = new WaitForSeconds(_spawnTime);
        Spawn();
    }

    private void Spawn()
    {
        Coin coin = Instantiate(_prefab, transform.position, transform.rotation, transform);
        coin.ObjectDestroyed += OnCoinDestroed;
    }

    private IEnumerator WaitForSpawn()
    {
        yield return wait;
        Spawn();
    }
}
