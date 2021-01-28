using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : MonoBehaviour
{
    public enum Mode { MechaZord, Human };

    [Header("Self Parts")]
    public PlayerController Control;
    public PlayerCollision Collision;
    public PlayerHealth Health;
    public PlayerShooter Shooter;
    public Animator animator;

    [Header("State")]
    public static Mode ActualMode = Mode.MechaZord;

    [Header("Actions")]
    public UnityAction EnterMechaForm;
    public UnityAction EnterHumanForm;

    private void Start()
    {
        /* EVENTOS DE ESTADO */
        //PlayerState. += ;

        /* EVENTOS DE CONTROLE */
        Control.StartShooting += Shooter.StartShootingRoutine;
        Control.StopShooting += Shooter.EndShootingRoutine;

        /* EVENTOS DE COLISAO */
        Collision.TakeDamege.AddListener(Health.TakeDamage);
        Collision.MechaFound += ChangeState;

        /* EVENTOS DE VIDA */
        //Health.OnDeath += ;
        Health.OnShieldBreak += ChangeState;

        /* EVENTOS DE TIRO */
        //Shooter. += ;


        animator.SetBool("MechaForm", true);

    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  CHANGE FORM */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void ChangeState()
    {

        if (ActualMode == Mode.MechaZord)
        {

            ActualMode = Mode.Human;
            animator.SetBool("HumanForm", true);
            animator.SetBool("MechaForm", false);
            animator.SetTrigger("ChangeForm");
            EnterHumanForm.Invoke();
        }
        else//(state == Mode.Human)
        {

            ActualMode = Mode.MechaZord;
            animator.SetBool("HumanForm", false);
            animator.SetBool("MechaForm", true);
            animator.SetTrigger("ChangeForm");
            EnterMechaForm.Invoke();
        }

        
    }

    
}

public class UnityIntAction : UnityEvent<int>
{
}
