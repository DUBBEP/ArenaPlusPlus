using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float platformSpeed = 3f;
    public float onscreenDelay = 5f;
    private float platformAngle;
    private Rigidbody playerRB;
    private PlayerBehavior playerScript; 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
        platformAngle = this.transform.eulerAngles.y;
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * platformSpeed * Time.deltaTime);
    }

    void OnCollisionStay(Collision collision) 
    {
        if (collision.gameObject.name == "Player" && playerScript.IsGrounded())
        {
            Quaternion travelDirection = Quaternion.AngleAxis(platformAngle, Vector3.up);
            Vector3 forceDirection = travelDirection * Vector3.forward;
            playerRB.velocity = forceDirection.normalized * platformSpeed * 1.055f;
        }
    }
}
