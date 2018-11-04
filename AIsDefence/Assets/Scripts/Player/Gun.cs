using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private float _fireRate;

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

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") == true)
        {
            //Shoot
        }
    }

}
