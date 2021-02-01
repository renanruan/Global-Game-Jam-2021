using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIntro : MonoBehaviour
{
    public void ButtonCLick()
    {
        SceneManager.LoadScene("Scene_InGame");
    }
}
