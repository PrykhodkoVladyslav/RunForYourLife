using UnityEngine;

public class ChasingController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform point;
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;
    private SpriteRotator _spriteRotator;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRotator = GetComponent<SpriteRotator>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!target)
        {
            _animator.SetBool(IsWalking, false);
            return;
        }

        Vector2 direction = (target.position - point.position).normalized;

        _rigidbody.MovePosition(_rigidbody.position + direction * (speed * Time.fixedDeltaTime));

        _spriteRotator.ByDirection(direction);

        _animator.SetBool(IsWalking, true);
    }
}