using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("HitPoints")]
    public int MaxHealth, MaxShield;
    private int CurrentHealth, CurrentShield;

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
        CurrentHealth = MaxShield;
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
            }
        }
        else//(Mode.Human)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDeath.Invoke();
            }
        }
    }


}
