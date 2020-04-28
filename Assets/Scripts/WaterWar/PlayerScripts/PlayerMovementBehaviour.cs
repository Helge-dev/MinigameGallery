using UnityEngine;

public class PlayerMovementBehaviour
{
    /*
     * Variables 
     */
    const float jumpForce = 150f,
                moveForce = 0.1f,
                rotationSpeed = 0.2f;
    /*
     * Methods 
     */
    /// <summary>
    /// Call this inside "void FixedUpdate(){...}" to make the object do movement
    /// </summary>
    /// <param name="playerID">The player this object will listen to for inputs</param>
    /// <param name="rigidbody">The Rigidbody perform movement with</param>
    public void DoMovementUpdate(int playerID, Rigidbody rigidbody) => DoPlayerMovementAndRotation(playerID, rigidbody); //Once per physics update. Do movement
    /// <summary>
    /// Process player movement
    /// </summary>
    void DoPlayerMovementAndRotation(int playerID, Rigidbody rb)
    {
        Vector3 movement = DataStorage.GetSetControllers[playerID].GetMovement * moveForce;
        if (movement != Vector3.zero) //If the Players Controller is Moving
        {
            //Move and Rotate
            rb.transform.Translate(movement, Space.World);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }
        if (Physics.Raycast(rb.transform.position, Vector3.down, 0.6f) && (DataStorage.GetSetControllers[playerID].GetButtonSouthDown)) //if button down and on ground, Jump
        {
            rb.AddForce(0, jumpForce, 0); //Jump
        }
    }
}
