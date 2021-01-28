using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : PhysicsObject
{
    [Header("Limits of Moviment")]
    public float CurrentSpeed = 5;
    public float NormalSpeed = 5;
    public float SlowedSpeed = 3;

    [Header("Self Parts")]
    public Transform spriteRenderer;
    public Animator animator;

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

        animator.SetFloat("VelocityX", Mathf.Abs(velocity.x));
        animator.SetFloat("VelocityY", Mathf.Abs(velocity.y));

        targetVelocity = move * CurrentSpeed;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  PLAYER INPUTS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartShooting.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopShooting.Invoke();
        }


        // Check Mouse Position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

     }

}
