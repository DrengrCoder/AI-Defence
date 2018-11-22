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
            if (this.gameObject.name.Contains("Bullet"))
            {
                _stats.TowerStats[_tVal - 1].Hits = _stats.TowerStats[_tVal - 1].Hits + 1;
                _stats.TowerStats[_tVal - 1].Damage = _stats.TowerStats[_tVal - 1].Damage + _damage;
                bool killed = false;
                switch (obj.gameObject.name)
                {
                    case "TempEnemy(Clone)":
                        killed = obj.gameObject.GetComponent<SuicideEnemy>().TakeDamage(_damage);
                        break;
                    case "RangedEnemy(Clone)":
                        killed = obj.gameObject.GetComponent<RangedEnemy>().TakeDamage(_damage);
                        break;
                    case "MeleeEnemy(Clone)":
                        killed = obj.gameObject.GetComponent<MeleeEnemy>().TakeDamage(_damage);
                        break;
                    default:
                        break;
                }
                if (killed == true)//Aneurin Addition
                {
                    _stats.TowerStats[_tVal - 1].Kills = _stats.TowerStats[_tVal - 1].Kills + 1;
                }
            }
            else if (this.gameObject.name.Contains("Bomb"))
            {
                Transform bombChild = this.gameObject.transform.GetChild(0);
                
                Vector3 vect = new Vector3(bombChild.position.x, bombChild.position.y, bombChild.position.z);

                Collider[] detectedColliders = Physics.OverlapSphere(vect, 0.1f);

                foreach (Collider col in detectedColliders)
                {
                    if (col.gameObject.tag == "Enemy")
                    {
                        _stats.TowerStats[_tVal - 1].Hits = _stats.TowerStats[_tVal - 1].Hits + 1;
                        _stats.TowerStats[_tVal - 1].Damage = _stats.TowerStats[_tVal - 1].Damage + _damage;
                        bool killed = false;
                        switch (col.gameObject.name)
                        {
                            case "TempEnemy(Clone)":
                                killed = col.gameObject.GetComponent<SuicideEnemy>().TakeDamage(_damage);
                                break;
                            case "RangedEnemy(Clone)":
                                killed = col.gameObject.GetComponent<RangedEnemy>().TakeDamage(_damage);
                                break;
                            case "MeleeEnemy(Clone)":
                                killed = col.gameObject.GetComponent<MeleeEnemy>().TakeDamage(_damage);
                                break;
                            default:
                                break;
                        }
                        if (killed == true)//Aneurin Addition
                        {
                            _stats.TowerStats[_tVal - 1].Kills = _stats.TowerStats[_tVal - 1].Kills + 1;
                        }
                    }
                }
                //
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
