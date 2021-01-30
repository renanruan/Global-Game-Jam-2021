using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    [Header("Self Parts")]
    public Animator animator;

    public void EnterHumanForm()
    {
        animator.SetBool("HumanForm", true);
        animator.SetBool("MechaForm", false);
        animator.SetTrigger("ChangeForm");
    }

    public void EnterMechaForm()
    {
        animator.SetBool("HumanForm", false);
        animator.SetBool("MechaForm", true);
        animator.SetTrigger("ChangeForm");
    }

    public void StartRunning()
    {
        animator.SetBool("Running", true);
    }

    public void StopRunning()
    {
        animator.SetBool("Running", false);
    }

    public void StartShooting()
    {
        animator.SetBool("Attacking", true);
    }

    public void StopShooting()
    {
        animator.SetBool("Attacking", false);
    }
}
