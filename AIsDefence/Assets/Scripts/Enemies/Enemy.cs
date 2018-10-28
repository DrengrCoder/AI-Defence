using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public float Speed;
    public string Name;
    public int Worth;

    [SerializeField]
    private int _maxHealth;

    public int CreditsOnDeath;
    public int Health;
    public int Damage;
    public float DistanceToEnd;
    public GameObject EnemyTarget;

    private float _height;

    private void Start()
    {
        _height = transform.position.y;
        Health = _maxHealth;
    }

    private void OnEnable()
    {
        _height = transform.position.y;
        Health = _maxHealth;
    }

    private void OnDisable()
    {
        //Move to Death() when turrets deal health damage
        CreditBanks Bank = FindObjectOfType<CreditBanks>();
        Bank.AddCredits(CreditsOnDeath);
    }

    public void TakeDamage(int damage)
    {
        Health = Health - damage;

        if (Health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("I be dead");
        gameObject.SetActive(false);
    }

    public void Move()
    {
        float step = Speed * Time.deltaTime;
        Vector3 newposition = Vector3.MoveTowards(transform.position, EnemyTarget.transform.position, step);
        newposition.y = _height;
        transform.position = newposition;

        DistanceToEnd = Vector3.Distance(transform.position, EnemyTarget.transform.position);
    }

    private void Update()
    {
        Move();
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }

}
