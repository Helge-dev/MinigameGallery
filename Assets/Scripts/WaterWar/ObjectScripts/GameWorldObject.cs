using UnityEngine;
public class GameWorldObject : MonoBehaviour
{
    private enum WorldObject
    {
        Washer,
        Table
    }
    [SerializeField] WorldObject go = WorldObject.Table;
    public void Interact(CharacterController controller)
    {
        switch (go)
        {
            case WorldObject.Washer:
                controller.GetComponent<PlayerBehaviour>().FillWaterMeter();
                break;
        }
    }
}
