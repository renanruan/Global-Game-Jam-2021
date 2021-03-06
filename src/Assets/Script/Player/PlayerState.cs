﻿using System.Collections;
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
    public PlayerSound Sounds;
    public PlayerAnime Animator;

    [Header("State")]
    public static Mode ActualMode = Mode.MechaZord;

    [Header("Player")]
    public static GameObject Player;

    [Header("Actions")]
    public UnityAction EnterMechaForm;
    public UnityAction EnterHumanForm;

    private void Awake()
    {
        Player = gameObject;
        ActualMode = Mode.MechaZord;
    }

    private void Start()
    {
        /* EVENTOS DE ESTADO */
        this.EnterMechaForm += Animator.EnterMechaForm;
        this.EnterMechaForm += Sounds.RetreveHead;
        this.EnterMechaForm += Health.RestoreShield;
        this.EnterMechaForm += BecameMecha;

        this.EnterHumanForm += Animator.EnterHumanForm;
        this.EnterHumanForm += Sounds.LosesHead;
        this.EnterHumanForm += Health.RestoreHealth;
        this.EnterHumanForm += BecameHuman;

        /* EVENTOS DE CONTROLE */
        Control.StartShooting += Shooter.StartShootingRoutine;
        Control.StartShooting += Animator.StartShooting;

        Control.StopShooting += Shooter.EndShootingRoutine;
        Control.StopShooting += Sounds.StopMachineGun;
        Control.StopShooting += Animator.StopShooting;

        Control.StartRunning += Sounds.StartEngine;
        Control.StartRunning += Animator.StartRunning;

        Control.StopRunning += Sounds.StopEngine;
        Control.StopRunning += Animator.StopRunning;

        /* EVENTOS DE COLISAO */
        Collision.TakeDamege.AddListener(Health.TakeDamage);
        Collision.TakeDamege.AddListener(Sounds.TakesDamage);
        Collision.MechaFound += ChangeState;

        /* EVENTOS DE VIDA */
        Health.OnDeath += Control.DesactiveCollider;
        Health.OnDeath += DeathMessage;
        Health.OnShieldBreak += ChangeState;

        /* EVENTOS DE TIRO */
        Shooter.Charge.AddListener(Animator.ChargeGun);
        Shooter.Charge.AddListener(Sounds.MachineGun);
        Shooter.OneShot += Sounds.Shoot;
        Shooter.ReloadOn += Sounds.Reload;
        Shooter.ReloadOn += Animator.ReloadOn;
        Shooter.ReloadOff += Animator.ReloadOff;

        /* EVENTOS DE SOM */
        //Sound. += ;

        /* EVENTOS DE ANIMACAO */
        //Animator. += ;

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

    void BecameHuman()
    {
        Control.StopShooting();
        SpotScript.ActiveSpot();
    }

    void BecameMecha()
    {
        SpotScript.Desactive();
    }

    public void DeathMessage()
    {
        GameManager.GM.looseMessage.text = "Parece que perdeu a cageça!";
    }

    
}

public class UnityIntAction : UnityEvent<int>
{
}

public class UnityFloatAction : UnityEvent<float>
{
}
