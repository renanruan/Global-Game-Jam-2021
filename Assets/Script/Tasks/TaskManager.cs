using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager TManeger;

    [Header("Tasks")]
    public Task[] Tasks;

    public float timer = 40;
    public int totalTasks = 0;

    private void Awake()
    {
        TManeger = this;
    }

    public void SpawnTask()
    {
        if (totalTasks < 1 + (int)(EnemySpawn.ESpawn.CurrentWave / 5))
        {
            int i = Random.Range(0, Tasks.Count());
            int j = 0;
            Task task = null;
            do
            {
                task = Tasks[i];
                i = (i + 1) % Tasks.Count();
                j++;

                if(j > Tasks.Count())
                {
                    return;
                }
            } while (task.IsAvailable);
            task.TaksConclusionTime = 60 - (int)(EnemySpawn.ESpawn.CurrentWave / 3);
            task.Activate();
        }
    }

    private void Update()
    {
        if (!GameManager.GM.Playing)
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            SpawnTask();
            timer = Random.Range(15, 45 - (int)(EnemySpawn.ESpawn.CurrentWave / 5));
        }
    }

    }
