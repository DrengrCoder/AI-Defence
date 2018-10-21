using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _spawnableEnemies;
    [SerializeField]
    private GameObject _spawnPoint;

    [SerializeField]
    private Waves[] _waves;

    [SerializeField]
    private Pools[] _pools;

    public int Wave = 0;

    [SerializeField]
    private GameObject EnemyTarget;
    [SerializeField]
    private float _spawnDelay = 30.0f;
    private float _spawnIn = 0.0f;

    private void Start()
    {
        Wave = 0;

        for (int i = 0; i < _pools.Length; i++)
        {
            for (int j = 0; j < _pools[i].Num.Length; j++)
            {
                GameObject temp = Instantiate(_spawnableEnemies[i]);
                _pools[i].Num[j] = temp;
            }
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < _waves[Wave].Spawns.Length; i++)
        {
            int numToSpawn = _waves[Wave].Spawns[i];
            int spawned = 0;

            for (int j = 0; j < _pools[i].Num.Length; j++)
            {
                if ((spawned < numToSpawn) && (_pools[i].Num[j].activeSelf == false))
                {
                    _pools[i].Num[j].transform.position = _spawnPoint.transform.position;
                    _pools[i].Num[j].GetComponent<Enemy>().EnemyTarget = EnemyTarget;
                    _pools[i].Num[j].SetActive(true);
                    spawned = spawned + 1;
                }
            }
        }

        Wave = Wave + 1;
    }

    private void Update()
    {
        if (_spawnIn >= _spawnDelay)
        {
            _spawnIn = 0.0f;
            SpawnEnemy();
        }

        _spawnIn = _spawnIn + Time.deltaTime;
    }

}

[System.Serializable]
public class Pools
{
    public GameObject[] Num;
}

[System.Serializable]
public class Waves
{
    public int[] Spawns;
}
