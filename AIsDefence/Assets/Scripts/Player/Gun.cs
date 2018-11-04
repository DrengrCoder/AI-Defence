using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private float _fireRate = 0.3f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private GameObject _bulletSpawn;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private int _numInPool = 20;
    private GameObject[] _bulletPool;

    private void Start()
    {
        _bulletPool = new GameObject[_numInPool];

        for (int i = 0; i < _numInPool; i++)
        {
            GameObject temp = Instantiate(_bullet);
            _bulletPool[i] = temp;
        }
    }

    private GameObject GetBullet()
    {
        for (int i = 0; i < _numInPool; i++)
        {
            if (_bulletPool[i].activeSelf == false)
            {
                return _bulletPool[i];
            }
        }

        return null;
    }

    private void Update()
    {
        if (_nextFire >= _fireRate)
        {
            if (Input.GetButton("Fire1") == true)
            {
                GameObject bullet = GetBullet();
                bullet.transform.position = _bulletSpawn.transform.position;
                bullet.transform.rotation = _bulletSpawn.transform.rotation;
                bullet.SetActive(true);
                _nextFire = 0.0f;
            }
        }
        else
        {
            _nextFire = _nextFire + Time.deltaTime;
        }
    }

}
