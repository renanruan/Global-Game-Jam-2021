using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Sounds")]
    public FMODUnity.StudioEventEmitter Step;
    public FMODUnity.StudioEventEmitter Engine;
    public FMODUnity.StudioEventEmitter Shooting;
    public FMODUnity.StudioEventEmitter GunRot;
    public FMODUnity.StudioEventEmitter LoseHead;
    public FMODUnity.StudioEventEmitter LevelMusic;
    public FMODUnity.StudioEventEmitter TakeDamage;

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
        GunRot.Play();
        GunRot.SetParameter("Shooting", 0);
    }

    public void StopMachineGun()
    {
        GunRot.SetParameter("Shooting", 1);
    }

    public void Shoot()
    {
        Shooting.Play();
    }

    public void LosesHead()
    {
        LoseHead.Play();
        LevelMusic.SetParameter("head", 0);
    }

    public void RetreveHead()
    {
        LevelMusic.SetParameter("head", 1);
    }

    public void TakesDamage(int i)
    {
        TakeDamage.Play();
    }
}