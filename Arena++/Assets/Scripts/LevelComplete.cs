using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public PlayerBehavior playerBehavior;



    public void FreezePlayer()
    {
        playerBehavior.standardFallForce = 3f;
        playerBehavior.floatyFallForce = 3f;
        playerBehavior.decendingForce = 3f;
        playerBehavior.fallSpeed = 3f;

        playerBehavior.playerIsMobile = false;
        playerBehavior.downTime = 1000f;
    }


    public void MakePlayerFly()
    {
        playerBehavior.standardFallForce = 10f;
        playerBehavior.floatyFallForce = 10f;
        playerBehavior.decendingForce = 10f;
        playerBehavior.fallSpeed = 10f;

    }

    public void LoadEndScreen()
    {
        SceneManager.LoadScene("Credits");
    }
}
