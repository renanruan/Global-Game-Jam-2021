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
        //Control. += ;

        /* EVENTOS DE COLISAO */
        Collision.TakeDamege.AddListener(Health.TakeDamage);

        /* EVENTOS DE VIDA */
        //Health.OnDeath += ;
        Health.OnShieldBreak += ChangeState;
       
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  CHANGE FORM */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void ChangeState()
    {

        if (ActualMode == Mode.MechaZord)
        {

            ActualMode = Mode.Human;
            EnterHumanForm.Invoke();
        }
        else//(state == Mode.Human)
        {

            ActualMode = Mode.MechaZord;
            EnterMechaForm.Invoke();
        }

        
    }

    
}

public class UnityIntAction : UnityEvent<int>
{
}
