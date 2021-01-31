using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("HitPoints")]
    public int MaxHealth;
    private int CurrentHealth;

    [Header("Health Control")]
    public Image HealthBar;

    [Header("Actions")]
    public UnityAction OnDeath;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  START */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Start()
    {
        MaxHealth += EnemySpawn.ESpawn.CurrentWave * (5 + EnemySpawn.ESpawn.CurrentWave / 20);
        CurrentHealth = MaxHealth;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  RECIVING DAMAGE FROM COLLISION */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void TakeDamage(int amount)
    {

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDeath.Invoke();
        }

    }


}
