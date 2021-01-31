using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public static List<EnemySpawnPoint> List = new List<EnemySpawnPoint>();

    private void Start()
    {
        EnemySpawnPoint.List.Add(this);
    }

    public static Transform GetSpawnPoint()
    {
        Transform result = null;
        do
        {
            result = List[Random.Range(0, List.Count)].transform;
        } while (Vector2.Distance(result.position, PlayerState.Player.transform.position) < 4);

        return result;
    }
}
