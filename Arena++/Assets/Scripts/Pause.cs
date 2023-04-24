using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject GameOverlay;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject arenaSpawn;
    [SerializeField] GameObject islandsSpawn;
    private bool gameIsPaused = false;
   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            if (gameIsPaused)
            {
                UnpauseGame();
            } 
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
        GameOverlay.SetActive(false);
        gameIsPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        GameOverlay.SetActive(true);
        gameIsPaused = false;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void TravelToArena()
    {
        Player.transform.position = arenaSpawn.transform.position;
        Player.transform.rotation = arenaSpawn.transform.rotation;

        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        GameOverlay.SetActive(true);
        gameIsPaused = false;
    }

    public void TravelToIslands()
    {
        Player.transform.position = islandsSpawn.transform.position;
        Player.transform.rotation = islandsSpawn.transform.rotation;
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        GameOverlay.SetActive(true);
        gameIsPaused = false;
    }
}
