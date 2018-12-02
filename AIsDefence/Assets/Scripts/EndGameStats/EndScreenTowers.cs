using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenTowers : MonoBehaviour {

    [SerializeField]
    private EndGameStats _stats;
    [SerializeField]
    private GameObject _WaveStatTemp;

    [SerializeField]
    private float _startY = 450;
    [SerializeField]
    private float _difference = 90;
    [SerializeField]
    private float _correction = 500;

    public void Populate()
    {
        int num = _stats.TowerStats.Length;

        for (int i = 0; i < num; i++)
        {
            GameObject waveStats = Instantiate(_WaveStatTemp);
            waveStats.transform.SetParent(gameObject.transform);

            float y = _startY - (_difference * i) - _correction;
            Debug.Log(y);
            waveStats.GetComponent<RectTransform>().localPosition = new Vector3(0, y, 0);
            waveStats.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            waveStats.GetComponent<PopulateTowerInfo>().Populate(i);
        }
    }
}
