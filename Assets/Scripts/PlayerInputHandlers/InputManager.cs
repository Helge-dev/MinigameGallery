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
    /// Use DataStorage.GetSetControllers instead!
    /// </summary>
    public static List<InputManager> GetKeyboards { get => keyboards; }
    /// <summary>
    /// Returns a direction the player wants to move. 
    /// Example code for movement: gameObject.transform.Translate(GetMovement*Time.deltaTime*force); force is any float
    /// </summary>
    public Vector3 GetMovement { get => new Vector3(movement.x, 0f, movement.y); }
    //Returns true if the related button is pressed
    /// <summary>
    /// Returns true if the South button is pressed (Keyboard a)
    /// </summary>
    public bool GetButtonSouthPressed { get => buttonSouthDown && !buttonSouthPressedLastFrame; }
    /// <summary>
    /// Returns true if the North button is pressed (Keyboard d)
    /// </summary>
    public bool GetButtonNorthPressed { get => buttonNorthDown && !buttonNorthPressedLastFrame; }
    /// <summary>
    /// Returns true if the East button is pressed (Keyboard f)
    /// </summary>
    public bool GetButtonEastPressed { get => buttonEastDown && !buttonEastPressedLastFrame; }
    /// <summary>
    /// Returns true if the West button is pressed (Keyboard s)
    /// </summary>
    public bool GetButtonWestPressed { get => buttonWestDown && !buttonWestPressedLastFrame; }
    /// <summary>
    /// Returns true if the Start button is pressed (Keyboard Enter)
    /// </summary>
    public bool GetButtonStartPressed { get => buttonStartDown && !buttonStartPressedLastFrame; }
    //Returns true if the related button is held down
    /// <summary>
    /// Returns true if the South button is held down (Keyboard a)
    /// </summary>
    public bool GetButtonSouthDown { get => buttonSouthDown; }
    /// <summary>
    /// Returns true if the North button is held down (Keyboard d)
    /// </summary>
    public bool GetButtonNorthDown { get => buttonNorthDown; }
    /// <summary>
    /// Returns true if the East button is held down (Keyboard f)
    /// </summary>
    public bool GetButtonEastDown { get => buttonEastDown; }
    /// <summary>
    /// Returns true if the West button is held down (Keyboard s)
    /// </summary>
    public bool GetButtonWestDown { get => buttonWestDown; }
    /// <summary>
    /// Returns true if the Start button is held down (Keyboard Enter)
    /// </summary>
    public bool GetButtonStartDown { get => buttonStartDown; }
    //Returns true if the player wants to move in a direction (For example in a menu)
    /// <summary>
    /// Returns true if dpad/up is held down (Keyboard arrow/up)
    /// </summary>
    public virtual bool GetMoveUp { get => moveUp == 1; }
    /// <summary>
    /// Returns true if dpad/down is held down (Keyboard arrow/down)
    /// </summary>
    public virtual bool GetMoveDown { get => moveDown == -1; }
    /// <summary>
    /// Returns true if dpad/left is held down (Keyboard arrow/left)
    /// </summary>
    public virtual bool GetMoveLeft { get => moveLeft == -1; }
    /// <summary>
    /// Returns true if dpad/right is held down (Keyboard arrow/right)
    /// </summary>
    public virtual bool GetMoveRight { get => moveRight == 1; }
    /// <summary>
    /// True if the input manager listens to keyboard inputs
    /// </summary>
    public bool GetIsKeyboard { get; private set; } = false;
    /// <summary>
    /// DO NOT CALL!
    /// </summary>
    public int SetKeyboardID { get; set; } = -1;
    /*
     * Methods
     */
    /// <summary>
    /// DO NOT CALL WITHOUT KNOWING HOW TO USE IT! Tells the inputmanager to manage keyboard inputs.
    /// </summary>
    public void ListenToKeyboard()
    {
        if (!GetIsKeyboard)
        {
            GetIsKeyboard = true;
            GameObject go = new GameObject();
            DontDestroyOnLoad(go);
            InputManager im = go.AddComponent<InputManager>();
            im.SetKeyboardID = 0;
            keyboards.Add(im);
        }
    }
    /// <summary>
    /// DO NOT CALL WITHOUT KNOWING HOW TO USE IT! Returns a new inputmanager if any key of the next player to be added is pressed
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
    /// Returns the input the player need to move
    /// </summary>
    /// <returns>A displayable string telling the input needed. For example. left stick</returns>
    public string GetRequiredKeyForMovement()
    {
        if (SetKeyboardID != -1) // If keyboard
        {
            return "Up: " + InputManagerKeyboard.KeyboardMoveUpButton(SetKeyboardID) + "\nDown: " + InputManagerKeyboard.KeyboardMoveDownButton(SetKeyboardID) + "\nLeft: " + InputManagerKeyboard.KeyboardMoveLeftButton(SetKeyboardID) + "\nRight: " + InputManagerKeyboard.KeyboardMoveRightButton(SetKeyboardID);
        }
        else // If Hand Controller
        {
            return "Movement: Left Stick or Dpad";
        }
    }
    public string GetRequiredKeyForActions()
    {
        if (SetKeyboardID != -1) // If keyboard
        {
            return "Action 1: " + InputManagerKeyboard.KeyboardNorthButton(SetKeyboardID) + "\nAction 2: " + InputManagerKeyboard.KeyboardSouthButton(SetKeyboardID) + "\nAction 3: " + InputManagerKeyboard.KeyboardWestButton(SetKeyboardID) + "\nAction 4: " + InputManagerKeyboard.KeyboardEastButton(SetKeyboardID);
        }
        else // If Hand Controller
        {
            return "[Xbox/Playstation]\nAction 1:[A/X]\nAction 2:[X/SQUARE]\nAction 3:[Y/TRIANGLE]\nAction 4:[B/CIRCLE]";
        }
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
        if (GetIsKeyboard)
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
                }
                else
                {
                    movement.y = moveUp = 0; // Tell the keyboard to move up
                }
            }
            else movement.y = moveUp = value.Get<float>(); // Update variables
        }
    }
    void OnMoveDown(InputValue value)
    {
        if (GetIsKeyboard)
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
                else
                {
                    movement.y = moveDown = 0; // Tell the keyboard to move down
                }
            }
            else movement.y = moveDown = -value.Get<float>(); // Update variables
        }
    }
    void OnMoveLeft(InputValue value)
    {
        if (GetIsKeyboard)
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
                else
                {
                    movement.x = moveLeft = 0; // Tell the keyboard to move down
                }
            }
            else movement.x = moveLeft = -value.Get<float>();
        }
    }
    void OnMoveRight(InputValue value)
    {
        if (GetIsKeyboard)
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
                else
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
        if (GetIsKeyboard)
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
        if (GetIsKeyboard)
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
        if (GetIsKeyboard)
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
        if (GetIsKeyboard)
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
        if (GetIsKeyboard)
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
