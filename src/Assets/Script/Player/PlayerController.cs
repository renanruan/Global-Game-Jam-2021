using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : PhysicsObject
{
    [Header("Self Parts")]
    public GameObject Torso, Pernas;
    public Collider2D coll;

    [Header("Limits of Moviment")]
    public float CurrentSpeed = 5;
    public float NormalSpeed = 5;
    public float SlowedSpeed = 3;

    [Header("Actions")]
    public UnityAction StartShooting;
    public UnityAction StopShooting;
    public UnityAction StartRunning;
    public UnityAction StopRunning;

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
            StartRunning.Invoke();
        }
        else
        {
            StopRunning.Invoke();
        }
        
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  PLAYER INPUTS */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    protected override void CheckInput()
    {
        if (PlayerState.ActualMode == PlayerState.Mode.MechaZord)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartShooting.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopShooting.Invoke();
            }

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

    public void DesactiveCollider()
    {
        coll = Torso.GetComponent<Collider2D>();
        coll.enabled = false;
    }

}
