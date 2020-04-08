using UnityEngine;
/*
 * Made By Helge Herrström
 */
public class PlayerJoinManager : MonoBehaviour
{
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
                g.name = "P" + (go.Length - (amountOfNewPlayers-1)); //Set name
                amountOfNewPlayers--; //Keep count of new players registered
                DontDestroyOnLoad(g); //Prevent the new player to be destroyed on future scene loads
                DataStorage.GetSetScore.Add(g.name,0); //Set the players scores
                Debug.Log("PLAYER " + g.name + " JOINED!");
            }
        }
    }
}
