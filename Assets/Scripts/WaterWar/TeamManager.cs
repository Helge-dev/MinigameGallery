using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    /// <summary>
    /// The key is any number between 0 and amount of teams-1 and  the value is a list of players in that team (Player id in character controller)
    /// </summary>
    public SortedDictionary<int, List<int>> GetSetTeams { get; set; } = new SortedDictionary<int, List<int>>();

    public void PlacePlayersInTeams(PlayerSpawnMananger psm)
    {
        List<int> players = new List<int>();
        players.AddRange(DataStorage.GetSetControllers.Keys);
        int teamAmount = GetAmountOfTeams(players.Count);
        int teamSize = GetTeamSize(teamAmount, players.Count);
        Debug.Log("Started");
        List<int> playersToAdd = new List<int>();
        for (int i = 0; i < teamAmount; i++)
        {
            playersToAdd = new List<int>();
            for (int c = 0; c < teamSize; c++)
            {
                int randomPlayerID = players[Mathf.FloorToInt(Random.Range(0,players.Count))];
                playersToAdd.Add(randomPlayerID); //Add a random player to the team
                players.Remove(randomPlayerID);
            }
            GetSetTeams.Add(i, playersToAdd);
        }
        players.Clear();
        SetPlayersTeamColor(psm);
    }
    /// <summary>
    /// Add a team color to each player
    /// </summary>
    void SetPlayersTeamColor(PlayerSpawnMananger psm)
    {
        PlayerSpawnMananger spawnM = GetComponent<PlayerSpawnMananger>();
        Color teamColor;
        foreach (List<int> team in GetSetTeams.Values)
        {
            teamColor = DataStorage.GetSetPlayerColor[team[0]];
            foreach (int player in team)
            {
                spawnM.GetSetPlayers[player].GetComponent<ColorCustomizer>().SetTeamColor(teamColor);
                Debug.Log("Team count" + team.Count + "Player " + player);
            }
        }
    }
    /// <summary>
    /// Returns a somewhat random teamcount depending on the player count
    /// </summary>
    /// <param name="playerCount">Amount of players</param>
    /// <returns></returns>
    int GetAmountOfTeams(int playerCount)
    {
        switch (playerCount)
        {
            case 8:
                if (GetRandomBoolean())
                    return 2;
                else
                    return 4;
            case 6:
                if (GetRandomBoolean())
                    return 3;
                else
                    return 2;
            case 4:
                if (GetRandomBoolean())
                    return 4;
                else
                    return 2;
            default:
                return playerCount;
        }
    }
    /// <summary>
    /// Returns the size of teams
    /// </summary>
    /// <param name="amountOfTeams">Amount of teams</param>
    /// <param name="playerCount">Amount of players</param>
    /// <returns></returns>
    int GetTeamSize(int amountOfTeams, int playerCount)
    {
        if (amountOfTeams == playerCount || playerCount % 2 != 0) //If amount of teams is the amount of players or uneven amount of players, return 1
        {
            return 1;
        }
        else
        {
            return playerCount / amountOfTeams;
        }
    }
    bool GetRandomBoolean() => Random.value > 0.5;
    /// <summary>
    /// Clear all lists in this class.
    /// </summary>
    public void ClearTeams()
    {
        foreach (List<int> i in GetSetTeams.Values)
        {
            i.Clear();
        }
        GetSetTeams.Clear();
    }

    public bool IsThereOneTeamLeft()
    {
        PlayerSpawnMananger psm = GetComponent<PlayerSpawnMananger>();
        bool oneTeamAliveFound = false;
        foreach (List<int> i in GetSetTeams.Values) //For each team
        {
            foreach (int p in i) //For each player
            {
                if (!psm.GetSetPlayers[p].GetComponent<PlayerBehaviour>().GetSetPlayerOutOfGame) //If player is alive
                {
                    if (oneTeamAliveFound) //If another team is alive
                    {
                        return false; //Return false
                    }
                    else //Else
                    {
                        oneTeamAliveFound = true; //Set that a team is alive
                        break;
                    }
                }
            }
        }
        if (!oneTeamAliveFound)
        {
            Debug.Log("There is O players alive?");
        }
        return true; //Return that only one team is alive
    }
}
