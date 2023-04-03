using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public RawImage platformIcon;
    public TextMeshProUGUI platCountDisplay;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI itemsCollectedDisplay;
    
    public Image EndScreenUI;

    public RawImage jumpRing1;
    public RawImage jumpRing2;
    public RawImage jumpRing3;

    public bool showWinScreen = false;
    public string labelText = "Collect all 6 items to complete the stage.";

    public bool showLossScreen = false;

    public int maxItems = 6;
    private int _itemsCollected = 0;

    public int airJumpCount = 0;

    public int platformsRemaining = 0;
    public bool platformTrigger;
    public int Items

    {
        get { return _itemsCollected; }
        set {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);

            if(_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";

                showWinScreen = true;

            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }
    private int _playerHP = 10;

    public int HP
    {
        get { return _playerHP; }
        set{
            _playerHP = value;
            Debug.LogFormat("Lives:{0}", _playerHP);
        
            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
                
            } else
            {
                labelText = "Ouch... that's got to hurt.";
            }     
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
    }

    void OnGUI()
    {
        healthDisplay.text = _playerHP.ToString("0");
        // GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);
        
        // GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected);
        itemsCollectedDisplay.text = _itemsCollected.ToString("0") + "/" + maxItems.ToString("0");
        
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            EndScreenUI.gameObject.SetActive(true);

            // if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "YOU WON!"))
            // {
            //     RestartLevel();
            // }
        
        }

        if (platformTrigger)
        {
            platformIcon.gameObject.SetActive(true);
            platCountDisplay.text = platformsRemaining.ToString("0");
            // GUI.Box(new Rect(Screen.width / 2 + 50, Screen.height / 2 - 25, 100, 25), "Platforms: " + platformsRemaining);
        } else
        {
            platformIcon.gameObject.SetActive(false);
            
        }
        
        if (airJumpCount > 0)
        {
            jumpRing1.gameObject.SetActive(true);
            if (airJumpCount > 1)
            {
                jumpRing2.gameObject.SetActive(true);
                if (airJumpCount > 2)
                {
                    jumpRing3.gameObject.SetActive(true);
                } else
                {
                    jumpRing3.gameObject.SetActive(false);
                }
            } else
            {
                jumpRing2.gameObject.SetActive(false);
                jumpRing3.gameObject.SetActive(false);
            }
            // GUI.Box(new Rect(Screen.width / 2 - 175, Screen.height / 2 - 25, 125, 25), "AirJump Count: " + airJumpCount);
        } else
        {
            jumpRing1.gameObject.SetActive(false);
            jumpRing2.gameObject.SetActive(false);
            jumpRing3.gameObject.SetActive(false);
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                RestartLevel();
            }
        }
    }
}
