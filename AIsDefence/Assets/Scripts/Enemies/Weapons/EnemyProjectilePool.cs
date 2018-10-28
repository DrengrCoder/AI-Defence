using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilePool : MonoBehaviour {

    [SerializeField]
    private int _numBullets;
    [SerializeField]
    private GameObject _bulletTemplate;

    private List<GameObject> _bullets;

    private void Start()
    {
        _bullets = new List<GameObject>();

        for (int i = 0; i < _numBullets; i++)
        {
            GameObject temp = Instantiate(_bulletTemplate);
            _bullets.Add(temp);
        }
    }

    public GameObject GetBullet(int damage)
    {
        GameObject bulletReturn = null;

        for(int i = 0; i < _bullets.Count; i++)
        {
            if (_bullets[i].activeSelf == false)
            {
                bulletReturn = _bullets[i];
                bulletReturn.GetComponent<RangedEnemyProjectile>().Damage = damage;
                return bulletReturn;
            }
        }

        return bulletReturn;
    }
}
