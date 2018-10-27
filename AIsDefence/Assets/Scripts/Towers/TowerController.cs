using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _bomb;

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
            Vector3 vect = new Vector3(transform.position.x, obj.transform.position.y, transform.position.z);

            GameObject inst = this._bullet;
            int force = 2500;
            if (this.gameObject.name.Contains("Red Tower"))
            {
                force = 1500;
                inst = this._bomb;
            }

            GameObject firedBullet = Instantiate(inst, vect, Quaternion.identity) as GameObject;

            firedBullet.transform.LookAt(obj.transform);

            firedBullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * force);
        }
    }
}
