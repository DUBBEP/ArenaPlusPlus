using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float respawnDelay = 5f;

    private bool isMissing;
    private float respawnTimer = 0f;

    public GameObject item;

    public bool collected
    {
        get{ return isMissing;}
        set{
            isMissing = value;
            if (isMissing && respawnTimer <= 0f)
            {
                respawnTimer = respawnDelay;
            }
        }
    }

    void Update()
    {
        if (respawnTimer >= 0f)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0f)
            {
                item.SetActive(true);
                collected = false;
            }
        }
    }
}