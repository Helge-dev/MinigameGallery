﻿using UnityEngine;
/*
 * Made by Helge Herrström
 */
 /*
  * This is the main class for Handling a player character
  */
public class PlayerBehaviour : MonoBehaviour
{
    /*
     * Variables
     */
    [SerializeField] CharacterController controller = null; //PlayerObjects Rigidbody used for physics
    [SerializeField] PlayerActionManager actionManager = null; //Action Manager class
    readonly PlayerMovementBehaviour movementB = new PlayerMovementBehaviour(); //Movement Manager class
    const int waterMeterMax = 10; // Max value of Water Meter
    int waterMeter = waterMeterMax; // Water Meter value
    bool outOfGame = false; // If player is out of game
    /// <summary>
    /// Returns the player id this object listens for inputs.
    /// </summary>
    public int GetSetPlayerID { get; set; }
    /// <summary>
    /// Returns true if the player is out of game
    /// </summary>
    public bool GetSetPlayerOutOfGame
    {
        get
        {
            return outOfGame;
        }
        set
        {
            gameObject.SetActive(!value);
            outOfGame = value;
        }
    }
    /// <summary>
    /// returns how much of this players water meter is filled.
    /// </summary>
    public int GetWaterMeter
    {
        get => waterMeter;
    }
    public static int GetWaterMeterMAX
    {
        get => waterMeterMax;
    }
    /*
     * Methods 
     */
    void Update()
    {
        if (GetSetPlayerOutOfGame) //If player out of game
        {
            PlayerOutOfGameUpdate();
        }
        else //If player in game
        {
            PlayerInGameUpdate();
        }
    }
    /// <summary>
    /// The update when the player is out of the game
    /// </summary>
    void PlayerOutOfGameUpdate() {}
    /// <summary>
    /// The update when the player is in game
    /// </summary>
    void PlayerInGameUpdate()
    {
        actionManager.DoActionUpdate(GetSetPlayerID, controller, ref waterMeter);
    }
    void FixedUpdate()
    {
        movementB.DoMovementUpdate(GetSetPlayerID, controller); //Do Movement
    }
    public void FillWaterMeter() => actionManager.FillWaterMeter(ref waterMeter);
}
