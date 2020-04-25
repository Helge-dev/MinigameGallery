﻿using UnityEngine;
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
        for (int id = 1; id <= DataStorage.GetSetControllers.Count; id++)
        {
            GameObject player = Instantiate(playerObject, new Vector3(0,(playerObject.transform.localScale.y/2f),0), Quaternion.identity, playerTransform);
            player.GetComponent<PlayerBehaviour>().GetSetPlayerID = id;
            player.GetComponent<Renderer>().material.color = DataStorage.GetSetPlayerColor[id];
        }
    }
}