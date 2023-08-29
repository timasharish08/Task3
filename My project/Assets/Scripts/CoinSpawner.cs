using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _spawnTime;

    public void OnCoinDestroed()
    {
        if (gameObject.activeSelf)
            StartCoroutine(WaitForSpawn());
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Coin coin = Instantiate(_coinPrefab, transform.position, transform.rotation, transform);
        coin.Init(this);
    }

    private IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(_spawnTime);
        Spawn();
    }
}
