using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Sounds")]
    public FMODUnity.StudioEventEmitter Step;
    public FMODUnity.StudioEventEmitter Engine;
    public FMODUnity.StudioEventEmitter Shooting;

    public void SoundStepOn()
    {
        Step.Play();
    }

    public void SoundStepOff()
    {
        Step.Stop();
    }

    public void StartEngine()
    {
        Engine.SetParameter("Moving", 1);
    }

    public void StopEngine()
    {
        Engine.SetParameter("Moving", 0);
    }

    public void StartMachineGun()
    {
        Shooting.SetParameter("Shooting", 0);
    }

    public void StopMachineGun()
    {
        Shooting.SetParameter("Shooting", 1);
    }
}