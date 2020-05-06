using UnityEngine;
public class GameWorldObject : MonoBehaviour
{
    private enum WorldObject
    {
        Washer,
        Table
    }
    [SerializeField] WorldObject go;
    void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                switch (go)
                {
                    case WorldObject.Table:
                        collision.gameObject.transform.localScale = Vector3.zero;
                        break;
                    case WorldObject.Washer:
                        collision.gameObject.GetComponent<PlayerBehaviour>().FillWaterMeter();
                        break;
                }
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                switch (go)
                {
                    case WorldObject.Table:
                        collision.gameObject.transform.localScale = Vector3.one;
                        break;
                }
                break;
        }
    }
}
