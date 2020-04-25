using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody rigidbody;
    const float jumpForce = 200f,
                moveForce = 0.1f;
    int playerID; //The player this object listens to
    /// <summary>
    /// Returns the player his object listens to
    /// </summary>
    public int GetSetPlayerID
    {
        get => playerID;
        set => playerID = value;
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        Vector3 movement = DataStorage.GetSetControllers[playerID].GetMovement * moveForce;
        if (movement != Vector3.zero)
        {
            MoveAndRotate(movement);
        }
        if (Physics.Raycast(transform.position, Vector3.down, 0.6f) && (DataStorage.GetSetControllers[playerID].GetButtonSouthDown)) //if button down and on ground, Jump
        {
            rigidbody.AddForce(0, jumpForce, 0); //Jump
        }
    }
    //Move and rotate object
    void MoveAndRotate(Vector3 movement)
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.Translate(movement);
        transform.LookAt(transform.position + movement);
    }
}
