using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _spawnableEnemies;
    [SerializeField]
    private int[] _maxEnemiesOfType;
    [SerializeField]
    private GameObject _spawnPoint;

    private int[] _spawnedEnemies;
    private float _spawnDelay = 10.0f;

    private void SpawnEnemy()
    {

    }

    private bool CheckEnemyNum()
    {
        return false;
    }

}
