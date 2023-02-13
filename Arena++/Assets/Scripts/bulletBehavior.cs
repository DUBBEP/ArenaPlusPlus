using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;   

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }

    void Update()
    {

    }
}
