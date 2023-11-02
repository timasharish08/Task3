using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(PlayerMover))]
public class PlayerViev : MonoBehaviour
{
    public readonly int IsRun = Animator.StringToHash(nameof(IsRun));

    private SpriteRenderer _renderer;
    private Animator _animator;
    private PlayerMover _mover;

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
        _mover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _mover.Move += OnPlayerMove;
        _mover.Stopped += OnPlayerStopped;
    }

    private void OnDisable()
    {
        _mover.Move -= OnPlayerMove;
        _mover.Stopped -= OnPlayerStopped;
    }
}
