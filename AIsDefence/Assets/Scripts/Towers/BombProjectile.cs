using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour {

    private int _damage = 0;

    private bool _inMotion = false;

    [SerializeField]
    private EndGameStats _stats;

    public int _tVal = 0;

    [SerializeField]
    private AudioClip _explosionAudio;
    [SerializeField]
    private ParticleSystem _explosionVisual;

    private void OnEnable()
    {
        if (_tVal != 0)
        {
            _stats.TowerStats[_tVal - 1].Shots = _stats.TowerStats[_tVal - 1].Shots + 1;
        }
    }

    public int BulletDamage
    {
        set
        {
            _damage = value;
        }
        get
        {
            return _damage;
        }
    }
    

    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger && (obj.gameObject.tag == "Enemy" || obj.gameObject.tag == "Ground"))
        {
            Transform child = this.gameObject.transform.GetChild(0);

            Collider[] detectedColliders = Physics.OverlapSphere(child.position, 0.1f);

            foreach (Collider col in detectedColliders)
            {
                bool killed = false; 

                if (col.gameObject.tag == "Enemy")
                {
                    _stats.TowerStats[_tVal - 1].Hits = _stats.TowerStats[_tVal - 1].Hits + 1;
                    _stats.TowerStats[_tVal - 1].Damage = _stats.TowerStats[_tVal - 1].Damage + _damage;

                    killed = col.GetComponent<Enemy>().TakeDamage(_damage);
                }

                if (killed == true)//Aneurin Addition
                {
                    _stats.TowerStats[_tVal - 1].Kills = _stats.TowerStats[_tVal - 1].Kills + 1;
                }
            }

            StartCoroutine(DestroyBullet());
        }
    }

    
    private IEnumerator DestroyBullet()
    {
        AudioSource.PlayClipAtPoint(_explosionAudio, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        _explosionVisual.Play();

        MeshRenderer child = GetComponentInChildren<MeshRenderer>();
        
        if (child != null)
            child.gameObject.SetActive(false);

        yield return new WaitForSeconds(3.0f);

        child.gameObject.SetActive(true);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
