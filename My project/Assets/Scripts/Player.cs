using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _invulnerabilityTime;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private bool _isGrounded;

    private int _health;
    private bool _canTakeDamage;
    private int _coinsCount;

    public void TakeCoin()
    {
        _coinsCount++;
    }

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
        {
            _animator.SetBool("IsRun", true);
            Run();
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }
    }

    private void Run()
    {
        Vector2 newPosition = transform.right * Input.GetAxis("Horizontal");

        _rigidbody.position += newPosition * _speed * Time.deltaTime;
        _renderer.flipX = newPosition.x < 0;
    }

    private void Start()
    {
        _canTakeDamage = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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
        {
            _isGrounded = true;
            print(1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            _isGrounded = false;
    }
}
