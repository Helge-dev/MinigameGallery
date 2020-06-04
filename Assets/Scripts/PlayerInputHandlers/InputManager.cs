using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input.Plugins.PlayerInput;
/*
 * Made By Helge Herrström
 */
public class InputManager : MonoBehaviour
{
    /*
     * Variables
     */
    static List<InputManager> keyboards = new List<InputManager>(); // Keyboards has to be handled differently from controllers. Only one keyboard can access input manager and has to register all keyboard players inputs
    Vector2 movement;
    float moveUp, moveDown, moveLeft, moveRight;
    bool buttonSouthPressedLastFrame, buttonNorthPressedLastFrame, buttonEastPressedLastFrame, buttonWestPressedLastFrame, buttonStartPressedLastFrame, buttonSouthDown, buttonNorthDown, buttonEastDown, buttonWestDown, buttonStartDown;
    /*
     * Get Properties
     */
    /// <summary>
    /// Returns a list with all the inputmanagers that is listening to keyboard inputs. 
    /// You can't get input from all players this way, use DataStorage.GetSetControllers instead!
    /// </summary>
    public static List<InputManager> GetKeyboards { get => keyboards; }
    /// <summary>
    /// Returns a direction the player wants to move.
    /// Example code for movement: transform.Translate(DataStorage.GetSetControllers[player_id].GetMovement*Time.deltaTime*force); force is any float
    /// </summary>
    public Vector3 GetMovement { get => new Vector3(movement.x, 0f, movement.y); }
    //Returns true if the related button is pressed
    /// <summary>
    /// Returns true if the South button is pressed on handcontrollers or keyboard
    /// Tips: GetKeyUsedForSouthButton explains which button the user has to press
    /// </summary>
    public bool GetButtonSouthPressed { get => buttonSouthDown && !buttonSouthPressedLastFrame; }
    /// <summary>
    /// Returns true if the North button is pressed on handcontrollers or keyboard
    /// Tips: GetKeyUsedForNorthButton explains which button the user has to press
    /// </summary>
    public bool GetButtonNorthPressed { get => buttonNorthDown && !buttonNorthPressedLastFrame; }
    /// <summary>
    /// Returns true if the East button is pressed on handcontrollers or keyboard
    /// Tips: GetKeyUsedForEastButton explains which button the user has to press
    /// </summary>
    public bool GetButtonEastPressed { get => buttonEastDown && !buttonEastPressedLastFrame; }
    /// <summary>
    /// Returns true if the West button is pressed on handcontrollers
    /// Tips: GetKeyUsedForWestButton explains which button the user has to press
    /// </summary>
    public bool GetButtonWestPressed { get => buttonWestDown && !buttonWestPressedLastFrame; }
    /// <summary>
    /// Returns true if the Start button is pressed on handcontrollers or keyboard
    /// Tips: GetKeyUsedForStartButton explains which button the user has to press
    /// </summary>
    public bool GetButtonStartPressed { get => buttonStartDown && !buttonStartPressedLastFrame; }
    //Returns true if the related button is held down
    /// <summary>
    /// Returns true if the South button is held down by handcontrollers or keyboard
    /// Tips: GetKeyUsedForSouthButton explains which button the user has to press
    /// </summary>
    public bool GetButtonSouthDown { get => buttonSouthDown; }
    /// <summary>
    /// Returns true if the North button is held down by handcontrollers or keyboard
    /// Tips: GetKeyUsedForNorthButton explains which button the user has to press
    /// </summary>
    public bool GetButtonNorthDown { get => buttonNorthDown; }
    /// <summary>
    /// Returns true if the East button is held down by handcontrollers or keyboard
    /// Tips: GetKeyUsedForEastButton explains which button the user has to press
    /// </summary>
    public bool GetButtonEastDown { get => buttonEastDown; }
    /// <summary>
    /// Returns true if the West button is held down by handcontrollers or keyboard
    /// Tips: GetKeyUsedForWestButton explains which button the user has to press
    /// </summary>
    public bool GetButtonWestDown { get => buttonWestDown; }
    /// <summary>
    /// Returns true if the Start button is held down by handcontrollers or keyboard
    /// Tips: GetKeyUsedForStartButton explains which button the user has to press
    /// </summary>
    public bool GetButtonStartDown { get => buttonStartDown; }
    //Returns true if the player wants to move in a direction (For example in a menu)
    /// <summary>
    /// Returns true if dpad/up is held down or keyboard move up key
    /// Tips: GetKeyUsedToMoveUp explains which button the user has to press
    /// </summary>
    public virtual bool GetMoveUp { get => moveUp == 1; }
    /// <summary>
    /// Returns true if dpad/down is held down or keyboard move down key
    /// Tips: GetKeyUsedToMoveDown explains which button the user has to press
    /// </summary>
    public virtual bool GetMoveDown { get => moveDown == -1; }
    /// <summary>
    /// Returns true if dpad/left is held down or keyboard move left key
    /// Tips: GetKeyUsedToMoveLeft explains which button the user has to press
    /// </summary>
    public virtual bool GetMoveLeft { get => moveLeft == -1; }
    /// <summary>
    /// Returns true if dpad/right is held down or keyboard move right key
    /// Tips: GetKeyUsedToMoveRight explains which button the user has to press
    /// </summary>
    public virtual bool GetMoveRight { get => moveRight == 1; }
    /// <summary>
    /// True if the input manager listens to keyboard inputs (Manages all keyboards)
    /// </summary>
    public bool GetIsKeyboardManager { get; private set; } = false;
    /// <summary>
    /// DO NOT SET! if it returns -1 it's not a keyboard. Anything else and it's a keyboard. 
    /// </summary>
    public int SetKeyboardID { get; set; } = -1;
    /*
     * Methods
     */
    /// <summary>
    /// DO NOT CALL! Used to tell the inputmanager to manage keyboard inputs.
    /// </summary>
    public void ListenToKeyboard()
    {
        if (!GetIsKeyboardManager)
        {
            GetIsKeyboardManager = true;
            GameObject go = new GameObject();
            DontDestroyOnLoad(go);
            InputManager im = go.AddComponent<InputManager>();
            im.SetKeyboardID = 0;
            keyboards.Add(im);
        }
    }
    /// <summary>
    /// DO NOT CALL! Returns a new inputmanager if any key of the next player to be added is pressed
    /// For example tfgh has to be pressed for the player that will use those keys to join
    /// </summary>
    /// <returns>A input manager</returns>
    public InputManager CheckForNewKeyboard()
    {
        for (int i = 0; i < 5; i++)
        {
            bool controlConfigurationIsInUse = false;
            foreach (InputManager im in keyboards)
                if (im.SetKeyboardID == i)
                    controlConfigurationIsInUse = true;
            if (!controlConfigurationIsInUse)
            {
                if (InputManagerKeyboard.KeyboardMoveUp(i) || InputManagerKeyboard.KeyboardMoveDown(i) || InputManagerKeyboard.KeyboardMoveLeft(i) || InputManagerKeyboard.KeyboardMoveRight(i))
                {
                    GameObject go = new GameObject();
                    DontDestroyOnLoad(go);
                    InputManager im = go.AddComponent<InputManager>();
                    im.SetKeyboardID = i;
                    keyboards.Add(im);
                    return im;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedForNorthButton => SetKeyboardID == -1 ? "[Y/TRIANGLE]" : InputManagerKeyboard.KeyboardNorthButton(SetKeyboardID);
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedForSouthButton => SetKeyboardID == -1 ? "[A/X]" : InputManagerKeyboard.KeyboardSouthButton(SetKeyboardID);
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedForWestButton => SetKeyboardID == -1 ? "[X/SQUARE]" : InputManagerKeyboard.KeyboardWestButton(SetKeyboardID);
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedForEastButton => SetKeyboardID == -1 ? "[B/CIRCLE]" : InputManagerKeyboard.KeyboardEastButton(SetKeyboardID);
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedForStartButton => SetKeyboardID == -1 ? "[START]" : InputManagerKeyboard.KeyboardStartButton(SetKeyboardID);

    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedToMoveUp => SetKeyboardID == -1 ? "[Left Stick/Dpad Up]" : InputManagerKeyboard.KeyboardMoveUpButton(SetKeyboardID);
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedToMoveDown => SetKeyboardID == -1 ? "[Left Stick/Dpad Down]" : InputManagerKeyboard.KeyboardMoveDownButton(SetKeyboardID);
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedToMoveLeft => SetKeyboardID == -1 ? "[Left Stick/Dpad Left]" : InputManagerKeyboard.KeyboardMoveLeftButton(SetKeyboardID);
    /// <summary>
    /// Returns a string with the key used by the player (For example "W" is walk forward)
    /// </summary>
    public string GetKeyUsedToMoveRight => SetKeyboardID == -1 ? "[Left Stick/Dpad Right]" : InputManagerKeyboard.KeyboardMoveRightButton(SetKeyboardID);

    /// <summary>
    /// Returns detailed information about the movement keys the user is assigned to or controller
    /// </summary>
    /// <returns>Returns key information </returns>
    public string GetRequiredKeyForMovement()
    {
        if (SetKeyboardID != -1) // If keyboard
            return "Movement [Keyboard]\nUp: " + GetKeyUsedToMoveUp + "\nDown: " + GetKeyUsedToMoveDown + "\nLeft: " + GetKeyUsedToMoveLeft + "\nRight: " + GetKeyUsedToMoveRight;
        else // If Controller
            return "Movement [Xbox / Playstation]\nLeft Stick or Dpad";
    }
    /// <summary>
    /// Returns detailed information about the action keys the user is assigned to or controller
    /// </summary>
    /// <returns>Returns key information</returns>
    public string GetRequiredKeyForActions()
    {
        string s = "";
        if (SetKeyboardID != -1) // If keyboard
            s = "Actions [Keyboard]";
        else // If Controller
            s = "Actions [Xbox / Playstation]";
        return s + "\nAction 1: " + GetKeyUsedForNorthButton + "\nAction 2: " + GetKeyUsedForSouthButton + "\nAction 3: " + GetKeyUsedForWestButton + "\nAction 4: " + GetKeyUsedForEastButton + "\nStart: " + GetKeyUsedForStartButton;
    }
    //After Update() is done, remember what buttons was pressed during the Update. (This is done to later know if the player is pressing or holding the button)
    private void LateUpdate()
    {
        buttonSouthPressedLastFrame = buttonSouthDown;
        buttonNorthPressedLastFrame = buttonNorthDown;
        buttonEastPressedLastFrame = buttonEastDown;
        buttonWestPressedLastFrame = buttonWestDown;
        buttonStartPressedLastFrame = buttonStartDown;
    }
    // On Direction Inputs
    void OnMove(InputValue value) => movement = value.Get<Vector2>();

    void OnMoveUp(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnMoveUp(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                if (InputManagerKeyboard.KeyboardMoveUp(SetKeyboardID)) // If the key pressed is on the keyboard
                {
                    movement.y = moveUp = value.Get<float>(); // Tell the keyboard to move up
                } else if(!InputManagerKeyboard.KeyboardMoveDown(SetKeyboardID))
                {
                    movement.y = moveUp = 0; // Tell the keyboard to move up
                }
            }
            else movement.y = moveUp = value.Get<float>(); // Update variables
        }
    }
    void OnMoveDown(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnMoveDown(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                if (InputManagerKeyboard.KeyboardMoveDown(SetKeyboardID)) // If the key pressed is on the keyboard
                {
                    movement.y = moveDown = -value.Get<float>(); // Tell the keyboard to move down
                }
                else if (!InputManagerKeyboard.KeyboardMoveUp(SetKeyboardID))
                {
                    movement.y = moveUp = 0; // Tell the keyboard to move up
                }
            }
            else movement.y = moveDown = -value.Get<float>(); // Update variables
        }
    }
    void OnMoveLeft(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnMoveLeft(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                if (InputManagerKeyboard.KeyboardMoveLeft(SetKeyboardID)) // If the key pressed is on the keyboard
                {
                    movement.x = moveLeft = -value.Get<float>(); // Tell the keyboard to move down
                }
                else if(!InputManagerKeyboard.KeyboardMoveRight(SetKeyboardID))
                {
                    movement.x = moveLeft = 0; // Tell the keyboard to move down
                }
            }
            else movement.x = moveLeft = -value.Get<float>();
        }
    }
    void OnMoveRight(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnMoveRight(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                if (InputManagerKeyboard.KeyboardMoveRight(SetKeyboardID)) // If the key pressed is on the keyboard
                {
                    movement.x = moveRight = value.Get<float>(); // Tell the keyboard to move down
                }
                else if(!InputManagerKeyboard.KeyboardMoveLeft(SetKeyboardID))
                {
                    movement.x = moveRight = 0; // Tell the keyboard to move down
                }
            }
            else movement.x = moveRight = value.Get<float>();
        }
    }
    // On Misc Inputs
    void OnButtonSouth(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnButtonSouth(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                buttonSouthDown = InputManagerKeyboard.KeyboardSouth(SetKeyboardID);
            }
            else buttonSouthDown = value.Get<float>() == 1;
        }
    }
    void OnButtonNorth(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnButtonNorth(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                buttonNorthDown = InputManagerKeyboard.KeyboardNorth(SetKeyboardID); // Tell the keyboard to move down
            }
            else buttonNorthDown = value.Get<float>() == 1;
        }
    }
    void OnButtonEast(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnButtonEast(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                buttonEastDown = InputManagerKeyboard.KeyboardEast(SetKeyboardID); // Tell the keyboard to move down
            }
            else buttonEastDown = value.Get<float>() == 1;
        }
    }
    void OnButtonWest(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnButtonWest(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                buttonWestDown = InputManagerKeyboard.KeyboardWest(SetKeyboardID); // Tell the keyboard to move down
            }
            else buttonWestDown = value.Get<float>() == 1;
        }
    }
    void OnStart(InputValue value)
    {
        if (GetIsKeyboardManager)
        {
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].OnStart(value);
            }
        }
        else
        {
            if (SetKeyboardID != -1) // If a keyboard
            {
                buttonStartDown = InputManagerKeyboard.KeyboardStart(SetKeyboardID); // Tell the keyboard to move down
            }
            else buttonStartDown = value.Get<float>() == 1;
        }
    }
}
