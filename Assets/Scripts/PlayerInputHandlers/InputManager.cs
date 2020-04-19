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
    //Returns how the player wants to move. Example code for movement: gameObject.transform.Translate(GetMovement*Time.deltaTime*force); force is any float
    public Vector3 GetMovement { get => new Vector3(movement.x,0f,movement.y); } 
    //Returns true if the related button is pressed
    public bool GetButtonSouthPressed { get => buttonSouthDown && !buttonSouthPressedLastFrame; }
    public bool GetButtonNorthPressed { get => buttonNorthDown && !buttonNorthPressedLastFrame; }
    public bool GetButtonEastPressed { get => buttonEastDown && !buttonEastPressedLastFrame; }
    public bool GetButtonWestPressed { get => buttonWestDown && !buttonWestPressedLastFrame; }
    public bool GetButtonStartPressed { get => buttonStartDown && !buttonStartPressedLastFrame; }
    //Returns true if the related button is held down
    public bool GetButtonSouthDown { get => buttonSouthDown; }
    public bool GetButtonNorthDown { get => buttonNorthDown; }
    public bool GetButtonEastDown { get => buttonEastDown; }
    public bool GetButtonWestDown { get => buttonWestDown; }
    public bool GetButtonStartDown { get => buttonStartDown; }
    //Returns true if the player wants to move in a direction (For example in a menu)
    public bool GetMoveUp { get => moveUp == 1; }
    public bool GetMoveDown { get => moveDown == -1; }
    public bool GetMoveLeft { get => moveLeft == -1; }
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
