using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool buttonState = true, hitOnce = true;
    public float timer = 2f, timeScale, timerText = 0.5f;
    public int rightEnd = 50, leftEnd = -50;
    [SerializeField]
    Material[] material = null;
    Renderer rend;
    [SerializeField]
    Text PlayerRight, PlayerLeft, leftSideHit, rightSideHit;
    [SerializeField]
    BallCollision ballCollision = null;
    List<int> PlayerListLeft = new List<int>();
    List<int> PlayerListRight = new List<int>();
    bool left = true;
    int whoIsHitting;



    void Start()
    {
        
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[1];
        int count = 0;
        if(DataStorage.GetSetControllers != null)
        {
            foreach (int i in DataStorage.GetSetControllers.Keys)
            {
                if (count % 2 == 0)
                {
                    PlayerListLeft.Add(i);
                }
                else
                {
                    PlayerListRight.Add(i);
                }

                count++;
            }
            Console.WriteLine(PlayerListLeft.Count + PlayerListRight.Count);
        }

        whoIsHitting = PlayerListLeft[0];
    }
    void Update()
    {
        Time.timeScale = timeScale;

        /*if(PlayerListLeft.Count > PlayerListRight.Count)
        {
            whoIsHitting = PlayerListLeft[0];
        }*/
        if (PlayerListRight.Count != 0)
        {
            PlayerRight.text = "Player " + PlayerListRight[0];
            PlayerRight.color = DataStorage.GetSetPlayerColor[PlayerListRight[0]];
        }
        if (PlayerListLeft.Count != 0)
        {
            PlayerLeft.text = "Player " + PlayerListLeft[0];
            PlayerLeft.color = DataStorage.GetSetPlayerColor[PlayerListLeft[0]];
        }

        if ((rb.position.x <= leftEnd || rb.position.x >= rightEnd) && hitOnce == false)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            buttonState = true;
            hitOnce = true;
            rend.sharedMaterial = material[1];
            
        }
        if(rb.position.x < rightEnd && rb.position.x > leftEnd)
        {
            hitOnce = false;
        }
        if (hitOnce)
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
            if(PlayerListLeft.Count <= 1 && PlayerListRight.Count <= 1)
            {
                
                if (left)
                {
                    CommonCommands.NextGame(PlayerListLeft, PlayerListRight);
                    
                }
                else if (!left)
                {
                    CommonCommands.NextGame(PlayerListRight, PlayerListLeft);
                }
                
            }
            if (left)
            {
                PlayerListLeft.RemoveAt(PlayerListLeft.Count - 1);
            }
            else if (!left)
            {
                PlayerListRight.RemoveAt(PlayerListRight.Count - 1);
            }


            
            
        }

        if ((Input.GetKey("1") || DataStorage.GetSetControllers[whoIsHitting].GetButtonEastPressed ) && buttonState == true)
        {
            Hit(1000, 800, "Normal Hit");
        }
        if ((Input.GetKey("2") || DataStorage.GetSetControllers[whoIsHitting].GetButtonSouthPressed) && buttonState == true)
        {
            Hit(825, 1400, "High Hit");
        }
        if ((Input.GetKey("3") || DataStorage.GetSetControllers[whoIsHitting].GetButtonWestPressed) && buttonState == true)
        {
            Hit(2100, -20, "Smash");
        }
        if ((Input.GetKey("4") || DataStorage.GetSetControllers[whoIsHitting].GetButtonNorthPressed) && buttonState == true)
        {
            Hit(600, 800, "Low Hit");
        }

    }

    public void Hit(int x, int y, string hitType)
    {
        if (rb.position.x <= leftEnd)
        {

            rb.AddForce(x, y, 0);
            leftSideHit.text = hitType;
        }
        else
        {
            rb.AddForce(-x, y, 0);
            rightSideHit.text = hitType;
        }

        AfterHit();
    }

    public void AfterHit()
    {
        
        if(left)
        {
            PlayerListRight.Add(PlayerListLeft[0]);
            PlayerListLeft.RemoveAt(0);
            whoIsHitting = PlayerListRight[0];
            left = false;
        }
        else
        {
            PlayerListLeft.Add(PlayerListRight[0]);
            PlayerListRight.RemoveAt(0);
            whoIsHitting = PlayerListLeft[0];
            left = true;
        }
        
        rb.useGravity = true;
        buttonState = false;
        rend.sharedMaterial = material[0];
        timer = 2;
        timeScale += 0.1f;
        ballCollision.nrOfBounces = 0;
        Debug.Log("Player nr: " + whoIsHitting + " is Hitting");
    }
}
