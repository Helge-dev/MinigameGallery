using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    const float jumpForce = 1f,
                moveForce = 10f;
    int playerID; //The player this object listens to
    /// <summary>
    /// Returns the player his object listens to
    /// </summary>
    public int GetSetPlayerID
    {
        get => playerID;
        set => playerID = value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DoPlayerMovementAndRotation();
    }
    /// <summary>
    /// Process player movement
    /// </summary>
    void DoPlayerMovementAndRotation()
    {
        if (DataStorage.GetSetControllers[playerID].GetMovement != Vector3.zero)
        {
            if (DataStorage.GetSetControllers[playerID].GetButtonSouthDown) //if button down, Jump and Walk
            {
                MoveAndRotate(DataStorage.GetSetControllers[playerID].GetMovement * moveForce + new Vector3(0, jumpForce, 0));
            }
            else //Only Walk
            {
                transform.Translate(DataStorage.GetSetControllers[playerID].GetMovement * moveForce);
            }
        }
        
    }
    //Move and rotate object
    void MoveAndRotate(Vector3 movement)
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.Translate(movement);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(DataStorage.GetSetControllers[playerID].GetMovement.x, DataStorage.GetSetControllers[playerID].GetMovement.z) * Mathf.Rad2Deg, 0);
    }
}
