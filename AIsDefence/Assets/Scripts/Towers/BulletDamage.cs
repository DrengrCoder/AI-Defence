using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {

    private int _damage = 0;

    [SerializeField]
    private EndGameStats _stats;

    public int _tVal = 0;

    public int BulletDamageValue
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    private void OnEnable()
    {
        if (_tVal != 0)
        {
            _stats.TowerStats[_tVal - 1].Shots = _stats.TowerStats[_tVal - 1].Shots + 1;
        }
        Invoke("DestroyBullet", 2f);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            _stats.TowerStats[_tVal - 1].Hits = _stats.TowerStats[_tVal - 1].Hits + 1;
            _stats.TowerStats[_tVal - 1].Damage = _stats.TowerStats[_tVal - 1].Damage + _damage;
            bool killed = false;

            if (obj.gameObject.GetComponent<Enemy>())
            {
                killed = obj.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            }

            if (killed == true)//Aneurin Addition
            {
                _stats.TowerStats[_tVal - 1].Kills = _stats.TowerStats[_tVal - 1].Kills + 1;
            }

            this.DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
