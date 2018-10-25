using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField]
    private GameObject _bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Enemy")
        {
            GameObject firedBullet = Instantiate(this._bullet, transform.position, Quaternion.identity) as GameObject;

            firedBullet.transform.LookAt(obj.transform);

            firedBullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 2500);
        }
    }
}
