using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShooter : MonoBehaviour
{
    [Header("Munition")]
    public GameObject Bullet;
    public GameObject ShootPoint;

    [Header("Shoot Settings")]
    public float AttackSpeed;
    private float AttackInterval = 0;
    public bool IsShooting = false;
    public Transform target;

    [Header("Actions")]
    public UnityAction ShootAction;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  SHOOT ROUTINE */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void StartShootingRoutine()
    {
        if (IsShooting)
            return;
        AttackInterval = 0;
        IsShooting = true;
    }
    public void EndShootingRoutine()
    {
        IsShooting = false;
    }
    private void Shoot()
    {
        ShootAction.Invoke();
        Vector2 targetPosition = target.transform.position;
        BulletIA bulletIA = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<BulletIA>();
        bulletIA.ConfigureDirection((targetPosition - (Vector2)transform.position).normalized);
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
