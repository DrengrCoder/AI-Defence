using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _bomb;

    private ProjectileManager _projectileManager;

	// Use this for initialization
	void Start () {
        _projectileManager = GameObject.Find("ProjectileManager").GetComponent<ProjectileManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
