using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    public event UnityAction<Vector2> Move;
    public event UnityAction Stopped;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded;

    private void Awake()
    {
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
        else
            Stopped?.Invoke();
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

    private void Run()
    {
        Vector2 newPosition = transform.right * Input.GetAxis("Horizontal");
        _rigidbody.position += newPosition * _speed * Time.deltaTime;

        if (Move != null)
            Move.Invoke(newPosition);
    }
}
