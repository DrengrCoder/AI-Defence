using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            if (this.gameObject.name.Contains("Bullet"))
            {
                obj.gameObject.SetActive(false);
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
                        col.gameObject.SetActive(false);
                    }
                }
            }

            Destroy(this.gameObject);
        }
    }
}
