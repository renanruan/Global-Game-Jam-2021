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
    public EnemyShooter Shooter;
    public EnemyHealth Health;

    [Header("IA")]
    public Transform target;
    public float maxLookDistance;
    public float maxAttackDistance;
    public float minDistanceFromPlayer;

    [Header("Actions")]
    public UnityIntAction TakeDamege = new UnityIntAction();
    public UnityAction StartShooting;
    public UnityAction StopShooting;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  START */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Start()
    {
        /* EVENTOS DE IA */
        TakeDamege.AddListener(Health.TakeDamage);
        StartShooting += Shooter.StartShootingRoutine;
        StopShooting += Shooter.EndShootingRoutine;

        /* EVENTOS DE VIDA */
        Health.OnDeath += Death;

        /* EVENTOS DE TIRO */
        //PlayerState. += ;

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
                //Shoot();
            }
        }
    }


    void LookAtTarget()
    {
        var dir = target.position - transform.position;
        dir.y = 0;
        var rotation = Quaternion.LookRotation(dir);
       // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
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
