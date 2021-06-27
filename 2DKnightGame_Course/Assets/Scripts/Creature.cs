using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creature : MonoBehaviour, IDestructable
{
    protected Animator _animator;
    protected Rigidbody2D _rb;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _health = 100;
    public float Health
    { 
        get { return _health; } 
        set { _health = value; } 
    }
    public float Damage 
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public float Speed 
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private void Awake()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _rb = gameObject.GetComponentInChildren<Rigidbody2D>();
    }

    public void Die()
    {
        GameController.Instance.Killed(this);
    }

    public void RecieveHit(float damage)
    {
        Health -= damage;
        GameController.Instance.Hit(this);
        if (Health <= 0)
        {
            Die();
        }
    }

    protected void DoHit(Vector3 hitPosition, float hitRadius, float hitDamage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitRadius);

        for (int i = 0; i < hits.Length; i++)
        {
            if (!GameObject.Equals(hits[i].gameObject, gameObject))
            {
                IDestructable destructable = hits[i].gameObject.GetComponent<IDestructable>();

                if (destructable != null)
                {
                    destructable.RecieveHit(hitDamage);
                }
            }
        }
    }

}
