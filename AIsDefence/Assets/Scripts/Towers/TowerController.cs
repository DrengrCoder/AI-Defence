using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    private int _health = 100;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _bomb;
    [SerializeField]
    private int _cost;
  
    private ProjectileManager _projectileManager;

    // Use this for initialization
    private void OnEnable()
    {
        CreditBanks Bank = FindObjectOfType<CreditBanks>();
        Bank.MinusCredits(_cost);
    }

    // Use this for initialization
    void Start () {
          _projectileManager = GameObject.Find("ProjectileManager").GetComponent<ProjectileManager>();
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            GameObject prefabProjectile = this._bullet;
            int force = 2500;

            if (this.gameObject.name.Contains("Red Tower"))
            {
                force = 1500;
                prefabProjectile = this._bomb;
            }

            _projectileManager.FireProjectile(this.gameObject, obj, prefabProjectile, force);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
