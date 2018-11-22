using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour {

    private int _damage = 0;

    private bool _inMotion = false;


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
                if (col.gameObject.tag == "Enemy")
                {
                    col.GetComponent<Enemy>().TakeDamage(_damage);
                }
            }

            DestroyBullet();
        }
    }

    
    private void DestroyBullet()
    {
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
