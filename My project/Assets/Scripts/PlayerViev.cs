using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(Player))]
public class PlayerViev : MonoBehaviour
{
    public readonly int IsRun = Animator.StringToHash(nameof(IsRun));

    private SpriteRenderer _renderer;
    private Animator _animator;
    private Player _player;

    public void OnPlayerMove(Vector2 moving)
    {
        _animator.SetBool(IsRun, true);
        _renderer.flipX = moving.x < 0;
    }

    public void OnPlayerStopped()
    {
        _animator.SetBool(IsRun, false);
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.ObjectMove += OnPlayerMove;
        _player.ObjectStopped += OnPlayerStopped;
    }

    private void OnDisable()
    {
        _player.ObjectMove -= OnPlayerMove;
        _player.ObjectStopped -= OnPlayerStopped;
    }
}
