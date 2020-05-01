using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerSpawnMananger psm;
    [SerializeField] TeamManager tm;
    bool gameEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        //Show Game Instructions
        //Create Map
        //Spawn Players
        psm.CreatePlayers();
    }
    void Update()
    {
        CheckIfGameDone();
    }

    void CheckIfGameDone()
    {
        if (!gameEnded) //If game not ended
        {
            //Update teams
            tm.UpdateTeams();
            //Check if game ended
            gameEnded = tm.IsThereOneTeamLeft();
            if (gameEnded) //If game ended
            {
                CommonCommands.NextGame(tm.GetTeamFirstPlace(), tm.GetTeamSecondPlace()); //Start next game
            }
        }
    }
}
