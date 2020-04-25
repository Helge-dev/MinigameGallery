using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerSpawnMananger psm;
    // Start is called before the first frame update
    void Start()
    {
        //Show Game Instructions
        //Create Map
        //Spawn Players
        psm.CreatePlayers();
    }
}
