using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn ESpawn;

    [Header("UI")]
    public Text WaveText;

    [Header("Enemys")]
    public GameObject[] Enemy;

    [Header("Waves")]
    public Wave[] Waves;
    public float MinWaveInterval, MaxWaveInterval, MinSpawnInterval, MaxSpawnInterval;
    public int CurrentWave = 0;
    public float timer;


    public void SpawnEnemy()
    {
        if (EnemyIA.EnemysAlive < 10 + (int)(CurrentWave * 3))
        {
            int enemyId = Waves[CurrentWave].GetRandomEnemy();
            Transform parent = EnemySpawnPoint.GetSpawnPoint();
            Instantiate(Enemy[enemyId], parent);
        }
    }

    private void Awake()
    {
        ESpawn = this;
        EnemySpawnPoint.List = new List<EnemySpawnPoint>();
        SpotScript.SpotList = new List<SpotScript>();
        EnemyIA.EnemysAlive = 0;
    }

    private void Start()
    {
        timer = Random.Range(MinWaveInterval, MaxWaveInterval);
    }
    private void Update()
    {
        if (!GameManager.GM.Playing)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (Waves[CurrentWave].WaveDone())
            {
                timer = Random.Range(MinWaveInterval, MaxWaveInterval);
                CurrentWave++;
                WaveText.text = "Wave " + (CurrentWave + 1).ToString();
            }
            else
            {
                SpawnEnemy();
                timer = Random.Range(MinSpawnInterval, MaxSpawnInterval);
            }
        }

    }

}


