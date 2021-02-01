using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAutoDestructive : MonoBehaviour
{
    public float Timer;

    public FMODUnity.StudioListener target;

    private void Update()
    {
        Timer -= Time.deltaTime;

        if(Timer <= 0)
        {
            target.enabled = true;
            this.enabled = false;
        }
    }
}
