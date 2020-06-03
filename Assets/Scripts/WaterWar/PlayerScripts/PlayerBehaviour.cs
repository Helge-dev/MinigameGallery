using UnityEngine;
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
    readonly GrabMovement movementG = new GrabMovement();
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
            UIManager.SetDisplayedPlayerOut = GetSetPlayerID;
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
        if (!GetSetPlayerOutOfGame) //If player in game
        {
            actionManager.DoActionUpdate(GetSetPlayerID, controller, ref waterMeter); // Check if the player is doing a action
        }
    }
    void FixedUpdate()
    {
        if (movementG.GetSetIsGrabbing) // If grabbed or is grabbing
        {
            movementG.DoMovementUpdate(); // Do Grabbed Movement
        }
        else if(!movementG.GetSetIsGrabbed) // If not grabbed or grabbing
        {
            movementB.DoMovementUpdate(GetSetPlayerID, controller); //Do Normal Movement
        }
    }
    public void FillWaterMeter() => actionManager.FillWaterMeter(ref waterMeter);
    public void ToggleGrab(GameObject grabbedObject)
    {
        grabbedObject.GetComponent<PlayerBehaviour>().ToggleIsGrabbed();
        if (movementG.GetSetIsGrabbing)
        {
            movementG.StopGrabbing();
        }
        else
        {
            movementG.StartMovement(gameObject, grabbedObject);
        }
    }
    public void ToggleIsGrabbed()
    {
        movementG.GetSetIsGrabbed = !movementG.GetSetIsGrabbed;
    }
}
