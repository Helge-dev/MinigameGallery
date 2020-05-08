using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerSpawnMananger psm = null;
    [SerializeField] TeamManager tm = null;
    [SerializeField] UIManager uim = null;
    bool gameEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        //Show Game Instructions
        //Create Map
        //Spawn Players
        psm.CreatePlayers();
        uim.SetupUI(psm);
    }
    void Update()
    {
        CheckIfGameDone();
        uim.UpdateUI(psm);
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
