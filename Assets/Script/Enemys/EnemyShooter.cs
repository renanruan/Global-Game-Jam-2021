using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Munition")]
    public GameObject Bullet;

    [Header("Shoot Settings")]
    public float AttackSpeed;
    private float AttackInterval = 0;
    public bool IsShooting = false;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  SHOOT ROUTINE */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void StartShootingRoutine()
    {
        AttackInterval = 0;
        IsShooting = true;
    }
    public void EndShootingRoutine()
    {
        IsShooting = false;
    }
    private void Shoot()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        BulletIA bulletIA = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<BulletIA>();
        bulletIA.ConfigureDirection((mousePos - (Vector2)transform.position).normalized);
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  SHOOT UPDATE */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void Update()
    {
        if (IsShooting)
        {
            AttackInterval -= Time.deltaTime;
            if (AttackInterval <= 0)
            {
                AttackInterval = 1 / AttackSpeed;
                Shoot();
            }
        }
    }
}
