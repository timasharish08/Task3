using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _prefab;
    [SerializeField] private float _spawnTime;

    private WaitForSeconds wait;

    public void OnCoinDestroed(Item item)
    {
        item.ObjectDestroyed -= OnCoinDestroed;

        if (gameObject.activeSelf)
            StartCoroutine(Spawn());
    }

    private void Start()
    {
        wait = new WaitForSeconds(_spawnTime);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return wait;
        Item item = Instantiate(_prefab, transform.position, transform.rotation, transform);
        item.ObjectDestroyed += OnCoinDestroed;
    }
}
