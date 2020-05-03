using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool buttonState = true, canHit = true;
    public float timer = 2f, timeScale, timerText = 0.5f;
    public int rightEnd = 50, leftEnd = -50;
    public Material[] material;
    Renderer rend;
    public Text leftSideHit, rightSideHit;
    [SerializeField]
    BallCollision ballCollision;
    
    


    void Start()
    {
        
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[1];
        //Time.timeScale = timeScale;
        
        
    }
    void Update()
    {
        Time.timeScale = timeScale;
        timerText += Time.deltaTime;


        if ((rb.position.x <= leftEnd || rb.position.x >= rightEnd) && canHit == false)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            buttonState = true;
            canHit = true;
            rend.sharedMaterial = material[1];
            
        }
        if(rb.position.x < rightEnd && rb.position.x > leftEnd)
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

        if (ballCollision.nrOfBounces > 1 && buttonState == true)
        {
            rb.useGravity = true;
            buttonState = false;
            rend.sharedMaterial = material[0];
        }

        if (Input.GetKey("1") && buttonState == true)
        {
            if(rb.position.x <= leftEnd)
            {
                
                rb.AddForce(1000, 800, 0);
                leftSideHit.text = "Normal shot!";
            }
            else
            {
                rb.AddForce(-1000, 800, 0);
                rightSideHit.text = "Normal shot!";
            }
           
            //RightSideHit.transform.
            AfterHit();
        }
        if (Input.GetKey("2") && buttonState == true)
        {
            if (rb.position.x <= leftEnd)
            {
                rb.AddForce(825, 1400, 0);
                leftSideHit.text = "High shot!";
            }
            else
            {
                rb.AddForce(-825, 1400, 0);
                rightSideHit.text = "High shot!";
            }
            //RightSideHit.transform.Rotate(new Vector3(30, 0, 0));
            AfterHit();
        }
        if (Input.GetKey("3") && buttonState == true)
        {
            if (rb.position.x <= leftEnd)
            {
                rb.AddForce(2100, -20, 0);
                leftSideHit.text = "Smash!";
            }
            else
            {
                rb.AddForce(-2100, -20, 0);
                rightSideHit.text = "Smash!";
            }
            //RightSideHit.transform.
            AfterHit();
        }
        if (Input.GetKey("4") && buttonState == true)
        {
            timer = 0f;
            if (rb.position.x <= leftEnd)
            {
                rb.AddForce(600, 800, 0);
                leftSideHit.text = "Light shot!"; 
                leftSideHit.transform.eulerAngles = new Vector3(
                 leftSideHit.transform.eulerAngles.x,
                 leftSideHit.transform.eulerAngles.y,
                 leftSideHit.transform.eulerAngles.z + 10);
            }
                
            else
            {
                rb.AddForce(-600, 800, 0);
                rightSideHit.text = "Light shot!";

            }

            
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
        ballCollision.nrOfBounces = 0;
    }
}
