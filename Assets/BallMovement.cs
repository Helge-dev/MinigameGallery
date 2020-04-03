using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool buttonState = true;
    void Start()
    {
        
        //rb.AddForce(1500, 500, 0);
    }

    void Update()
    {
        if (Input.GetKey("h") && buttonState == true)
        {
            rb.AddForce(1000 , 800 , 0);
            buttonState = false;
        }
    }
}
