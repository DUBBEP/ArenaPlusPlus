using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformPickupBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Platforms Armed");

            gameManager.platformTrigger = true;
            gameManager.platformsRemaining +=3;
        }
    }
}
