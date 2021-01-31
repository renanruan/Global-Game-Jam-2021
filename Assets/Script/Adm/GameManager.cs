using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public Animator anim;
    public bool Playing = false;

    private void Awake()
    {
        GM = this;
        EnemySpawnPoint.List = new List<EnemySpawnPoint>();
    }


    public void StartLevel()
    {
        Playing = true;
        anim.SetBool("Start", true);
    }

    public void Loose()
    {
        Playing = false;
        anim.SetBool("Loose", true);
    }

    public void Win()
    {
        Playing = false;
        anim.SetBool("Win", true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_InGame");
    }
}
