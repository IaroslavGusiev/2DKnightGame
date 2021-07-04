using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knight : Creature
{
    [SerializeField] private float _stairsSpeed;
    private bool _onGround = true;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    private bool onStairs = false;
    [SerializeField] private Transform _atackPoint;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _hitDelay;
    public bool OnStairs
    {
        get { return onStairs; }
        set
        {
            if (value == true)
            {
                _rb.gravityScale = 0;
            }
            else
            {
                _rb.gravityScale = 1;
            }
            onStairs = value;
        }
    }

    private void Start()
    {
        GameController.Instance.Knight = this;
        GameController.Instance.OnUpdateHeroParameters += HandleOnUpdateHeroParameters;
    }

    void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        Movement();
        _animator.SetBool("Jump", !_onGround);
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Atack");
            Invoke("Attack", _hitDelay);
        }
        if (Input.GetButtonDown("Jump") && _onGround)
        {
            _rb.AddForce(Vector2.up * _jumpForce);
        }
        _onGround = GroundCheck();
        if (onStairs == true)
        {
            Vector2 velocity = _rb.velocity;
            velocity.y = Input.GetAxis("Vertical") * _stairsSpeed;
            _rb.velocity = velocity;
        }
    }

    private void Movement() 
    {
        Vector2 velocity = _rb.velocity;
        velocity.x = Input.GetAxis("Horizontal") * _speed;
        _rb.velocity = velocity;
        if (transform.localScale.x < 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = Vector3.one;
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }
    }

    private bool GroundCheck() 
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, _groundCheck.position);
        for (int i = 0; i < hits.Length; i++)
        {
            if (!GameObject.Equals(hits[i].collider.gameObject, gameObject))
            {
            return true;
            }
        }
        return false;
    }

    private void Attack() 
    {
        DoHit(_atackPoint.position, _attackRadius, Damage);
    }

    private void HandleOnUpdateHeroParameters(HeroParameters parameters) 
    {
        Health = parameters.MaxHealth;
        Damage = parameters.Damage;
        Speed = parameters.Speed;
    }

    private void OnDisable()
    {
        GameController.Instance.OnUpdateHeroParameters -= HandleOnUpdateHeroParameters;
    }
}
