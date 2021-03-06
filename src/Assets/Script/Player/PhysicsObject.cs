﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    [Header("Self Parts")]
    public Rigidbody2D rb2d;
    public Collider2D coll2d;

    protected Vector2 targetVelocity;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.1f;


    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        if (GameManager.GM.Playing)
        {
            ComputeVelocity();
            CheckInput();
        }
    }

    protected virtual void ComputeVelocity()
    {

    }

    protected virtual void CheckInput()
    {

    }

    void FixedUpdate()
    {
        velocity.x = targetVelocity.x;
        velocity.y = targetVelocity.y;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Movement(deltaPosition);
    }

    void Movement(Vector2 move)
    {
        float distance = move.magnitude;


        if (distance > minMoveDistance)
        {

            int count = coll2d.Cast(move * Vector2.up, contactFilter, hitBuffer, shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            count = coll2d.Cast(move * Vector2.right, contactFilter, hitBuffer, shellRadius);
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                float projection = Vector2.Dot(move, currentNormal);
                if (projection < 0)
                {
                    move = move - projection * currentNormal;
                }
            }

        }
 
        transform.position = (Vector2)transform.position + move;
    }
}
