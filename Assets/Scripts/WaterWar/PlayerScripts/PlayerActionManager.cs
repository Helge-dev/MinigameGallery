using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] GameObject waterBullet;
    /// <summary>
    /// Call this inside "void Update()" to track player button actions
    /// </summary>
    /// <param name="playerID">The player this object will listen to for inputs</param>
    /// <param name="waterMeter">The water meter of the player</param>
    public void DoActionUpdate(int playerID, Rigidbody rigidbody, ref int waterMeter)
    {
        ShootAction(playerID, rigidbody, ref waterMeter);
    }
    void ShootAction(int playerID, Rigidbody rigidbody, ref int waterMeter)
    {
        if (DataStorage.GetSetControllers[playerID].GetButtonWestDown)
        {
            Instantiate(waterBullet, rigidbody.transform.position + rigidbody.transform.forward*(rigidbody.transform.localScale.z - waterBullet.transform.localScale.z), rigidbody.rotation, GameObject.FindGameObjectWithTag("BulletHolder").transform);
        }
    }
}
