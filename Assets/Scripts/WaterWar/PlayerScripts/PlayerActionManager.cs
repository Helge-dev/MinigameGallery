using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] GameObject waterBullet = null;
    [SerializeField] ParticleSystem water = null;
    static Transform bulletHolder;
    const float sendCollisionCheckDuration = 0.2f; //The lower the more accurate the collision detection will be of the particle system
    float sendCollisionTimer = 0f; //A timer
    const float fillWaterDuration = 0.2f; //The time it takes to fill one bar of water
    float fillWaterTimer = 0f; //A timer
    const float raycastForwardReach = 1f;
    private void Start()
    {
        bulletHolder = GameObject.FindGameObjectWithTag("BulletHolder").transform;
    }
    /// <summary>
    /// Call this inside "void Update()" to track player button actions
    /// </summary>
    /// <param name="playerID">The player this object will listen to for inputs</param>
    /// <param name="waterMeter">The water meter of the player</param>
    public void DoActionUpdate(int playerID, CharacterController controller, ref int waterMeter)
    {
        sendCollisionTimer += Time.deltaTime;
        fillWaterTimer += Time.deltaTime;
        ShootAction(playerID, controller, ref waterMeter);
        ForwardRaycastUpdate(controller, playerID);
    }
    void ShootAction(int playerID, CharacterController controller, ref int waterMeter)
    {
        if (DataStorage.GetSetControllers[playerID].GetButtonWestDown && waterMeter > 0)
        {
            if (sendCollisionTimer >= sendCollisionCheckDuration) //Send a water collision detection
            {
                Instantiate(waterBullet, water.transform.position + controller.transform.forward * (controller.transform.localScale.z - waterBullet.transform.localScale.z), controller.transform.rotation, bulletHolder);
                sendCollisionTimer = 0;
                waterMeter--;
            }
        }
        else
        {
            water.Play(); // Stop the particle system from emitting particles
        }
    }
    public void FillWaterMeter(ref int waterMeter)
    {
        if (fillWaterTimer >= fillWaterDuration && waterMeter < PlayerBehaviour.GetWaterMeterMAX)
        {
            waterMeter++;
            fillWaterTimer = 0;
        }
    }
    void ForwardRaycastUpdate(CharacterController controller, int playerID)
    {
        if (DataStorage.GetSetControllers[playerID].GetButtonNorthDown)
        {
            Ray ray = new Ray(controller.transform.position, controller.transform.TransformDirection(Vector3.forward));
            RaycastHit info = new RaycastHit();
            if (Physics.Raycast(ray, out info, raycastForwardReach))
            {
                UpdateInteractionWithObjects(controller, info, playerID);
            }
        }
    }
    void UpdateInteractionWithObjects(CharacterController controller, RaycastHit info, int playerID)
    {
        switch (info.collider.tag)
        {
            case "PlayerInteractable":
                GameWorldObject wObject = info.collider.GetComponent<GameWorldObject>();
                wObject.Interact(controller);
                break;
            case "Player":
                if (DataStorage.GetSetControllers[playerID].GetButtonNorthPressed) // If the button was pressed
                {
                    GetComponent<PlayerBehaviour>().ToggleGrab(info.collider.GetComponent<PlayerBehaviour>().gameObject);
                }
                break;
            case "Damageable":
                info.collider.transform.parent.GetComponent<PlayerBehaviour>().GetSetPlayerOutOfGame = true;
                break;
        }
    }
}
