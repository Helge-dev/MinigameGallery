//Arvid Almquist
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Input.LowLevel;
using System.Runtime.InteropServices.ComTypes;

public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool buttonState = true, hitOnce = true;
    float timer = 9999f, timeScale = 1, timeScaleForTimer = 1, timerText, timeBeforeReset = 5f, timerTextOut = 3f;
    public int rightEnd = 50, leftEnd = -50;
    int whoIsHitting;
    public bool left = true;
    bool resetGame;
    bool canEnd = true;
    [SerializeField]
    Material[] material = null;
    Renderer rend;
    [SerializeField]
    Text playerRight, playerLeft, leftSideHit, rightSideHit, playerOut, buttonMenu, queueLeft, queueRight;
    [SerializeField]
    BallCollision ballCollision = null;
    List<int> playerListLeft = new List<int>();
    List<int> playerListRight = new List<int>();
    Vector3 lastForce = Vector3.zero;
    bool canRemove = true;
    bool twoLeft = false;
    bool justRemoved = false;
    bool ableToRemove = true;


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

        if(playerListLeft.Count + playerListRight.Count == 2)
        {
            twoLeft = true;
        }
        //buttonMenu.text = DataStorage.GetSetControllers[1].GetKeyUsedForSouthButton
    }
    void Update()
    {
        buttonMenu.text = "Player " + whoIsHitting + ": High = " + DataStorage.GetSetControllers[whoIsHitting].GetKeyUsedForSouthButton +
            "   Normal = " + DataStorage.GetSetControllers[whoIsHitting].GetKeyUsedForEastButton + "   Low = " + DataStorage.GetSetControllers[whoIsHitting].GetKeyUsedForNorthButton 
            + "    Smash = " + DataStorage.GetSetControllers[whoIsHitting].GetKeyUsedForWestButton;
        buttonMenu.color = DataStorage.GetSetPlayerColor[whoIsHitting];

        Time.timeScale = timeScale;
        timerText -= Time.deltaTime;
        timerTextOut -= Time.deltaTime;
        queueLeft.text = "";
        foreach(int i in playerListLeft)
        {
            queueLeft.text += "Player: " + i + " ";
        }
        queueRight.text = "";
        foreach (int i in playerListRight)
        {
            queueRight.text += "Player: " + i + " ";
        }


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

        if(timerTextOut <= 0)
        {
            playerOut.text = null;
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
            if (ballCollision.nrOfBouncesBad == 0 && ballCollision.nrOfBouncesGood == 0)
            {
                CheckNoBounce();
            }
            if (!justRemoved)
            {
                lastForce = rb.velocity;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                buttonState = true;
                hitOnce = true;
                rend.sharedMaterial = material[1];
            }
        }

        //used to reset the condition of when you can hit
        if(rb.position.x < rightEnd && rb.position.x > leftEnd)
        {
            hitOnce = false;
        }

        
        //keeps track of the timer before the ball drops
        if (hitOnce && !justRemoved)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                rb.useGravity = true;
                buttonState = false;
                rend.sharedMaterial = material[0];
                rb.velocity = lastForce;

                timer = 9999;
                if (left)
                {
                    playerOut.text = "Player " + playerListLeft[0] + " out! ";
                    timerTextOut = 4f;
                    playerListLeft.RemoveAt(0);
                }
                else if (!left)
                {
                    
                    playerOut.text = "Player " + playerListRight[0] + " out! ";
                    timerTextOut = 4f;
                    playerListRight.RemoveAt(0); 
                }
                resetGame = true;
                //Reset();

            }
        }

        EndGame(playerListLeft, playerListRight, ballCollision.nrOfBouncesBad, 1);
        EndGame(playerListRight, playerListLeft, ballCollision.nrOfBouncesGood, 2);

        //These are the 4 different "hits" you can go and their values
        if (DataStorage.GetSetControllers[whoIsHitting].GetButtonEastPressed  && buttonState == true)
        {
            Hit(1040, 920, "Normal", 10);
        }
        if (DataStorage.GetSetControllers[whoIsHitting].GetButtonSouthPressed && buttonState == true)
        {
            Hit(745, 1580, "High", 15);
        }
        if (DataStorage.GetSetControllers[whoIsHitting].GetButtonWestPressed && buttonState == true)
        {
            Hit(2100, -30, "Smash", -10);
        }
        if (DataStorage.GetSetControllers[whoIsHitting].GetButtonNorthPressed && buttonState == true)
        {
            Hit(1260, 600, "Low", -5);
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
            rightSideHit.transform.eulerAngles = new Vector3(0, 0, zr);
           
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
        timeScaleForTimer += 0.05f;
        timer = 3f * timeScaleForTimer;
        ballCollision.nrOfBouncesGood = 0;
        ballCollision.nrOfBouncesBad = 0;
        ableToRemove = true;
    }

    //Resets the ball when a player is removed from the match
    public void Reset()
    {
        ballCollision.nrOfBouncesGood = 0;
        ballCollision.nrOfBouncesBad = 0;
        rb.velocity = Vector3.zero;
        lastForce = Vector3.zero;
        rb.useGravity = false;
        buttonState = true;
        hitOnce = true;
        rend.sharedMaterial = material[1];
        timeBeforeReset = 5f;
        timer = 9999;
        justRemoved = false;
        if ((playerListLeft.Count + playerListRight.Count) <= 2)
            twoLeft = true;
        if (playerListLeft.Count < playerListRight.Count)
        {
            left = false;
            rb.transform.position = new Vector3(50, 10, 0);
        }
        if (playerListLeft.Count >playerListRight.Count)
        {
            left = true;
            rb.transform.position = new Vector3(-50, 10, 0);
        }
        if (playerListLeft.Count == playerListRight.Count)
        {
            left = true;
            rb.transform.position = new Vector3(-50, 10, 0);
        }
        canRemove = true;
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
                if(playerListRight.Count > 0)
                    whoIsHitting = playerListRight[0];
                left = false;
            }
            else
            {
                if(playerListRight.Count > 0)
                    whoIsHitting = playerListLeft[0];
                left = true;
            }
        }
        
    }

    //Sends the winner to "CommonCommands" or removes players from the list if there are more than 2 players left
    public void EndGame(List<int> firstList, List<int> secondList, int bounces, int howMany)
    {
        if ((bounces >= howMany))
        {
            if ((playerListLeft.Count + playerListRight.Count) <= 2 && canEnd && twoLeft && ableToRemove)
            {
                Time.timeScale = 1;
                timeScale = 1f;
                if (left)
                {
                    if(ballCollision.nrOfBouncesGood > 1)
                        CommonCommands.NextGame(firstList, secondList);
                    else if (ballCollision.nrOfBouncesBad > 0)
                        CommonCommands.NextGame(firstList, secondList);
                }
                else if (!left)
                {
                    if (ballCollision.nrOfBouncesGood > 1)
                        CommonCommands.NextGame(secondList, firstList);
                    else if (ballCollision.nrOfBouncesBad > 0)
                        CommonCommands.NextGame(secondList, firstList);
                }
                canEnd = false;
            }
            else if (canEnd && canRemove && !twoLeft && ableToRemove)
            {
                canRemove = false;
                if (left)
                {
                    timerTextOut = 4f;
                    if(howMany == 1)
                    {
                        playerOut.text = "Player " + playerListLeft[playerListLeft.Count - 1] + " out! ";
                        playerListLeft.RemoveAt(playerListLeft.Count - 1);
                    }
                    else if (howMany == 2)
                    {
                        playerOut.text = "Player " + playerListLeft[0] + " out! ";
                        playerListLeft.RemoveAt(0);
                    }
                }
                else if (!left)
                {
                    timerTextOut = 4f;
                    if (howMany == 1)
                    {
                        playerOut.text = "Player " + playerListRight[playerListRight.Count - 1] + " out! ";
                        playerListRight.RemoveAt(playerListRight.Count - 1);
                    }
                    else if (howMany == 2)
                    {
                        playerOut.text = "Player " + playerListRight[0] + " out! ";
                        playerListRight.RemoveAt(0);
                    }
                }
                justRemoved = true;
                resetGame = true;
                ableToRemove = false;
            }
            if (howMany == 2)
                ballCollision.nrOfBouncesGood = 0;
            else if (howMany == 1)
                ballCollision.nrOfBouncesBad = 0;
        }
    }

    public void CheckNoBounce()
    {
        if ((playerListLeft.Count + playerListRight.Count == 2) && twoLeft )
        {
            if (canEnd && ableToRemove)
            {
                Time.timeScale = 1;
                timeScale = 1f;
                if (rb.position.x <= leftEnd)
                {
                    CommonCommands.NextGame(playerListLeft, playerListRight);
                }
                else if (rb.position.x >= rightEnd)
                {
                    CommonCommands.NextGame(playerListRight, playerListLeft);
                }
                canEnd = false;
            }
        }
        else if(canEnd && canRemove && !twoLeft && ableToRemove)
        {
            //Debug.Log("REMOVE!!!!");
            canRemove = false;
            timerTextOut = 4f;
            if (rb.position.x <= leftEnd)
            {
                playerOut.text = "Player " + playerListLeft[playerListLeft.Count - 1] + " out! ";
                playerListLeft.RemoveAt(playerListLeft.Count - 1);
            }
            else if (rb.position.x >= rightEnd)
            {
                playerOut.text = "Player " + playerListRight[playerListRight.Count - 1] + " out! ";
                playerListRight.RemoveAt(playerListRight.Count - 1);
            }
            resetGame = true;
            justRemoved = true;
            ableToRemove = false;
        }
    }
}
