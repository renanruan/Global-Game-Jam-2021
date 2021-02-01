using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [Header("Self Parts")]
    public Animator anim;
    public FMODUnity.StudioEventEmitter SAcept;
    public FMODUnity.StudioEventEmitter SDeny;
    public FMODUnity.StudioEventEmitter SHov;
    public FMODUnity.StudioEventEmitter SClick;
    public FMODUnity.StudioEventEmitter SAppear;
    public SpriteRenderer Arrow;

    [Header("State Machine")]
    public bool IsAvailable = false;
    public bool InContact = false;

    [Header("Timer Settings")]
    public float TimerTime;
    public float TimeHold;
    public float BeenHolding;
    public bool TimerActive = false;
    public float TaksConclusionTime;


    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  COLLISION CALLS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InContact = true;
            BeenHolding = TimeHold;
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
    public void Activate()
    {
        IsAvailable = true;
        anim.SetBool("Available", IsAvailable);
        TimerActive = true;
        TimerTime = TaksConclusionTime;
        TaskManager.TManeger.totalTasks++;
        SoundAppear();
    }
    void OnTaskConclusion()
    {
        IsAvailable = false;
        anim.SetBool("Available", IsAvailable);
        TimerActive = false;
        TaskManager.TManeger.totalTasks--;

    }
    void OnTimerTick()
    {
        GameManager.GM.looseMessage.text = "Tarefa não concluida a tempo...";
        GameManager.GM.Loose();
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  SOUNDS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public void SoundAccept()
    {
        SAcept.Play();
    }
    public void SoundDeny()
    {
        SDeny.Play();
    }
    public void SoundHover()
    {
        SHov.Play();
    }

    public void SoundAppear()
    {
        SAppear.Play();
    }

    public void SoundClick()
    {
        SClick.Play();
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  TIMER RUNNING */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Update()
    {
        if(!GameManager.GM.Playing)
        {
            return;
        }

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
                BeenHolding = TimeHold;
                SoundClick();
            }
            else if (Input.GetKey(KeyCode.E))
            {
                BeenHolding -= Time.deltaTime;
                if(BeenHolding <= 0)
                {
                    OnTaskConclusion();
                    BeenHolding = TimeHold;
                }
            }
        }

        float perc =  BeenHolding / TimeHold;
        Arrow.color = new Color(perc, perc, perc);
    }

    private void Start()
    {
        BeenHolding = TimeHold;
    }
}
