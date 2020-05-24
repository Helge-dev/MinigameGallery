﻿//Arvid Almquist
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Input.LowLevel;

public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool buttonState = true, hitOnce = true;
    public float timer = 9999f, timeScale, timerText, timeBeforeReset = 5f;
    public int rightEnd = 50, leftEnd = -50;
    int whoIsHitting;
    public bool left = true;
    bool resetGame;
    bool canEnd = true;
    [SerializeField]
    Material[] material = null;
    Renderer rend;
    [SerializeField]
    Text playerRight, playerLeft, leftSideHit, rightSideHit;
    [SerializeField]
    BallCollision ballCollision = null;
    List<int> playerListLeft = new List<int>();
    List<int> playerListRight = new List<int>();
    Vector3 lastForce = Vector3.zero;


    //does things when scene starts
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
                    playerListLeft.Add(i);
                }
                else
                {
                    playerListRight.Add(i);
                }

                count++;
            }
            Console.WriteLine(playerListLeft.Count + playerListRight.Count);
        }

        whoIsHitting = playerListLeft[0];
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

        //Updates the UI to show everyone who's next 
        if (playerListRight.Count != 0)
        {
            playerRight.text = "Player " + playerListRight[0];
            playerRight.color = DataStorage.GetSetPlayerColor[playerListRight[0]];
            
        }
        if (playerListLeft.Count != 0)
        {
            playerLeft.text = "Player " + playerListLeft[0];
            playerLeft.color = DataStorage.GetSetPlayerColor[playerListLeft[0]];
        }

        //Makes the ball possible to hit, but only once
        if ((rb.position.x <= leftEnd || rb.position.x >= rightEnd) && hitOnce == false)
        {
            lastForce = rb.velocity;
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
                rb.velocity = lastForce;
                resetGame = true;
                
            }
        }

        EndGame(playerListLeft, playerListRight, ballCollision.nrOfBouncesBad, 1);
        EndGame(playerListRight, playerListLeft, ballCollision.nrOfBouncesGood, 2);


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

    //manages the hit
    public void Hit(int x, int y, string hitType, int zr)
    {
        if (rb.position.x <= leftEnd)
        {

            rb.AddForce(x, y, 0);
            leftSideHit.text = hitType;
            leftSideHit.transform.eulerAngles = new Vector3(0, 0, zr);
        }
        else
        {
            rb.AddForce(-x, y, 0);
            rightSideHit.text = hitType;
            leftSideHit.transform.eulerAngles = new Vector3(0, 0, zr);
        }

        AfterHit();
    }

    //this method manages everyhing that happens after the ball has been hit. Also manages the queues for the players
    public void AfterHit()
    {
        FindObjectOfType<AudioManager>().Play("Hit");
        QueueHandler();
        timerText = 1f; 
        rb.useGravity = true;
        buttonState = false;
        rend.sharedMaterial = material[0];
        timer = 3;
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

    public void QueueHandler()
    {
        if (playerListLeft.Count + playerListRight.Count > 2 || playerListLeft.Count + playerListRight.Count < 2)
        {
            if (left)
            {
                playerListRight.Add(playerListLeft[0]);
                playerListLeft.RemoveAt(0);
                whoIsHitting = playerListRight[0];
                left = false;
            }
            else
            {
                playerListLeft.Add(playerListRight[0]);
                playerListRight.RemoveAt(0);
                whoIsHitting = playerListLeft[0];
                left = true;
            }
        }
        else
        {
            if (left)
            {
                //PlayerListRight.Add(PlayerListLeft[0]);
                //PlayerListLeft.RemoveAt(0);

                if(playerListRight.Count > 0)
                    whoIsHitting = playerListRight[0];
                left = false;
            }
            else
            {
                //PlayerListLeft.Add(PlayerListRight[0]);
                //PlayerListRight.RemoveAt(0);

                if(playerListRight.Count > 0)
                    whoIsHitting = playerListLeft[0];
                left = true;
            }
        }
    }

    //Sens the winner to "CommonCommands" or removes players from the list if there are more than 2 players left
    public void EndGame(List<int> firstList, List<int> secondList, int bounces, int howMany)
    {
        if ((bounces >= howMany))
        {
            rb.useGravity = true;
            buttonState = false;
            rend.sharedMaterial = material[0];
            if (playerListLeft.Count <= 1 && playerListRight.Count <= 1 && canEnd)
            {
                if (left)
                {
                    CommonCommands.NextGame(firstList, secondList);

                }
                else if (!left)
                {
                    CommonCommands.NextGame(secondList, firstList);
                }
                canEnd = false;
            }
            else if (canEnd)
            {
                if (left)
                {
                    playerListLeft.RemoveAt(playerListLeft.Count - 1);
                }
                else if (!left)
                {
                    playerListRight.RemoveAt(playerListRight.Count - 1);
                }
            }

            resetGame = true;

        }
    }
}
