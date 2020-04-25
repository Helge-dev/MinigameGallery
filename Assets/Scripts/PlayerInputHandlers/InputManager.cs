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
    Vector2 movement;
    float moveUp, moveDown, moveLeft, moveRight;
    bool buttonSouthPressedLastFrame, buttonNorthPressedLastFrame, buttonEastPressedLastFrame, buttonWestPressedLastFrame, buttonStartPressedLastFrame, buttonSouthDown, buttonNorthDown, buttonEastDown, buttonWestDown, buttonStartDown;
    /*
     * Get Properties
     */
    /// <summary>
    /// Returns a direction the player wants to move. Example code for movement: gameObject.transform.Translate(GetMovement*Time.deltaTime*force); force is any float
    /// </summary>
    public Vector3 GetMovement { get => new Vector3(movement.x,0f,movement.y); } 
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
    public bool GetMoveUp { get => moveUp == 1; }
    /// <summary>
    /// Returns true if dpad/down is held down (Keyboard arrow/down)
    /// </summary>
    public bool GetMoveDown { get => moveDown == -1; }
    /// <summary>
    /// Returns true if dpad/left is held down (Keyboard arrow/left)
    /// </summary>
    public bool GetMoveLeft { get => moveLeft == -1; }
    /// <summary>
    /// Returns true if dpad/right is held down (Keyboard arrow/right)
    /// </summary>
    public bool GetMoveRight { get => moveRight == 1; }
    /*
     * Methods
     */
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
    void OnMoveUp(InputValue value) => movement.y = moveUp = value.Get<float>();
    void OnMoveDown(InputValue value) => movement.y = moveDown = -value.Get<float>();
    void OnMoveLeft(InputValue value) => movement.x = moveLeft = -value.Get<float>();
    void OnMoveRight(InputValue value) => movement.x = moveRight = value.Get<float>();
    // On Misc Inputs
    void OnButtonSouth(InputValue value) => buttonSouthDown = value.Get<float>() == 1;
    void OnButtonNorth(InputValue value) => buttonNorthDown = value.Get<float>() == 1;
    void OnButtonEast(InputValue value) => buttonEastDown = value.Get<float>() == 1;
    void OnButtonWest(InputValue value) => buttonWestDown = value.Get<float>() == 1;
    void OnStart(InputValue value) => buttonStartDown = value.Get<float>() == 1;
}
