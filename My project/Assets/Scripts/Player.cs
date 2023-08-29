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
    [SerializeField] private float _invulnerabilityTime;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded;

    private int _health;
    private bool _canTakeDamage;
    private int _coinsCount;

    public void TakeDamage()
    {
        if (_canTakeDamage == false)
            return;

        _health--;
        InvulnerabilityTimer();

        if (_health <= 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
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

    private void Run()
    {
        Vector2 newPosition = transform.right * Input.GetAxis("Horizontal");
        _rigidbody.position += newPosition * _speed * Time.deltaTime;

        if (ObjectMove != null)
            ObjectMove.Invoke(newPosition);
    }

    private void Awake()
    {
        _canTakeDamage = true;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void TakeCoin(Coin coin)
    {
        _coinsCount++;
        Destroy(coin.gameObject);
    }

    private IEnumerator InvulnerabilityTimer()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(_invulnerabilityTime);
        _canTakeDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            _isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            TakeCoin(coin);
    }
}
