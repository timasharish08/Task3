using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;

    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;

    private Vector3[] _positions;
    private int _currentPosition;

    private Coroutine _moving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _positions = _movePoints.Select(point => point.position).ToArray();
    }

    private void Start()
    {
        _moving = StartCoroutine(Moving(_positions[_currentPosition]));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            StopCoroutine(_moving);
            _moving = StartCoroutine(Moving(player.transform));
        }
    }

    public void AttackPlayer(Player player)
    {
        player.TakeDamage(_damage);
    }

    private void OnMoveEnd()
    {
        _currentPosition = ++_currentPosition % _movePoints.Length;
        _moving = StartCoroutine(Moving(_positions[_currentPosition]));
    }

    private IEnumerator Moving(Vector3 position)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (transform.position != position)
        {
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, position, _speed * Time.deltaTime);
            yield return wait;
        }

        yield return new WaitForSeconds(_waitTime);
        OnMoveEnd();
    }

    private IEnumerator Moving(Transform point)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (transform.position != point.position)
        {
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, point.position, _speed * Time.deltaTime);
            yield return wait;
        }

        yield return new WaitForSeconds(_waitTime);
        OnMoveEnd();
    }
}
