using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUIBehavior : MonoBehaviour
{
    public int xOffSet = 100;
    public int yOffSet = 100;

    // Update is called once per frame
    void Update() {
        float xPos = Screen.width - xOffSet;
        float yPos = Screen.height - yOffSet;
        Vector2 MinimapLocation = new Vector2(xPos, yPos);
        this.transform.position = MinimapLocation;
    }
}
