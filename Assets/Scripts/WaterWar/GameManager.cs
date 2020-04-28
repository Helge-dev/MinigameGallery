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
        if (!gameEnded)
        {
            gameEnded = tm.IsThereOneTeamLeft();
            if (gameEnded)
            {
                CommonCommands.NextGame(new System.Collections.Generic.List<int>(), new System.Collections.Generic.List<int>());
            }
        }
    }
}
