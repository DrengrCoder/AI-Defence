using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _spawnableEnemies;
    [SerializeField]
    private GameObject[] _spawnPoints;
    [SerializeField]
    private GameObject _bossSpawn;

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
    private EnemyProjectilePool _bulletPool;
    [SerializeField]
    private float _delayOnOverSpawn = 3.0f;
    [SerializeField]
    private float _spawnDelay = 30.0f;
    private float _spawnIn = 0.0f;

    [SerializeField]
    private Text _waveNum;
    [SerializeField]
    private Text _waveTimerNum;

    private void Start()
    {
        Wave = 0;
        _spawnIn = 30.0f;
        _waveNum.text = Wave.ToString();
        _waveTimerNum.text = _spawnIn.ToString();

        for (int i = 0; i < _pools.Length; i++)
        {
            for (int j = 0; j < _pools[i].Num.Length; j++)
            {
                GameObject temp = Instantiate(_spawnableEnemies[i]);

                if (temp.GetComponent<RangedEnemy>())
                {
                    temp.GetComponent<RangedEnemy>().BulletPool = _bulletPool;
                }

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

        Wave = Wave + 1;
        _waveNum.text = Wave.ToString();

        if (Wave == _waves.Length)
        {
            _waveTimerNum.text = "0.00";
        }

        DelayedSpawns(tospawnEnemies);
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

        int bossNum = _waves[Wave - 1].Boss;

        for (int j = 0; j < _pools[bossNum].Num.Length; j++)
        {
            if ((bossNum != 100) && (_pools[bossNum].Num[j].activeSelf == false))
            {
                GameObject boss = _pools[bossNum].Num[j];

                float height = boss.transform.position.y;
                Vector3 spawn = _bossSpawn.transform.position;
                spawn.y = height;
                boss.transform.position = spawn;

                boss.GetComponent<Enemy>().EnemyTarget = EnemyTarget;
                boss.SetActive(true);
            }
        }
    }

    private IEnumerator MoreSpawns(List<GameObject> tospawnEnemies)
    {
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

        if (Wave != (_waves.Length))
        {
            _spawnIn = _spawnIn + Time.deltaTime;
            float temp = _spawnDelay - _spawnIn;
            _waveTimerNum.text = temp.ToString("F2");
        }
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
    public int Boss = 100;
}
