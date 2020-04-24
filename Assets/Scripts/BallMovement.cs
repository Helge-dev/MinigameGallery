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
    public float timeScale;
    public Text RightSideHit, LeftSideHit;
    public float timerText = 0.5f;


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
        Time.timeScale = 3f;
        timerText += Time.deltaTime;
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
            {
                
                rb.AddForce(1000, 800, 0);
                RightSideHit.text = "Normal shot!";
            }
            else
            {
                rb.AddForce(-1000, 800, 0);
                LeftSideHit.text = "Normal shot!";
            }
           
            //RightSideHit.transform.
            AfterHit();
        }
        if (Input.GetKey("2") && buttonState == true)
        {
            if (rb.position.x <= -50)
            {
                rb.AddForce(825, 1400, 0);
                RightSideHit.text = "High shot!";
            }
            else
            {
                rb.AddForce(-825, 1400, 0);
                LeftSideHit.text = "High shot!";
            }
            //RightSideHit.transform.Rotate(new Vector3(30, 0, 0));
            AfterHit();
        }
        if (Input.GetKey("3") && buttonState == true)
        {
            if (rb.position.x <= -50)
            {
                rb.AddForce(2100, -20, 0);
                RightSideHit.text = "Smash!";
            }
            else
            {
                rb.AddForce(-2100, -20, 0);
                LeftSideHit.text = "Smash!";
            }
            //RightSideHit.transform.
            AfterHit();
        }
        if (Input.GetKey("4") && buttonState == true)
        {
            timer = 0f;
            if (rb.position.x <= -50)
            {
                rb.AddForce(600, 800, 0);
                RightSideHit.text = "Light shot!";
            }
                
            else
            {
                rb.AddForce(-600, 800, 0);
                LeftSideHit.text = "Light shot!";
            }

            /*RightSideHit.transform.eulerAngles = new Vector3(
                RightSideHit.transform.eulerAngles.x,
                RightSideHit.transform.eulerAngles.y,
                RightSideHit.transform.eulerAngles.z + 5);*/
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
