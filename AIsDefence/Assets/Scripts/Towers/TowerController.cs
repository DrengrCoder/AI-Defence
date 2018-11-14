using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    enum AttackChoice { First, Last, Strongest, Weakest};

    private int _health = 100;
    
    [SerializeField]
    private int _cost;
  
    [HideInInspector]
    public ProjectileManager _projectileManager;
    private EditTowerMenu _towerEditMenu;

    [HideInInspector]
    public List<GameObject> _inRangeEnemies = new List<GameObject>();
    [HideInInspector]
    public GameObject _currentTarget;
    private AttackChoice _aiAttackOption = AttackChoice.First;

    [HideInInspector]
    public bool _canAttack = true;
    private float _attackCooldown = 0.0f;
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

    public float AttackCooldown
    {
        set
        {
            this._attackCooldown = value;
        }
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
        if (this._currentTarget != null && !this._currentTarget.activeInHierarchy && this._inRangeEnemies.Count > 0)
        {
            AllocateNewTarget();
        }

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

    void FixedUpdate()
    {
        AllocateNewTarget();
    }

    private void ValidateEnemiesInRange()
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in this._inRangeEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (GameObject enemy in enemiesToRemove)
        {
            this._inRangeEnemies.Remove(enemy);
        }
    }

    public void AllocateNewTarget()
    {
        ValidateEnemiesInRange();

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
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().Health.CompareTo( e2.GetComponent<Enemy>().Health ));
                break;
            default:
                break;
        }

        if (_inRangeEnemies.Count > 0)
        {
            this._currentTarget = _inRangeEnemies[0];
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

