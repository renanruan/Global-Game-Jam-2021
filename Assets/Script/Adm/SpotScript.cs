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
    private void Start()
    {
        SpotList.Add(this);
        gameObject.SetActive(false);
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
