using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [Header("State Machine")]
    public bool IsAvailable = false;
    public bool InContact = false;

    [Header("Timer Settings")]
    public float TimerTime;
    public bool TimerActive = false;
    public float MinTime, MaxTime;


    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  COLLISION CALLS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.tag == "Player")
        {
            InContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InContact = true;
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  AVAILABILITY CALLS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void OnTimerTick()
    {
        IsAvailable = true;
        TimerActive = false;
        TimerTime = 0;
    }
    void OnTaskConclusion()
    {
        IsAvailable = false;
        TimerActive = true;
        TimerTime = Random.Range(MinTime, MaxTime);
    }


    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  TIMER RUNNING */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Update()
    {
        if (TimerActive)
        {
            TimerTime -= Time.deltaTime;

            if (TimerTime <= 0)
            {
                OnTimerTick();
            }
        }
    }
}
