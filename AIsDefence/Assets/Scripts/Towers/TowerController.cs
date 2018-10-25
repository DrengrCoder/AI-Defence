using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField]
    private GameObject _bullet;

    public int speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameObject firedBullet = Instantiate(this._bullet, transform.position, Quaternion.identity) as GameObject;
            firedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        }
    }
}
