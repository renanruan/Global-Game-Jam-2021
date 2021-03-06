﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("HitPoints")]
    public int MaxHealth, MaxShield;
    public int CurrentHealth, CurrentShield;

    [Header("Health Control")]
    public Image HealthBar;
    public Image ShieldBar;

    [Header("Actions")]
    public UnityAction OnDeath;
    public UnityAction OnShieldBreak;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  START */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentShield = MaxShield;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  RECIVING DAMAGE FROM COLLISION */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void TakeDamage(int amount)
    {
        if (PlayerState.Mode.MechaZord == PlayerState.ActualMode)
        {
            CurrentShield -= amount;

            if(CurrentShield <= 0)
            {
                CurrentShield = 0;
                OnShieldBreak.Invoke();
                RestoreHealth();
                return;
            }

            UI_HP.Instance.SetHealth(CurrentShield);
        }
        else//(Mode.Human)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                GameManager.GM.Loose();
                OnDeath.Invoke();
            }

            UI_HP.Instance.SetHealth(CurrentHealth);
        }

        
    }

    public void RestoreShield()
    {
        CurrentShield = MaxShield;

        UI_HP.Instance.SetHealth(CurrentShield);
    }

    public void RestoreHealth()
    {
        CurrentHealth = MaxHealth;

        UI_HP.Instance.SetHealth(CurrentHealth);
    }


}
