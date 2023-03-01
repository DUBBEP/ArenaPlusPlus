using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    void Start()
    {
      gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    void OnTriggerEnter(Collider collision)
    {
      if (collision.gameObject.name == "Player") 
      {
        Destroy(this.transform.parent.gameObject);
        Debug.Log("Item Collected!");
        gameManager.Items += 1;
      } 
    }
}