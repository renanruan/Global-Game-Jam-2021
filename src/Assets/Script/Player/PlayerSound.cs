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
    public FMODUnity.StudioEventEmitter RetrieveHead;
    public FMODUnity.StudioEventEmitter LevelMusic;
    public FMODUnity.StudioEventEmitter TakeDamage;
    public FMODUnity.StudioEventEmitter Reloading;

    private bool available = false;
    private float timer = 0.5f;

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

    public void MachineGun(float f)
    {
        if (!available)
            return;

        if(f == 0)
        {
            StartMachineGun();
        }
    }

    public void StartMachineGun()
    {
        GunRot.Play();
        GunRot.SetParameter("Shooting", 1);
    }

    public void StopMachineGun()
    {
        GunRot.SetParameter("Shooting", 0);
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
        RetrieveHead.Play();
    }

    public void TakesDamage(int i)
    {
        TakeDamage.Play();
    }

    public void Reload()
    {
        Reloading.Play();
        GunRot.SetParameter("Shooting", 0);
    }


    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            available = true;
            this.enabled = false;
        }
    }
}