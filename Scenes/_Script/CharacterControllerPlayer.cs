using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterControllerPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator; 

    private void Start()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        Move(KeyCode.A, KeyCode.D);
    }

    private void Move(KeyCode keyCodeA, KeyCode keyCodeD)
    {
        if (Input.GetKey(keyCodeA))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            EnableAnimation();
            _spriteRenderer.flipX = true;
        }
        else
        {
            DisableAnimation();
        }

        if (Input.GetKey(keyCodeD))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            EnableAnimation();
            _spriteRenderer.flipX = false;
        }
        else
        {
            DisableAnimation();
        }
    }

    private void EnableAnimation()
    {
        _animator.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
    }

    private void DisableAnimation()
    {
        _animator.SetFloat("Speed", 0.0f, 0.2f, Time.deltaTime);
    }
}
