using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multijumpPickupBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    public GameObject jumpBoost;
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
        if (collision.gameObject.name == "Player" && gameManager.airJumpCount < 3)
        {
            Debug.Log("Increased jumps by 1");
            gameManager.airJumpCount += 1;
            pickupSpawner.collected = true;
            jumpBoost.SetActive(false);
        }
    }
}
