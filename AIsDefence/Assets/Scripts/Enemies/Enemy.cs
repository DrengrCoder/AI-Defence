using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour{

    //public float Speed; replaced by navmesh
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
        gameObject.GetComponent<NavMeshAgent>().SetDestination(EnemyTarget.transform.position);
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
