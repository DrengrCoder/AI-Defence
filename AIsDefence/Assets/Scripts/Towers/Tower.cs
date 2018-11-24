using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Tower : MonoBehaviour {

    //stats / properties
    public int _baseHealth = 100;
    private int _maxHealth = 100;
    private int _health = 100;
    [SerializeField]
    private int _cost;

    //This is for the end game stats, should only be edited by InstantiateObjectOnclick.cs
    public int Num = 0;

    //upgrade properties
    public /*const*/ int _baseUpgradeCost = 100;//cant be accessed if its a const variable
    public int _upgradeCost = 100;
    public int[] _futureCosts;
    public /*const*/ int _maxUpgrades = 5;
    public int _upgradePointer = 0;
    
    //firing mechanics variables
    [HideInInspector]
    public bool _canFire = true;
    private float _currentWaitTime = 0.0f;
    private bool _resettingDelay = false;
    private float _resetTime = 0.0f;
    //firing mechanics object references
    private AttackChoice _attackParameters = AttackChoice.First;
    [HideInInspector]
    public GameObject _currentTarget;
    [HideInInspector]
    public List<GameObject> _inRangeEnemies = new List<GameObject>();

    //object references
    [HideInInspector]
    public ProjectileManager _projectileManager;
    private EditTowerMenu _towerEditMenu;

    //System
    private void OnEnable()
    {
        CreditBanks Bank = FindObjectOfType<CreditBanks>();
        Bank.MinusCredits(_cost);
    }
    void Start ()
    {
        _projectileManager = GameObject.Find("ProjectileManager").GetComponent<ProjectileManager>();
        _towerEditMenu = GameObject.Find("UI/Menus").GetComponent<EditTowerMenu>();
        _towerEditMenu.AddTower(this.gameObject);
        _towerEditMenu.UpdateTowers();
    }
	void Update ()
    {
        if (_currentTarget != null && !this._currentTarget.activeInHierarchy && this._inRangeEnemies.Count > 0)
        {
            AllocateNewTarget();
        }

        if (!_canFire)
        {
            _currentWaitTime = _currentWaitTime + Time.deltaTime;

            if (_currentWaitTime >= GetTowerFireRate())
            {
                if (_resettingDelay)//currently only reset for burst tower
                {
                    _resettingDelay = false;
                    SetTowerFireRate(_resetTime);
                }

                _currentWaitTime = 0.0f;
                _canFire = true;
            }
        }

        if (_currentTarget != null)
        {
            transform.LookAt(new Vector3(_currentTarget.transform.position.x, 0, _currentTarget.transform.position.z));
        }
    }
    void FixedUpdate()
    {
        AllocateNewTarget();
    }

    //Targeting
    public AttackChoice TargetParameters
    {
        set
        {
            _attackParameters = value;
        }
        get
        {
            return _attackParameters;
        }
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

        switch (this._attackParameters)
        {
            case AttackChoice.First:
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().DistanceToEnd.CompareTo(e2.GetComponent<Enemy>().DistanceToEnd));
                break;
            case AttackChoice.Last:
                _inRangeEnemies.Sort((e1, e2) => -1 * e1.GetComponent<Enemy>().DistanceToEnd.CompareTo(e2.GetComponent<Enemy>().DistanceToEnd));
                break;
            case AttackChoice.MostHealth:
                _inRangeEnemies.Sort((e1, e2) => -1 * e1.GetComponent<Enemy>().Health.CompareTo(e2.GetComponent<Enemy>().Health));
                break;
            case AttackChoice.LeastHealth:
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().Health.CompareTo(e2.GetComponent<Enemy>().Health));
                break;
            case AttackChoice.MostDamage:
                _inRangeEnemies.Sort((e1, e2) => -1 * e1.GetComponent<Enemy>().Health.CompareTo(e2.GetComponent<Enemy>().Damage));
                break;
            case AttackChoice.LeastDamage:
                _inRangeEnemies.Sort((e1, e2) => e1.GetComponent<Enemy>().Health.CompareTo(e2.GetComponent<Enemy>().Damage));
                break;
            default:
                break;
        }

        if (_inRangeEnemies.Count > 0)
        {
            this._currentTarget = _inRangeEnemies[0];
        }
    }

    //Firing reset (for burst fire)
    public bool ResettingDelay
    {
        set
        {
            _resettingDelay = value;
        }
    }
    public float ResetDelayTo
    {
        set
        {
            _resetTime = value;
        }
    }

    //Update / Return variables
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _towerEditMenu.RemoveTower(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    public int TowerHealth
    {
        set
        {
            _maxHealth = value;
            _health = value;
        }
        get
        {
            return _maxHealth;
        }
    }

    public abstract void SetTowerFireRate(float rate);
    public abstract void SetTowerDamage(int damage);
    public abstract float GetTowerFireRate();
    public abstract int GetTowerDamage();
    public abstract TowerType GetTowerType();

    public abstract int BaseDamage();
    public abstract float BaseFireRate();
    public abstract int BaseRange();

}

public static class AttackChoiceUtils
{
    public static string GetDescription(AttackChoice option)
    {
        switch (option)
        {
            case AttackChoice.First:
                return "Enemy Closest to base";
            case AttackChoice.Last:
                return "Enemy Furthest from base";
            case AttackChoice.MostHealth:
                return "Enemy with Most Health";
            case AttackChoice.LeastHealth:
                return "Enemy with Least Health";
            case AttackChoice.MostDamage:
                return "Enemy with Most Damage";
            case AttackChoice.LeastDamage:
                return "Enemy with Least Damage";
            default:
                return "";
        }
    }

    public static string GetBaseText(AttackChoice option)
    {
        switch (option)
        {
            case AttackChoice.First:
                return "First";
            case AttackChoice.Last:
                return "Last";
            case AttackChoice.MostHealth:
                return "Most-Health";
            case AttackChoice.LeastHealth:
                return "Least-Health";
            case AttackChoice.MostDamage:
                return "Most-Damage";
            case AttackChoice.LeastDamage:
                return "Least-Damage";
            default:
                return "";
        }
    }
}

public enum AttackChoice
{
    First,
    Last,
    MostHealth,
    LeastHealth,
    MostDamage,
    LeastDamage
}

public enum TowerType
{
    None,
    SingleFire,
    BurstFire,
    SpreadFire,
    AoeFire,
    PulseFire
}