using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            _enemy.AttackPlayer(player);
    }
}
