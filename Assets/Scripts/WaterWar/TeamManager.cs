using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] PlayerSpawnMananger playerSpawnManager;
    /// <summary>
    /// The key is any number between 0 and amount of teams-1 and  the value is a list of players in that team (Player id in character controller)
    /// </summary>
    public SortedDictionary<int, List<int>> GetSetTeams { get; set; } = new SortedDictionary<int, List<int>>();
    List<int> teamOutOrder = new List<int>(); //Saves the order in which teams are out
    /// <summary>
    /// Update team logic (Keeps track of which team is out or not)
    /// </summary>
    public void UpdateTeams()
    {
        UpdateTeamOutList();
    }
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
        teamOutOrder.Clear();
    }
    /// <summary>
    /// Returns true if only one team is left
    /// </summary>
    /// <returns></returns>
    public bool IsThereOneTeamLeft()
    {
        if (teamOutOrder.Count >= GetSetTeams.Keys.Count - 1)
            return true;
        return false;
    }
    /// <summary>
    /// Returns the last team standing
    /// </summary>
    /// <returns></returns>
    public List<int> GetTeamFirstPlace()
    {
        //Find the team that isn't out of game and return them
        foreach (int i in GetSetTeams.Keys)
        {
            if (!teamOutOrder.Contains(i))
            {
                return GetSetTeams[i];
            }
        }
        Debug.Log("WARNING: There is no winner!");
        return null;
    }
    /// <summary>
    /// Returns the team in second place
    /// </summary>
    /// <returns></returns>
    public List<int> GetTeamSecondPlace()
    {
        return GetSetTeams[teamOutOrder[GetSetTeams.Count - 2]];
    }

    void UpdateTeamOutList()
    {
        for (int team = 0; team < GetSetTeams.Count; team++)
        {
            if (teamOutOrder.Contains(team))
            {
                continue;
            }
            else
            {
                bool playerInGame = false;
                foreach (int player in GetSetTeams[team])
                {
                    if (!playerSpawnManager.GetSetPlayers[player].GetComponent<PlayerBehaviour>().GetSetPlayerOutOfGame)
                    {
                        playerInGame = true;
                        break;
                    }
                }
                if (!playerInGame)
                {
                    teamOutOrder.Add(team);
                }
            }
        }
    }
}
