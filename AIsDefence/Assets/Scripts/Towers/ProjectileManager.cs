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

    public void FireSpreadProjectile(GameObject tower, Collider target, GameObject projectilePrefab, int force, int damage)
    {
        Vector3 startPosition = new Vector3(tower.transform.position.x, target.transform.position.y, tower.transform.position.z);

        List<GameObject> projectiles = new List<GameObject>();

        int totalCount = 0;
        for (int i = 0; i < this._pool.Count; i++)
        {
            if (_pool[i].name.Contains(projectilePrefab.name) && !_pool[i].activeInHierarchy)
            {
                projectiles.Add(_pool[i]);
                if (++totalCount >= 5)
                {
                    break;
                }
            }
        }

        projectiles[0].transform.position = startPosition;
        projectiles[0].transform.LookAt(target.transform);
        projectiles[0].transform.rotation *= Quaternion.Euler(0, -18, 0);

        for (int i = 0; i < projectiles.Count; i++) 
        {
            projectiles[i].GetComponent<BulletDamage>().BulletDamageValue = damage;

            if (i != 0)
            {
                projectiles[i].transform.position = startPosition;
                projectiles[i].transform.rotation = projectiles[0].transform.rotation * Quaternion.Euler(0, i * 9, 0);
            }

            projectiles[i].SetActive(true);
            projectiles[i].GetComponent<Rigidbody>().AddRelativeForce(transform.forward * force);
        }
    }
}
