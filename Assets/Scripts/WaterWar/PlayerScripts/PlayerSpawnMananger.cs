using UnityEngine;
/*
 * Created by Helge Herrström 
 */
public class PlayerSpawnMananger : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;
    /// <summary>
    /// Creates a player for each controller registered
    /// </summary>
    public void CreatePlayers()
    {
        for (int i = 0; i < DataStorage.GetSetControllers.Count-1; i++)
        {
            Instantiate(playerObject, playerTransform);
        }
    }
}
