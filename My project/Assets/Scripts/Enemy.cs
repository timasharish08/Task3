using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;

    private Vector3[] _positions;
    private int _currentPosition;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _positions = GetComponentsInChildren<Transform>().Select(child => child.position).ToArray();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnMoveEnd()
    {
        _currentPosition = ++_currentPosition % _positions.Length;
        StartCoroutine(MoveToPosition());
    }

    private IEnumerator MoveToPosition()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        while (transform.position != _positions[_currentPosition])
        {
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, _positions[_currentPosition], _speed * Time.deltaTime);
            yield return wait;
        }

        yield return new WaitForSeconds(_waitTime);
        OnMoveEnd();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            player.TakeDamage();
    }
}
