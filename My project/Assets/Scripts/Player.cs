using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public event UnityAction<Vector2> ObjectMove;
    public event UnityAction ObjectStopped;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private int _maxHealth;
    [SerializeField] private float _invulnerabilityTime;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded;

    [SerializeField] private int _health;
    private bool _canTakeDamage;
    private int _coinsCount;

    private void Awake()
    {
        _health = _maxHealth;
        _canTakeDamage = true;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isGrounded && Input.GetKey(KeyCode.W))
        {
            _rigidbody.velocity = transform.up * _jumpForce;
            _isGrounded = false;
        }

        if (Input.GetButton("Horizontal"))
            Run();
        else if (ObjectStopped != null)
            ObjectStopped.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ground ground))
            _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ground ground))
            _isGrounded = false;
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

    private void Run()
    {
        Vector2 newPosition = transform.right * Input.GetAxis("Horizontal");
        _rigidbody.position += newPosition * _speed * Time.deltaTime;

        if (ObjectMove != null)
            ObjectMove.Invoke(newPosition);
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
