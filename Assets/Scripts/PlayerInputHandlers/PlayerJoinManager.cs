using UnityEngine;
/*
 * Made By Helge Herrström
 */
public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] PlayerColorManager colorManager;
    void OnPlayerJoined() => SetNewPlayersData(); //When a new controller is connected, Set the name of the controller and other player specific data
    //Set the name of new players and other related data.
    void SetNewPlayersData()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("GameController");
        //Find how many new players has joined.
        int amountOfNewPlayers = 0;
        foreach (GameObject g in go)
        {
            if (g.name == "Controller(Clone)")
            {
                amountOfNewPlayers++;
            }
        }
        //Set the name of new player, (Example names, P1, P2...)
        foreach (GameObject g in go)
        {
            if (g.name == "Controller(Clone)") //If it's a new player
            {
                amountOfNewPlayers--; //Keep count of new players registered
                SetNewPlayerData(g, go.Length-amountOfNewPlayers);
            }
        }
    }
    void SetNewPlayerData(GameObject g, int playerIndex)
    {
        g.name = "P" + playerIndex; //Set name
        DataStorage.GetSetControllers.Add(playerIndex,g.GetComponent<InputManager>()); //Store a reference of controller
        DontDestroyOnLoad(g); //Prevent the new player to be destroyed on future scene loads
        DataStorage.GetSetScore.Add(playerIndex, 0); //Set the players scores
        //Add color to the player
        DataStorage.GetSetPlayerColor.Add(playerIndex, Color.black);
        colorManager.UpdateToNewPlayerImage(playerIndex); // Changes the design in the UI
        colorManager.ChangePlayerColor(playerIndex); 
        Debug.Log("PLAYER " + g.name + " JOINED!");
    }
}
