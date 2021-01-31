using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_AMMO : MonoBehaviour
{
    public static UI_AMMO Instance;

    public Image[] Ammo;

    public Sprite AmmoOn;
    public Sprite AmmoOff;

    public void Awake()
    {
        Instance = this;
    }

    public void SetAmmo(int Percentage)
    {
        float fraction = 100f / Ammo.Count();
        int i = 0;

        while (i < Ammo.Count())
        {
            if (i * fraction < Percentage)
            {
                Ammo[i].sprite = AmmoOn;
            }
            else
            {
                Ammo[i].sprite = AmmoOff;
            }

            i++;
        }
    }
}
