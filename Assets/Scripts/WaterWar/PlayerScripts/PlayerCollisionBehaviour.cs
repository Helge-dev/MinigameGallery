using UnityEngine;
/*
 * Made by Helge Herrström
 */
public class PlayerCollisionBehaviour : MonoBehaviour
{
    readonly string bulletTag = "Bullet";
    void OnCollisionEnter(Collision collision)
    {
        //Do actions depending on the colliding object
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                break;
        }
    }
}
