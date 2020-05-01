using UnityEngine;

public class TagCollisionBehaviour : MonoBehaviour
{
    string bulletTag = "Bullet";
    [SerializeField] PlayerBehaviour playerBehaviour;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == bulletTag)
        {
            playerBehaviour.GetSetPlayerOutOfGame = true;
            Destroy(collision.gameObject);
        }
    }
}
