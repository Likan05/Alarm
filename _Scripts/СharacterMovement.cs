using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ÑharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private int _moveRihght;
    private int _moveLeft;
    private const string Speed = nameof(Speed);

    private void Awake()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _moveRihght = 1;
        _moveLeft = -1;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            ChangeDirection(_moveLeft);
        else if (Input.GetKey(KeyCode.D))
            ChangeDirection(_moveRihght);
        else
            DisableAnimation();
    }

    private void ChangeDirection(int value)
    {
        transform.Translate(_speed * Time.deltaTime * value, 0, 0);
        EnableAnimation();
        if (value < 0)        
            _spriteRenderer.flipX = true;
        else if (value > 0)
            _spriteRenderer.flipX = false;

    }

    private void EnableAnimation()
    {
        _animator.SetFloat(Speed, 0.5f, 0.1f, Time.deltaTime);
    }

    private void DisableAnimation()
    {
        _animator.SetFloat(Speed, 0.0f, 0.1f, Time.deltaTime);
    }
}
