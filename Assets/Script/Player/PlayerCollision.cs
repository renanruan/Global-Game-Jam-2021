using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [Header("Actions")]
    public UnityIntAction TakeDamege = new UnityIntAction();
    public UnityAction MechaFound;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  RIGID BODY CALL */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamege.Invoke(collision.gameObject.GetComponent<BulletIA>().Damage);
            collision.GetComponent<BulletIA>().HitDamage();
        }
        else
        if (collision.gameObject.tag == "Head")
        {
            MechaFound.Invoke();
        }

    }

}
