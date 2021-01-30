using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class EnemyIA : MonoBehaviour
{
    [Header("Self Parts")]
    public SpriteRenderer sprite;
    public Animator animator;
    public EnemyHealth Health;
    public EnemyShooter Shooter;


    [Header("IA")]
    public Transform target;
    public float maxLookDistance;
    public float maxAttackDistance;
    public float minDistanceFromPlayer;
    public bool targetLocker;

    [Header("Actions")]
    public UnityIntAction TakeDamege = new UnityIntAction();
    public UnityAction StartShooting;
    public UnityAction StopShooting;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  START */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Start()
    {
        target = PlayerState.Player.transform;

        /* EVENTOS DE IA */
        TakeDamege.AddListener(Health.TakeDamage);
        StartShooting += Shooter.StartShootingRoutine;
        StopShooting += Shooter.EndShootingRoutine;

        /* EVENTOS DE VIDA */
        Health.OnDeath += Death;

        /* EVENTOS DE TIRO */
        Shooter.target = target;

    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  DETECT PLAYER */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= maxLookDistance)
        {
            LookAtTarget();

            //Check distance and time
            if (distance <= maxAttackDistance)
            {
                StartShooting.Invoke();
            }
            else
            {
                StopShooting.Invoke();
            }
        }
    }


    void LookAtTarget()
    {
        // Torso olha para o Mouse
        Vector2 targetPos = target.position - transform.position;
        targetPos.Normalize();
        float rot_z = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

    }


    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  MOVIMENT */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  COLLISION */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamege.Invoke(10);
            Destroy(collision.gameObject);
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  DEATH */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void Death()
    {
        Destroy(gameObject);
    }


 




 


    /*
    void Shoot()
    {
        //Reset the time when we shoot
        shotTime = Time.time;
        Instantiate(projectile, transform.position + (target.position - transform.position).normalized, Quaternion.LookRotation(target.position - transform.position));
    }
    */
}
