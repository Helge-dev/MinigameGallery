using UnityEngine;

public class TagCollisionBehaviour : MonoBehaviour
{
    readonly string bulletTag = "Bullet";
    [SerializeField] PlayerBehaviour playerBehaviour;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == bulletTag)
        {
            float angle = (collision.gameObject.transform.rotation.y - transform.rotation.y);
            if (angle*Mathf.Rad2Deg <= 45 && angle*Mathf.Rad2Deg >= -45) //The tag has to be hit from behind
            {
                playerBehaviour.GetSetPlayerOutOfGame = true;
                Destroy(collision.gameObject);
            }
        }
    }
}
