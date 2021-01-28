using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [Header("Self Parts")]
    public Animator anim;

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
        if (collision.gameObject.tag == "Player")
        {
            InContact = true;
            anim.SetBool("Collided", InContact);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InContact = false;
            anim.SetBool("Collided", InContact);
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  AVAILABILITY CALLS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void OnTimerTick()
    {
        IsAvailable = true;
        anim.SetBool("Available", IsAvailable);
        TimerActive = false;
        TimerTime = 0;
    }
    void OnTaskConclusion()
    {
        IsAvailable = false;
        anim.SetBool("Available", IsAvailable);
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

            if (TimerTime <= 0f)
            {
                OnTimerTick();
            }
        }

        if(IsAvailable && InContact)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnTaskConclusion();
            }
        }
    }

    private void Start()
    {
        TimerActive = true;
    }
}
