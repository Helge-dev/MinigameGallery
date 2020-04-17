using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool buttonState = true;
    public bool canHit = true;
    public float timer = 2f;
    public Material[] material;
    Renderer rend;
    public float timeScale = 1f;
    public Text RightSideHit;


    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[1];
        //rb.AddForce(1500, 500, 0);
        Time.timeScale = timeScale;
    }
    void Update()
    {
        Time.timeScale = timeScale;
        if ((rb.position.x <= -50 || rb.position.x >= 50) && canHit == false)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            buttonState = true;
            canHit = true;
            rend.sharedMaterial = material[1];
            
        }
        if(rb.position.x < 50 && rb.position.x > -50)
        {
            canHit = false;
        }
        if (canHit)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                rb.useGravity = true;
                buttonState = false;
                rend.sharedMaterial = material[0];
                
            }
        }


        if (Input.GetKey("1") && buttonState == true)
        {
            if(rb.position.x <= -50)
                rb.AddForce(1200 , 800 , 0);
            else
                rb.AddForce(-1200, 800, 0);
            RightSideHit.text = "Normal shot!";
            AfterHit();
        }
        if (Input.GetKey("2") && buttonState == true)
        {
            if (rb.position.x <= -50)
                rb.AddForce(800, 1200, 0);
            else
                rb.AddForce(-800, 1200, 0);
            RightSideHit.text = "High shot!";
            AfterHit();
        }
        if (Input.GetKey("3") && buttonState == true)
        {
            if (rb.position.x <= -50)
                rb.AddForce(2000, 0, 0);
            else
                rb.AddForce(-2000, 0, 0);
            RightSideHit.text = "Smash!";
            AfterHit();
        }
        if (Input.GetKey("4") && buttonState == true)
        {
            if (rb.position.x <= -50)
                rb.AddForce(600, 800, 0);
            else
                rb.AddForce(600, 800, 0);
            RightSideHit.text = "Light shot!";
            AfterHit();
        }

    }

    public void AfterHit()
    {
        rb.useGravity = true;
        buttonState = false;
        rend.sharedMaterial = material[0];
        timer = 2;
        timeScale += 0.1f;

    }
}
