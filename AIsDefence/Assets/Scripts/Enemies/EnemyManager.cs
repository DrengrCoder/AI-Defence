using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _spawnableEnemies;
    [SerializeField]
    private Spawns[] _spawnPoints;
    [SerializeField]
    private GameObject _bossSpawn;
    [SerializeField]
    private StatManager _statManager;

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

    private bool _finished = false;

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

        if ((Wave - 1) != -1)
        {
            CreditBanks Bank = FindObjectOfType<CreditBanks>();
            Bank.AddPlayerCredits(_waves[Wave - 1].PlayerCredits);
        }

        Wave = Wave + 1;
        _waveNum.text = Wave.ToString();

        if (Wave == _waves.Length)
        {
            _waveTimerNum.text = "0.00";

            _finished = true;
        }

        DelayedSpawns(tospawnEnemies);
    }

    private void DelayedSpawns(List<GameObject> tospawnEnemies)
    {
        List<GameObject> toRemoveFromList = new List<GameObject>();

        for (int k = 0; k < tospawnEnemies.Count; k++)
        {
            if (_spawnPoints[_waves[Wave - 1].SpawnPoint].SpawnPoints[k].name != _protectorName)
            {
                float height = tospawnEnemies[k].transform.position.y;
                Vector3 spawn = _spawnPoints[_waves[Wave - 1].SpawnPoint].SpawnPoints[k].transform.position;
                spawn.y = height;
                tospawnEnemies[k].transform.position = spawn;

                tospawnEnemies[k].GetComponent<Enemy>().EnemyTarget = EnemyTarget;
                tospawnEnemies[k].SetActive(true);
                tospawnEnemies[k].GetComponent<Enemy>().SetWaveNum(Wave - 1);
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

        if (bossNum != 100)
        {
            for (int j = 0; j < _pools[bossNum].Num.Length; j++)
            {
                if (_pools[bossNum].Num[j].activeSelf == false)
                {
                    GameObject boss = _pools[bossNum].Num[j];

                    float height = boss.transform.position.y;
                    Vector3 spawn = _bossSpawn.transform.position;
                    spawn.y = height;
                    boss.transform.position = spawn;

                    boss.GetComponent<Enemy>().EnemyTarget = EnemyTarget;
                    boss.SetActive(true);
                    boss.GetComponent<Enemy>().SetWaveNum(Wave - 1);
                }
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

    private void FixedUpdate()
    {
        if (_finished == true)
        {
            CheckWin();
        }
    }

    private void CheckWin()
    {
        bool won = true;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i =0; i < Enemies.Length; i++)
        {
            if (Enemies[i].activeSelf == true)
            {
                won = false;
                break;
            }
        }

        if (won == true)
        {
            _statManager.CompletedLevel();
        }
    }

}

[System.Serializable]
public class Pools
{
    public GameObject[] Num;
}

[System.Serializable]
public class Spawns
{
    public GameObject[] SpawnPoints;
}

[System.Serializable]
public class Waves
{
    public int[] Spawns;
    public int Boss = 100;
    public int SpawnPoint;

    public int PlayerCredits;
}
