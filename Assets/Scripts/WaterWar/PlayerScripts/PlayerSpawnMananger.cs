using System.Collections.Generic;
using UnityEngine;
/*
 * Created by Helge Herrström 
 */
public class PlayerSpawnMananger : MonoBehaviour
{
    [SerializeField] GameObject playerObject = null;
    [SerializeField] Transform playerTransform = null;
    /// <summary>
    /// Returns the transform all players are placed in
    /// </summary>
    public Transform GetPlayerTransform { get => playerTransform; }
    /// <summary>
    /// The key is the players controller ID and value is the players character
    /// </summary>
    public SortedDictionary<int, GameObject> GetSetPlayers { get; set; } = new SortedDictionary<int, GameObject>();
    /// <summary>
    /// Creates a player for each controller registered
    /// </summary>
    public void CreatePlayers()
    {
        for (int id = 1; id <= DataStorage.GetSetControllers.Count; id++)
        {
            GameObject player = Instantiate(playerObject, new Vector3(id*playerObject.transform.localScale.x,(playerObject.transform.localScale.y/2f),0), Quaternion.identity, playerTransform);
            player.GetComponent<PlayerBehaviour>().GetSetPlayerID = id;
            player.GetComponent<ColorCustomizer>().SetPlayerColor(DataStorage.GetSetPlayerColor[id]);
            GetSetPlayers.Add(id, player);
        }
        GetComponent<TeamManager>().PlacePlayersInTeams(this);
    }
    
}
