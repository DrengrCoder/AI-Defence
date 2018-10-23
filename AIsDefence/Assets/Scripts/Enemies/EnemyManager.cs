using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _spawnableEnemies;
    [SerializeField]
    private GameObject[] _spawnPoints;

    [SerializeField]
    private Waves[] _waves;

    [SerializeField]
    private Pools[] _pools;

    public int Wave = 0;

    [SerializeField]
    private string _protectorName = "Protector";

    [SerializeField]
    private GameObject EnemyTarget;
    [SerializeField]
    private float _delayOnOverSpawn = 3.0f;
    [SerializeField]
    private float _spawnDelay = 30.0f;
    private float _spawnIn = 0.0f;

    private void Start()
    {
        Wave = 0;
        _spawnIn = 30.0f;

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
        List<GameObject> tospawnEnemies = new List<GameObject>();

        for (int i = 0; i < _waves[Wave].Spawns.Length; i++)
        {
            int numToSpawn = _waves[Wave].Spawns[i];
            int tospawn = 0;

            for (int j = 0; j < _pools[i].Num.Length; j++)
            {
                if ((tospawn < numToSpawn) && (_pools[i].Num[j].activeSelf == false))
                {
                    tospawnEnemies.Add(_pools[i].Num[j]);
                    tospawn = tospawn + 1;
                }
            }
        }

        DelayedSpawns(tospawnEnemies);

        Wave = Wave + 1;
    }

    //Need to add a protector
    private void DelayedSpawns(List<GameObject> tospawnEnemies)
    {
        List<GameObject> toRemoveFromList = new List<GameObject>();

        for (int k = 0; k < tospawnEnemies.Count; k++)
        {
            if (_spawnPoints[k].name != _protectorName)
            {
                float height = tospawnEnemies[k].transform.position.y;
                Vector3 spawn = _spawnPoints[k].transform.position;
                spawn.y = height;
                tospawnEnemies[k].transform.position = spawn;

                tospawnEnemies[k].GetComponent<Enemy>().EnemyTarget = EnemyTarget;
                tospawnEnemies[k].SetActive(true);
                toRemoveFromList.Add(tospawnEnemies[k]);
            }
            else
            {

                for (int i = 0; i < toRemoveFromList.Count; i++)
                {
                    tospawnEnemies.Remove(toRemoveFromList[i]);
                }

                StartCoroutine(MoreSpawns(tospawnEnemies));
                return;
            }
        }
    }

    private IEnumerator MoreSpawns(List<GameObject> tospawnEnemies)
    {
        Debug.Log("entered");
        yield return new WaitForSeconds(_delayOnOverSpawn);
        DelayedSpawns(tospawnEnemies);
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
