using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIA : MonoBehaviour
{
    [Header("Settings")]
    public float Velocity;
    public float TimeSpawn;
    public Vector3 Direction = Vector3.zero;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  INITIAL CONFIGURATION */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void ConfigureDirection(Vector3 Direction)
    {
        this.Direction = Direction;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  MOVIMENT  */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void Update()
    {
        if(Direction != Vector3.zero)
        {
            transform.position += Velocity * Direction * Time.deltaTime;
        }

        TimeSpawn -= Time.deltaTime;
        if(TimeSpawn <= 0)
        {
            Destroy(gameObject);
        }
    }
}
