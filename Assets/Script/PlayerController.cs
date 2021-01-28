using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PhysicsObject
{
    [Header("Limits of Moviment")]
    public float CurrentSpeed = 5;
    public float NormalSpeed = 5;
    public float SlowedSpeed = 3;

    [Header("Self Parts")]
    public Transform spriteRenderer;
    public Animator animator;

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
            //if (shield.SetAction(true))
            //{
            //    animator.SetBool("Defending", true);
            //    CurrentSpeed = SlowedSpeed;
            //}
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //if (shield.SetAction(false))
            //{
            //    animator.SetBool("Defending", false);
            //    CurrentSpeed = NormalSpeed;
            //}
        }


        // Check Mouse Position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

     }

}
