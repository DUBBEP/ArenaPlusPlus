using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float platformSpeed = 3f;
    public float onscreenDelay = 20;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * platformSpeed * Time.deltaTime);
    }
}
