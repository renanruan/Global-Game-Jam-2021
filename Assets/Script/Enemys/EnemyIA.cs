using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyIA : MonoBehaviour
{
    public static int EnemysAlive = 0;

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

    [Header("Moviment")]
    public Vector2 Direction;
    public bool Moving = false;
    public float Speed;
    private float Timer = 0;

    [Header("Actions")]
    public FMODUnity.StudioEventEmitter SoundDie;
    public FMODUnity.StudioEventEmitter SoundShoot;

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
        EnemysAlive++;

        /* EVENTOS DE IA */
        TakeDamege.AddListener(Health.TakeDamage);
        StartShooting += Shooter.StartShootingRoutine;
        StopShooting += Shooter.EndShootingRoutine;

        /* EVENTOS DE VIDA */
        Health.OnDeath += Death;

        /* EVENTOS DE TIRO */
        Shooter.target = target;
        Shooter.ShootAction += Shoot;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  DETECT PLAYER */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Ve player
        if (distance <= maxLookDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, maxLookDistance, LayerMask.GetMask("Default"));

            if (hit)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    LookAtTarget();
                    animator.SetFloat("Speed", 0);

                    // Atira no player
                    if (distance <= maxAttackDistance)
                    {
                        StartShooting.Invoke();
                    }
                    else
                    {
                        StopShooting.Invoke();
                    }

                    return;
                }
            }
        }

        Move();
        return;
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
    public void ChangeDirection(bool forceChange)
    {
        Vector2 newDirection = Vector2.zero;

        do
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    newDirection = Vector2.up;
                    break;
                case 1:
                    newDirection = Vector2.down;
                    break;
                case 2:
                    newDirection = Vector2.right;
                    break;
                case 3:
                    newDirection = Vector2.left;
                    break;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, newDirection, 0.6f, LayerMask.GetMask("Default"));
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    newDirection = Direction;
                }
            }
        } while (newDirection == Direction && forceChange);

        Direction = newDirection;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg + 90);
    }

    public void Move()
    {
        Timer -= Time.deltaTime;

        if(Moving)
        {
            animator.SetFloat("Speed", 1);
            transform.position += (Vector3)Direction * Speed * Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, 0.6f, LayerMask.GetMask("Default"));
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    ChangeDirection(true);
                    return;
                }
            }

            

            if (Timer <= 0)
            {
                int random = Random.Range(0, 100);

                if(random > 20)
                {
                    ChangeDirection(false);
                    Timer = Random.Range(3, 7);
                }
                else
                {
                    Moving = false;
                    animator.SetFloat("Speed", 0);
                    Timer = Random.Range(1, 3);
                    return;
                }
            }

            
        }
        else
        {
            if(Timer <= 0)
            {
                ChangeDirection(false);
                Moving = true;
                Timer = Random.Range(3, 7);
            }
        }

        

    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  COLLISION */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamege.Invoke(10);
            collision.GetComponent<BulletIA>().HitDamage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            ChangeDirection(true);
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  DEATH */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void Death()
    {
        SoundDie.Play();
        animator.SetBool("Morto", true);
        EnemysAlive--;
        this.enabled = false;
        Shooter.enabled = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  SHOOT */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void Shoot()
    {
        SoundShoot.Play();
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
