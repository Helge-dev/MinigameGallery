using UnityEngine;

public class PlayerMovementBehaviour
{
    /*
     * Variables 
     */
    const float jumpForce = 0.2f,
                moveForce = 0.1f,
                rotationSpeed = 0.2f,
                gravity = 0.02f,
                raycastForwardReach = 1f;
    float velocityY = 0;
    
    /*
     * Methods 
     */
    /// <summary>
    /// Call this inside "void FixedUpdate(){...}" to make the object do movement
    /// </summary>
    /// <param name="playerID">The player this object will listen to for inputs</param>
    /// <param name="rigidbody">The Rigidbody perform movement with</param>
    public void DoMovementUpdate(int playerID, CharacterController controller) => DoPlayerMovementAndRotation(playerID, controller); //Once per physics update. Do movement
    /// <summary>
    /// Process player movement
    /// </summary>
    void DoPlayerMovementAndRotation(int playerID, CharacterController controller)
    {
        Vector3 movement = DataStorage.GetSetControllers[playerID].GetMovement * moveForce;
        if (movement != Vector3.zero)
        {
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }
        if (controller.isGrounded)
        {
            if (DataStorage.GetSetControllers[playerID].GetButtonSouthDown) //if button down and on ground, Jump
            {
                velocityY = jumpForce;
            }
        }
        else
        {
            velocityY -= gravity;
        }
        controller.Move(movement + new Vector3(0,velocityY,0));
    }
}
