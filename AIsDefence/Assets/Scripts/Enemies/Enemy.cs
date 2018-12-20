using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour{

    //public float Speed; replaced by navmesh
    public string Name;

    public int _maxHealth;

    public int CreditsOnDeath;
    public int Health;
    public int Damage;
    public float DistanceToEnd;
    public GameObject EnemyTarget;
    public float AttackCooldown = 2.0f;

    private float _height;
    [SerializeField]
    private float _timeTillAttack = 0.0f;
    public bool CanAttack = true;

    public GameObject FaceObjective;

    private int _waveBelongTo = 100;
    [SerializeField]
    private bool _boss = false;
    [SerializeField]
    private EndGameStats _stats;

    [SerializeField]
    private Material _originalMaterial;
    [SerializeField]
    private Material _whiteMaterial;
    [SerializeField]
    private Material _redMaterial;
    [SerializeField]
    private GameObject[] _bodyParts;

    public AudioSource AttackSound;//Scales to sfx * master

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
        FaceObjective = EnemyTarget;
        _timeTillAttack = 0.0f;
        CanAttack = true;
    }

    private void OnDisable()
    {
        //Move to Death() when turrets deal health damage
        CreditBanks Bank = FindObjectOfType<CreditBanks>();

        if (Bank != null)
        {
            Bank.AddCredits(CreditsOnDeath);
        }

        _waveBelongTo = 100;
    }

    public void SetWaveNum(int i)
    {
        _waveBelongTo = i;

        if (_boss == false)
        {
            _stats.WaveStats[_waveBelongTo].NumEnemies = _stats.WaveStats[_waveBelongTo].NumEnemies + 1;
        }
        else
        {
            _stats.WaveStats[_waveBelongTo].Bosses = _stats.WaveStats[_waveBelongTo].Bosses + 1;
        }
    }

    public bool TakeDamage(int damage)//returns true for killed, false for damaged
    {
        Health = Health - damage;

        if (Health <= 0)
        {
            Death();
            return true;
        }

        StartCoroutine(GotHit());

        return false;
    }

    private IEnumerator GotHit()
    {
        for (int i = 0; i < _bodyParts.Length; i++)
        {
            _bodyParts[i].GetComponent<Renderer>().material = _redMaterial;
        }

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < _bodyParts.Length; i++)
        {
            _bodyParts[i].GetComponent<Renderer>().material = _whiteMaterial;
        }

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < _bodyParts.Length; i++)
        {
            _bodyParts[i].GetComponent<Renderer>().material = _originalMaterial;
        }
    }

    public void Death()
    {
        if (_boss == false)
        {
            _stats.WaveStats[_waveBelongTo].EnemiesKilled = _stats.WaveStats[_waveBelongTo].EnemiesKilled + 1;
        }
        else
        {
            _stats.WaveStats[_waveBelongTo].BossesKilled = _stats.WaveStats[_waveBelongTo].BossesKilled + 1;
        }

        gameObject.SetActive(false);
    }

    public void Move()
    {
        DistanceToEnd = Vector3.Distance(transform.position, EnemyTarget.transform.position);
    }

    private void Update()
    {
        Move();
        FaceTarget(FaceObjective);

        if (CanAttack == false)
        {
            _timeTillAttack = _timeTillAttack + Time.deltaTime;

            if (_timeTillAttack >= AttackCooldown)
            {
                CanAttack = true;
                _timeTillAttack = 0.0f;
            }
        }
    }

    public void FaceTarget(GameObject enemyTarget)
    {
        Vector3 lookPos = enemyTarget.transform.position - transform.position;
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
