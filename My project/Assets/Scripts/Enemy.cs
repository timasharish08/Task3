using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;

    private EnemyMover _mover;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            _mover.MoveTo(player);
    }

    public void AttackPlayer(Player player)
    {
        player.TakeDamage(_damage);
    }
}
