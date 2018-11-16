using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    public enum AttackChoice { First = 1, Last = 2, HighHealth = 3, LowHealth = 4, HighDamage = 5, LowDamage = 6 };
    public enum TowerType { None = 1, SingleFire = 2, BurstFire = 3, SpreadFire = 4, AoeFire = 5, PulseFire = 6 };

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
    private TowerType _towerType = TowerType.None;

    [HideInInspector]
    public bool _canAttack = true;
    private float _attackCooldown = 0.0f;
    private float _waitTime = 0.0f;
    
    private bool _resettingDelay = false;
    private float _resetDelayTo = 0.0f;

    public void SetAttackOption(int option)
    {
        _aiAttackOption = (AttackChoice)option;
        AllocateNewTarget();
    }

    public float AttackCooldown
    {
        set
        {
            this._attackCooldown = value;
        }
    }
    public bool ResettingDelay
    {
        set
        {
            _resettingDelay = value;
        }
    }
    public float DelayReset
    {
        set
        {
            _resetDelayTo = value;
        }
    }
    
    private void InitialiseTowerType()
    {
        switch (this.gameObject.name)
        {
            case "Single Fire Tower(Clone)":
                this._towerType = TowerType.SingleFire;
                break;
            case "Burst Fire Tower(Clone)":
                this._towerType = TowerType.BurstFire;
                break;
            case "Spread Fire Tower(Clone)":
                this._towerType = TowerType.SpreadFire;
                break;
            case "AOE Tower(Clone)":
                this._towerType = TowerType.AoeFire;
                break;
            case "Pulse Fire Tower(Clone)":
                this._towerType = TowerType.PulseFire;
                break;
            default:
                break;
        }
    }
    public int ReturnTowerType()
    {
        return (int)_towerType;
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
        InitialiseTowerType();
        _aiAttackOption = (AttackChoice)_towerEditMenu.CheckTargetParameters((int)_towerType);
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
                if (_resettingDelay)//currently only reset for burst tower
                {
                    _resettingDelay = false;
                    _attackCooldown = _resetDelayTo;
                }
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
            case AttackChoice.HighHealth:
                _inRangeEnemies.Sort((e1, e2) => -1* e1.GetComponent<Enemy>().Health.CompareTo( e2.GetComponent<Enemy>().Health ));
                break;
            case AttackChoice.LowHealth:
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().Health.CompareTo( e2.GetComponent<Enemy>().Health ));
                break;
            case AttackChoice.HighDamage:
                _inRangeEnemies.Sort((e1, e2) => -1* e1.GetComponent<Enemy>().Health.CompareTo( e2.GetComponent<Enemy>().Damage ));
                break;
            case AttackChoice.LowDamage:
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().Health.CompareTo( e2.GetComponent<Enemy>().Damage ));
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

