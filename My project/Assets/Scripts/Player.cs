using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _invulnerabilityTime;

    private int _health;
    private bool _canTakeDamage;
    private int _coinsCount;

    private void Awake()
    {
        _health = _maxHealth;
        _canTakeDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            TakeCoin(coin);
        else if (collision.TryGetComponent(out MedicineKit medicine))
            TakeMedicineKit(medicine);
    }

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage == false)
            return;

        _health -= damage;
        StartCoroutine(InvulnerabilityTimer());

        if (_health <= 0)
            Destroy(gameObject);
    }

    private void TakeCoin(Coin coin)
    {
        _coinsCount++;
        Destroy(coin.gameObject);
    }

    private void TakeMedicineKit(MedicineKit medicine)
    {
        _health = Mathf.Clamp(_health + 1, 0, _maxHealth);
        Destroy(medicine.gameObject);
    }

    private IEnumerator InvulnerabilityTimer()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(_invulnerabilityTime);
        _canTakeDamage = true;
    }
}
