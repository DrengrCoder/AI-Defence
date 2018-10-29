using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    enum AttackChoice { First, Last, Strongest, Weakest};

    private int _health = 100;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _bomb;
    [SerializeField]
    private int _cost;
  
    private ProjectileManager _projectileManager;
    private EditTowerMenu _towerEditMenu;

    private List<GameObject> _inRangeEnemies = new List<GameObject>();
    private GameObject _currentTarget;
    private AttackChoice _aiAttackOption = AttackChoice.First;

    private bool _canAttack = true;
    private float _attackCooldown = 0.5f;
    private float _waitTime = 0.0f;

    public void SetAttackOption(string option)
    {
        switch (option)
        {
            case "First":
                _aiAttackOption = AttackChoice.First;
                break;
            case "Last":
                _aiAttackOption = AttackChoice.Last;
                break;
            case "Strongest":
                _aiAttackOption = AttackChoice.Strongest;
                break;
            case "Weakest":
                _aiAttackOption = AttackChoice.Weakest;
                break;
            default:
                break;
        }

        AllocateNewTarget();
    }
    
    private void OnEnable()
    {
        CreditBanks Bank = FindObjectOfType<CreditBanks>();
        Bank.MinusCredits(_cost);
    }
    
    void Start () {
        _projectileManager = GameObject.Find("ProjectileManager").GetComponent<ProjectileManager>();
        _towerEditMenu = GameObject.Find("UI/Menus").GetComponent<EditTowerMenu>();
        _towerEditMenu.AddTower(this.gameObject);
    }

    void Update()
    {
        if (!_canAttack)
        {
            _waitTime = _waitTime + Time.deltaTime;

            if (_waitTime >= _attackCooldown)
            {
                _waitTime = 0.0f;
                _canAttack = true;
            }
        }
    }

    private void AllocateNewTarget()
    {
        switch (this._aiAttackOption)
        {
            case AttackChoice.First:
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().DistanceToEnd.CompareTo( e2.GetComponent<Enemy>().DistanceToEnd ));
                break;
            case AttackChoice.Last:
                _inRangeEnemies.Sort((e1, e2) => -1* e1.GetComponent<Enemy>().DistanceToEnd.CompareTo( e2.GetComponent<Enemy>().DistanceToEnd ));
                break;
            case AttackChoice.Strongest:
                _inRangeEnemies.Sort((e1, e2) => -1* e1.GetComponent<Enemy>().Health.CompareTo( e2.GetComponent<Enemy>().Health ));
                break;
            case AttackChoice.Weakest:
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().Health.CompareTo(e2.GetComponent<Enemy>().Health));
                break;
            default:
                break;
        }

        this._currentTarget = _inRangeEnemies[0];
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            this._inRangeEnemies.Add(obj.gameObject);
            AllocateNewTarget();
        }
    }

    private void OnTriggerStay(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy" && _canAttack)
        {
            Attack();
            _canAttack = false;
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            this._inRangeEnemies.Remove(obj.gameObject);
            if (obj == _currentTarget)
            {
                AllocateNewTarget();
            }
        }
    }

    private void Attack()
    {
        GameObject prefabProjectile = this._bullet;
        int force = 2500;

        if (this.gameObject.name.Contains("Red Tower"))
        {
            force = 1500;
            prefabProjectile = this._bomb;
        }

        _projectileManager.FireProjectile(this.gameObject, _currentTarget.GetComponent<CapsuleCollider>(), prefabProjectile, force);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}

