﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {

    private void OnEnable()
    {
        Invoke("DestroyBullet", 2f);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            if (this.gameObject.name.Contains("Bullet"))
            {
                switch (obj.gameObject.name)
                {
                    case "TempEnemy(Clone)":
                        obj.gameObject.GetComponent<SuicideEnemy>().TakeDamage(5);
                        break;
                    case "RangedEnemy(Clone)":
                        obj.gameObject.GetComponent<RangedEnemy>().TakeDamage(5);
                        break;
                    case "MeleeEnemy(Clone)":
                        obj.gameObject.GetComponent<MeleeEnemy>().TakeDamage(5);
                        break;
                    default:
                        break;
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
                        switch (col.gameObject.name)
                        {
                            case "TempEnemy(Clone)":
                                col.gameObject.GetComponent<SuicideEnemy>().TakeDamage(50);
                                break;
                            case "RangedEnemy(Clone)":
                                col.gameObject.GetComponent<RangedEnemy>().TakeDamage(10);
                                break;
                            case "MeleeEnemy(Clone)":
                                col.gameObject.GetComponent<MeleeEnemy>().TakeDamage(10);
                                break;
                            default:
                                break;
                        }
                    }
                }
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
