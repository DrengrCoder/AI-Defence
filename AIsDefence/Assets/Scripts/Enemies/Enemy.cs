using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour{

    //public float Speed; replaced by navmesh
    public string Name;

    [SerializeField]
    private int _maxHealth;

    public int CreditsOnDeath;
    public int Health;
    public int Damage;
    public float DistanceToEnd;
    public GameObject EnemyTarget;
    public float AttackCooldown = 2.0f;

    private float _height;
    private float _timeTillAttack = 0.0f;
    public bool CanAttack = true;

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
        _timeTillAttack = 0.0f;
        CanAttack = true;
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
        gameObject.SetActive(false);
    }

    public void Move()
    {
        DistanceToEnd = Vector3.Distance(transform.position, EnemyTarget.transform.position);
    }

    private void Update()
    {
        Move();
        FaceTarget();

        if (CanAttack == false)
        {
            _timeTillAttack = _timeTillAttack + Time.deltaTime;

            if (_timeTillAttack >= AttackCooldown)
            {
                _timeTillAttack = 0.0f;
                CanAttack = true;
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 lookPos = EnemyTarget.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        float rotateSpeed = gameObject.GetComponent<NavMeshAgent>().angularSpeed;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }

}
