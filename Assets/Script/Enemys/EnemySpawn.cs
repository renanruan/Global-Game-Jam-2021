using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Spawn Area")]
    public Rect Area;

    [Header("Enemys")]
    public GameObject[] Enemy;

    public void SpawnEnemy(int id)
    {

    }

    private void OnDrawGizmos()
    {
        Area.position = transform.position;
        Gizmos.DrawLine(new Vector3(Area.xMin,Area.yMin), new Vector3(Area.xMin, Area.yMax));
        Gizmos.DrawLine(new Vector3(Area.xMax, Area.yMin), new Vector3(Area.xMax, Area.yMax));
        Gizmos.DrawLine(new Vector3(Area.xMin, Area.yMin), new Vector3(Area.xMax, Area.yMin));
        Gizmos.DrawLine(new Vector3(Area.xMin, Area.yMax), new Vector3(Area.xMax, Area.yMax));
    }

}
