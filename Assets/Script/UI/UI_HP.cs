using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    public static UI_HP Instance;

    public Image[] Hearts;

    public Sprite HeartOn;
    public Sprite HeartOff;

    public void Awake()
    {
        Instance = this;
    }

    public void SetHealth(int Percentage)
    {
        float fraction = 100f / Hearts.Count();
        int i = 0;

        while(i < Hearts.Count())
        {
            if( i * fraction < Percentage)
            {
                Hearts[i].sprite = HeartOn;
            }
            else
            {
                Hearts[i].sprite = HeartOff;
            }

            i++;
        }
    }

}
