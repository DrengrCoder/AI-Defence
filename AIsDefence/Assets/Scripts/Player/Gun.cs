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

    [SerializeField]
    private float _fowardZ = 0.0f;
    [SerializeField]
    private float _backZ = 0.0f;
    private float _currentZ = 0.0f;

    private void Start()
    {
        _currentZ = _fowardZ;
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
                _currentZ = _currentZ - 0.1f;
                if (_currentZ < _backZ)
                {
                    _currentZ = _backZ;
                }
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _currentZ);
            }
        }
        else
        {
            _currentZ = _currentZ + (0.1f * Time.deltaTime);
            if (_currentZ > _fowardZ)
            {
                _currentZ = _fowardZ;
            }
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _currentZ);
                _nextFire = _nextFire + Time.deltaTime;
        }
    }

}
