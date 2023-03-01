using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformPickupBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    public GameObject platform;
    public GameObject pickupParent;
    private Spawner pickupSpawner;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
        pickupSpawner = pickupParent.GetComponent<Spawner>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && gameManager.platformsRemaining < 3)
        {
            Debug.Log("Platforms Armed");
            gameManager.platformTrigger = true;
            gameManager.platformsRemaining += 3 - gameManager.platformsRemaining;
            pickupSpawner.collected = true;
            platform.SetActive(false);
        }
    }
}
