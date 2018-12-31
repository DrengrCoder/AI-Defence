using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour {

    //public GameObject _recoilComponent;
    //[HideInInspector]
    //public bool _recoiling = false;
    //[HideInInspector]
    //public float _recoilTime = 0f;
    //[HideInInspector]
    //public float _currentRecoilTime = 0f;
    //private bool _reversingRecoil = false;

    [SerializeField]
    private Slider _healthBar;

    public ParticleSystem _pulseEffect;
    [HideInInspector]
    public bool _emittingPulse = false;
    private int _scale = 0;

    public int _maxHealth = 100;
    [SerializeField]
    private int _health = 100;
    
    public int _cost;

    //This is for the end game stats, should only be edited by InstantiateObjectOnclick.cs
    public int Num = 0;

    //This is for radial menu, used with above Num variable. Denoates if radial wheel
    //is active over the current instantiated tower, only used in radial menu controller script.
    [HideInInspector]
    public bool MenuActiveOverThis = false;

    //upgrade properties
    [HideInInspector]
    public int _maxUpgrades = 5;

    [HideInInspector]
    public int _healthLevel = 0;
    public int[] _healthLevelCosts;

    [HideInInspector]
    public int _damageLevel = 0;
    public int[] _damageLevelCosts;

    [HideInInspector]
    public int _fireRateLevel = 0;
    public int[] _fireRateLevelCosts;

    [HideInInspector]
    public int _rangeLevel = 0;
    public int[] _rangeLevelCosts;
    
    //firing mechanics variables
    [HideInInspector]
    public bool _canFire = true;
    private float _currentWaitTime = 0.0f;
    //firing mechanics object references
    private AttackChoice _attackParameters = AttackChoice.First;
    [HideInInspector]
    public GameObject _currentTarget;
    [HideInInspector]
    public List<GameObject> _inRangeEnemies = new List<GameObject>();

    //object references
    [HideInInspector]
    public ProjectileManager _projectileManager;

    public AudioSource AttackSound;

    //System
    private void OnEnable()
    {
        CreditBanks Bank = FindObjectOfType<CreditBanks>();
        Bank.MinusCredits(_cost);
    }
    void Start ()
    {
        _projectileManager = GameObject.Find("ProjectileManager").GetComponent<ProjectileManager>();
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;
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

            if (_currentWaitTime >= GetTowerFireRate(true))
            {
                _currentWaitTime = 0.0f;
                _canFire = true;
            }
        }

        if (_currentTarget != null)
        {
            transform.LookAt(new Vector3(_currentTarget.transform.position.x, 0, _currentTarget.transform.position.z));
        }

        if (_emittingPulse)
        {
            _scale++;

            var sm = _pulseEffect.shape;
            sm.radius = _scale * (Time.deltaTime * 4);
            
            if (_scale >= GetTowerFireRate(false) * 60)//multiplying a constant so speed scales with firerate
            {
                _emittingPulse = false;

                sm.radius = 0.01f;
                _scale = 0;

                var em = _pulseEffect.emission;
                em.enabled = false;
            }
        }

        //if (_recoiling)
        //{
        //    _currentRecoilTime += Time.deltaTime;

        //    if (_reversingRecoil == false)
        //    {
        //        _recoilComponent.transform.localPosition = new Vector3(0, 0, 2);

        //        if (_currentRecoilTime >= _recoilTime / 2)
        //        {
        //            _reversingRecoil = true;
        //        }
        //    }
        //    else
        //    {
        //        _recoilComponent.transform.localPosition = new Vector3(0, 0, -2);

        //        if (_currentRecoilTime >= _recoilTime)
        //        {
        //            _reversingRecoil = false;
        //            _recoiling = false;
        //        }
        //    }
        //}
    }
    void FixedUpdate()
    {
        AllocateNewTarget();
        _healthBar.value = _health;
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
    
    //Update / Return variables
    public void Death()
    {
        Destroy(this.gameObject);
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            Death();
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
    public abstract float GetTowerFireRate(bool burstDelay);
    public abstract int GetTowerDamage();

    public abstract int GetDamageUpgrade();
    public abstract int GetHealthUpgrade();
    public abstract float GetFirerateUpgrade();
    public abstract int GetRadiusUpgrade();

    public abstract TowerType GetTowerType();

}

public static class AttackChoiceUtils
{
    public static string GetDescription(AttackChoice option)
    {
        switch (option)
        {
            case AttackChoice.First:
                return "The Enemy Closest to base";
            case AttackChoice.Last:
                return "The Enemy Furthest from base";
            case AttackChoice.MostHealth:
                return "The Enemy with the Most Health";
            case AttackChoice.LeastHealth:
                return "The Enemy with the Least Health";
            case AttackChoice.MostDamage:
                return "The Enemy with the Most Damage";
            case AttackChoice.LeastDamage:
                return "The Enemy with the Least Damage";
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