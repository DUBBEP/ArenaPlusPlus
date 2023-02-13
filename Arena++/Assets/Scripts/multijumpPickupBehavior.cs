using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multijumpPickupBehavior : MonoBehaviour
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
            Debug.Log("Increased jumps by 1");
            gameManager.airJumpCount += 1;
        }
    }
}
