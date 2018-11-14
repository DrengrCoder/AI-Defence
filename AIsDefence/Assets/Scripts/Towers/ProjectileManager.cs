using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _prefabProjectiles;

    [SerializeField]
    private List<GameObject> _pool;

    private int _poolMax = 10;

	// Use this for initialization
	void Start () {

		foreach (GameObject obj in _prefabProjectiles)
        {
            for (int i = 0; i < _poolMax; i++)
            {
                GameObject temp = Instantiate(obj);
                temp.SetActive(false);
                _pool.Add(temp);
            }
        }

	}
	
	public void FireProjectile(GameObject tower, Collider target, GameObject projectilePrefab, int force, int damage)
    {
        Vector3 position = new Vector3(tower.transform.position.x, target.transform.position.y, tower.transform.position.z);
        
        for (int i = 0; i < this._pool.Count; i++)
        {
            if (_pool[i].name.Contains(projectilePrefab.name) && !_pool[i].activeInHierarchy)
            {
                GameObject firedProjectile = _pool[i];
                firedProjectile.GetComponent<BulletDamage>().BulletDamageValue = damage;
                firedProjectile.transform.position = position;
                firedProjectile.transform.LookAt(target.transform);
                firedProjectile.SetActive(true);
                firedProjectile.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * force);
                break;
            }
        }
    }
}
