/*
 * Made By Helge Herrström
 * Contains data that all scenes share
 */
using System.Collections.Generic;
using UnityEngine;

public static class DataStorage
{
    readonly static string[] eightPlayerGames = { "WaterWar", "Table Tennis" },
                             fourPlayerGames = { };
    /*
     * Variables
     */
    static int amountOfGames = 0, //How many games is one round
               gamesLeft = 0; //Games left to play
    static SortedDictionary<int, int> scores = new SortedDictionary<int, int>(); //Stores the players scores. The key is the players personal number (For Example, Player 1 has 1, P2 2 and so on)
    static SortedDictionary<int, Color> playerColors = new SortedDictionary<int, Color>(); //Stores the players color. The key is the players personal number (For Example, Player 1 has 1, P2 2 and so on)
    static SortedDictionary<int, InputManager> controllers = new SortedDictionary<int, InputManager>(); //Stores player controllers, the index is the player priority (ex, player 1, player 2)
    static List<string> playableGames = new List<string>(); // strings of scenes playable (some games can't be played with more than 4 playerss)
    static List<string> filteredGames = new List<string>(); // strings of scenes not to load
    /*
     * Properties
     */
    public static string[] GetEightPlayerGames { get => eightPlayerGames; }
    public static string[] GetFourPlayerGames { get => fourPlayerGames; }
    /// <summary>
    /// Returns how many games are left to play
    /// </summary>
    public static int GetSetGamesLeft
    {
        get => gamesLeft;
        set => gamesLeft = value;
    }
    /// <summary>
    /// Returns how many games to have played
    /// </summary>
    public static int AmountOfGames
    {
        get => amountOfGames;
        set => amountOfGames = value;
    }
    /// <summary>
    /// Returns player scores
    /// </summary>
    public static SortedDictionary<int, int> GetSetScore
    {
        get => scores;
        set => scores = value;
    }
    /// <summary>
    /// Returns player colors
    /// </summary>
    public static SortedDictionary<int, Color> GetSetPlayerColor
    {
        get => playerColors;
        set => playerColors = value;
    }
    /// <summary>
    /// Returns player controllers
    /// </summary>
    public static SortedDictionary<int, InputManager> GetSetControllers
    {
        get => controllers;
        set => controllers = value;
    }
    /// <summary>
    /// Returns a list of scene names that can be played with the amount of players connected
    /// </summary>
    public static List<string> GetSetPlayableGames
    {
        get => playableGames;
        set => playableGames = value;
    }
    /// <summary>
    /// Returns a list of scenes that has been played
    /// </summary>
    public static List<string> GetSetFilteredGames
    {
        get => filteredGames;
        set => filteredGames = value;
    }
}
