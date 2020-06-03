using UnityEngine;

public class GrabMovement
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

    CharacterController controller = null;

    GameObject grabbedObj = null;
    int grabbingObjID = 0, grabbedObjID = 0;
    Animator grabbingObjAnimator = null, grabbedObjAnimator = null;

    Vector2 firstPositionLastUpdate, secondPositionLastUpdate; // The position of the objects last update
    Transform firstObjDefaultTransform, secondObjDefaultTransform;

    public bool GetSetIsGrabbing { get; private set; } = false;
    public bool GetSetIsGrabbed { get; set; } = false;

    public void StopGrabbing()
    {
        if (GetSetIsGrabbing)
        {
            grabbedObj.transform.SetParent(secondObjDefaultTransform);
            GetSetIsGrabbing = false;
            controller.center = new Vector3(0, 0.2f, -0.2f);
            controller.radius = controller.radius / 2.5f;
        }
    }
    public void StartMovement(GameObject grabbingObj, GameObject grabbedObj)
    {
        if (!GetSetIsGrabbed)
        {
            controller = grabbingObj.GetComponent<CharacterController>();
            controller.center = new Vector3(0, 0.2f, 0.2f);
            controller.radius = controller.radius * 2.5f;
            GetSetIsGrabbing = true;
            grabbingObjID = grabbingObj.GetComponent<PlayerBehaviour>().GetSetPlayerID;
            grabbedObjID = grabbedObj.GetComponent<PlayerBehaviour>().GetSetPlayerID;
            firstPositionLastUpdate = grabbingObj.transform.position;
            secondPositionLastUpdate = grabbedObj.transform.position;
            // Change parent transform of players
            firstObjDefaultTransform = grabbingObj.transform.parent;
            secondObjDefaultTransform = grabbedObj.transform.parent;
            grabbingObj.transform.LookAt(grabbedObj.transform);
            grabbedObj.transform.LookAt(grabbingObj.transform);
            grabbedObj.transform.SetParent(grabbingObj.transform);
            grabbingObjAnimator = grabbingObj.GetComponentInChildren<Animator>();
            grabbedObjAnimator = grabbedObj.GetComponentInChildren<Animator>();
            this.grabbedObj = grabbedObj;
        }
    }
    // Update is called once per frame
    public void DoMovementUpdate()
    {
        //Create a gameobject in middle of the players and attach the two. Attach a character controller to the object. 
        Vector3 movement = DataStorage.GetSetControllers[grabbingObjID].GetMovement + DataStorage.GetSetControllers[grabbedObjID].GetMovement;
        movement.Normalize();
        movement *= moveForce;
        Debug.Log(movement);
        //They jump together
        if (movement != Vector3.zero)
        {
            grabbingObjAnimator.SetBool("Walking", true);
            grabbedObjAnimator.SetBool("Walking", true);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }
        else
        {
            grabbingObjAnimator.SetBool("Walking", false);
            grabbedObjAnimator.SetBool("Walking", false);
        }
        if (controller.isGrounded)
        {
            if (DataStorage.GetSetControllers[grabbingObjID].GetButtonSouthDown || DataStorage.GetSetControllers[grabbedObjID].GetButtonSouthDown) //if button down and on ground, Jump
            {
                velocityY = jumpForce;
            }
        }
        else
        {
            velocityY -= gravity;
        }
        controller.Move(movement + new Vector3(0, velocityY, 0));
    }
}
