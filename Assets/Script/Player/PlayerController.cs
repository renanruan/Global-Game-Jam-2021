using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : PhysicsObject
{
    [Header("Self Parts")]
    public Animator animator;
    public GameObject Torso, Pernas;

    [Header("Sounds")]
    public FMODUnity.StudioEventEmitter Engine;
    public FMODUnity.StudioEventEmitter Shooting;

    [Header("Limits of Moviment")]
    public float CurrentSpeed = 5;
    public float NormalSpeed = 5;
    public float SlowedSpeed = 3;

    [Header("Actions")]
    public UnityAction StartShooting;
    public UnityAction StopShooting;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  START SETTINGS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Start()
    {
        CurrentSpeed = NormalSpeed;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  PHYSICS VIRTUAL CALL */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        targetVelocity = move * CurrentSpeed;

        if(targetVelocity.magnitude != 0f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        Engine.SetParameter("Moving", (int)move.magnitude);
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  PLAYER INPUTS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartShooting.Invoke();
            animator.SetBool("Attacking", true);
            Shooting.SetParameter("Shooting", 1);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopShooting.Invoke();
            animator.SetBool("Attacking", false);
            Shooting.SetParameter("Shooting", 0);
        }


        // Torso olha para o Mouse
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePos.Normalize();
        float rot_z = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Torso.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

        // Perna seguem movimento
        if (targetVelocity == Vector2.zero)
            return;
        mousePos = targetVelocity.normalized;
        rot_z = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Pernas.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

    }

}
