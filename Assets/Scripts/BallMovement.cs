using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool buttonState = true, hitOnce = true;
    public float timer = 2f, timeScale, timerText, timeBeforeReset = 5f;
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
    public bool left = true;
    int whoIsHitting;
    bool resetGame;
    



    void Start()
    {
        
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[1];
        int count = 0;

        //Puts all the players in these two lists
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
        timerText -= Time.deltaTime;

        //Manages the "reset"
        if (resetGame)
        {
            timeBeforeReset -= Time.deltaTime;
            if(timeBeforeReset < 0)
            {
                resetGame = false;
                Reset();
            }
        }
        
        //resets the texts after 0,5 sec
        if(timerText <= 0)
        {
            
            leftSideHit.text = null;
            rightSideHit.text = null;
        }
        /*if(PlayerListLeft.Count > PlayerListRight.Count)
        {
            whoIsHitting = PlayerListLeft[0];
        }*/

        //Updates the UI to show everyone who's next 
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

        //Makes the ball possible to hit, but only once
        if ((rb.position.x <= leftEnd || rb.position.x >= rightEnd) && hitOnce == false)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            buttonState = true;
            hitOnce = true;
            rend.sharedMaterial = material[1];
           
        }

        //used to reset the condition of when you can hit
        if(rb.position.x < rightEnd && rb.position.x > leftEnd)
        {
            hitOnce = false;
        }
        //keeps track of the timer before the ball drops
        if (hitOnce)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                rb.useGravity = true;
                buttonState = false;
                rend.sharedMaterial = material[0];
                resetGame = true;
                
            }
        }

        //removes players from the game. Also checks who the winner is
        if ((ballCollision.nrOfBouncesBad >= 1))
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
            
            resetGame = true;
            //Reset();
            
        }
        //This is just copy of the "if" statement above but removes the other person instead youself! I only changed left to right and right to left
        //Will combine the code so it becomes more readable
        if (ballCollision.nrOfBouncesGood > 1)
        {
            rb.useGravity = true;
            buttonState = false;
            rend.sharedMaterial = material[0];
            if (PlayerListLeft.Count <= 1 && PlayerListRight.Count <= 1)
            {
                if (left)
                {
                    CommonCommands.NextGame(PlayerListRight, PlayerListLeft);

                }
                else if (!left)
                {
                    CommonCommands.NextGame(PlayerListLeft, PlayerListRight);
                }
            }
            if (left)
            {
                PlayerListRight.RemoveAt(PlayerListRight.Count - 1);
            }
            else if (!left)
            {
                PlayerListLeft.RemoveAt(PlayerListLeft.Count - 1);
            }

            resetGame = true;
            //Reset();

        }

        //These are the 4 different "hits" you can go and their values
        if ((Input.GetKey("1") || DataStorage.GetSetControllers[whoIsHitting].GetButtonEastPressed ) && buttonState == true)
        {
            Hit(1000, 800, "Normal Hit", 10);
        }
        if ((Input.GetKey("2") || DataStorage.GetSetControllers[whoIsHitting].GetButtonSouthPressed) && buttonState == true)
        {
            Hit(825, 1400, "High Hit", 15);
        }
        if ((Input.GetKey("3") || DataStorage.GetSetControllers[whoIsHitting].GetButtonWestPressed) && buttonState == true)
        {
            Hit(2100, -20, "Smash", -10);
        }
        if ((Input.GetKey("4") || DataStorage.GetSetControllers[whoIsHitting].GetButtonNorthPressed) && buttonState == true)
        {
            Hit(600, 800, "Low Hit", -5);
        }

    }

    public void RemovePlayer()
    {

    }
    //manages the hit
    public void Hit(int x, int y, string hitType, int zr)
    {
        // zr = the rotation for the z angle. I need to rotate it back somehow!
        if (rb.position.x <= leftEnd)
        {

            rb.AddForce(x, y, 0);
            leftSideHit.text = hitType;
            //leftSideHit.transform.Rotate( new Vector3(0, 0, zr));
        }
        else
        {
            rb.AddForce(-x, y, 0);
            rightSideHit.text = hitType;
            //rightSideHit.transform.Rotate(new Vector3(0, 0, zr));
        }

        AfterHit();
    }

    //this method manages everyhing that happens after the ball has been hit. Also manages the queues for the players
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
        timerText = 1f; 
        rb.useGravity = true;
        buttonState = false;
        rend.sharedMaterial = material[0];
        timer = 2;
        timeScale += 0.1f;
        ballCollision.nrOfBouncesGood = 0;
        ballCollision.nrOfBouncesBad = 0;
        Debug.Log("Player nr: " + whoIsHitting + " is Hitting");
    }

    //Resets the ball when a player is removed from the match
    public void Reset()
    {
        rb.transform.position = new Vector3(-50, 10, 0);
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        buttonState = true;
        hitOnce = true;
        rend.sharedMaterial = material[1];
        timeBeforeReset = 5f;
        timer = 3;
    }
}
