using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] GameObject waterBullet;
    [SerializeField] ParticleSystem water;
    static Transform bulletHolder;
    const float sendCollisionCheckDuration = 0.2f; //The lower the more accurate the collision detection will be of the particle system
    float sendCollisionTimer = 0f; //A timer
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
        ShootAction(playerID, controller, ref waterMeter);
    }
    void ShootAction(int playerID, CharacterController controller, ref int waterMeter)
    {
        if (DataStorage.GetSetControllers[playerID].GetButtonWestDown)
        {
            if (sendCollisionTimer >= sendCollisionCheckDuration) //Send a water collision detection
            {
                Instantiate(waterBullet, water.transform.position + controller.transform.forward * (controller.transform.localScale.z - waterBullet.transform.localScale.z), controller.transform.rotation, bulletHolder);
                sendCollisionTimer = 0;
            }
        }
        else
        {
            water.Play(); // Stop the particle system from emitting particles
        }
    }
}
