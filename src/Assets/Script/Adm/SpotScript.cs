using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotScript : MonoBehaviour
{

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  STATIC ADM  */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public static List<SpotScript> SpotList = new List<SpotScript>();
    private static SpotScript ActivedSpot;

    public static void ActiveSpot()
    {
        int i = Random.Range(0, SpotList.Count);

        while (true)
        {
            SpotScript spot = SpotList[i];

            if(Vector2.Distance(spot.gameObject.transform.position, PlayerState.Player.transform.position) > 4)
            {
                spot.Active();
                ActivedSpot = spot;
                return;
            }
            i = (i + 1) % SpotList.Count;
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  START  */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    public float BipDelay;
    private float timer;
    public FMODUnity.StudioEventEmitter Bip;
    public AudioSource Bip3D;

    private void Start()
    {
        SpotList.Add(this);
        gameObject.SetActive(false);
        timer = 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = BipDelay;
            //Bip.Play();
            Bip3D.Play();
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public static void Desactive()
    {
        ActivedSpot.gameObject.SetActive(false);
    }

}
